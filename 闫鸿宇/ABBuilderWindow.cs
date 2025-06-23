#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; // 添加MD5计算命名空间
using System.Text; // 添加StringBuilder命名空间

public class ABBuilderWindow : EditorWindow
{
    // 配置数据
    private string rootFolder = "Assets/ABAssets";
    private string outputPath = "AssetBundles";
    private BuildTarget buildTarget = BuildTarget.StandaloneWindows;
    private bool clearNamesBeforeBuild = true;
    private string namingRule = "{path}";
    private string fileSuffix = "bundle";
    private NameCase nameCase = NameCase.LowerCase;
    private CompressionType compressionType = CompressionType.LZ4;
    private FolderPackMode folderPackMode = FolderPackMode.IndividualFiles;
    private bool generateMD5Manifest = true; // 新增：是否生成MD5清单
    
    // UI状态
    private Vector2 scrollPos;
    private bool showAdvancedSettings;
    private readonly HashSet<string> customRules = new HashSet<string> { "{path}", "{folder}", "{filename}" };
    
    // 打包策略枚举
    private enum CompressionType
    {
        Uncompressed,
        LZMA,
        LZ4
    }
    
    private enum FolderPackMode
    {
        IndividualFiles,  // 每个文件单独打包
        MergeByFolder,    // 每个文件夹合并打包
        Both              // 同时生成两种模式
    }
    
    private enum NameCase
    {
        Original,
        LowerCase,
        UpperCase
    }

    [MenuItem("Tools/AssetBundle Builder Pro")]
    public static void ShowWindow()
    {
        GetWindow<ABBuilderWindow>("AB Builder Pro");
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        
        // 基础配置区域
        EditorGUILayout.LabelField("基础配置", EditorStyles.boldLabel);
        FolderSelectionField("资源根目录", ref rootFolder);
        FolderSelectionField("输出路径", ref outputPath);
        buildTarget = (BuildTarget)EditorGUILayout.EnumPopup("目标平台", buildTarget);
        
        // 压缩策略
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("压缩策略", EditorStyles.boldLabel);
        compressionType = (CompressionType)EditorGUILayout.EnumPopup("压缩方式", compressionType);
        EditorGUILayout.HelpBox(
            "Uncompressed - 不压缩(加载快，体积大)\n" +
            "LZMA - 高压缩率(加载慢，体积小)\n" +
            "LZ4 - 平衡模式(加载快，体积适中)", 
            MessageType.Info);
        
        // 文件夹打包模式
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("文件夹打包模式", EditorStyles.boldLabel);
        folderPackMode = (FolderPackMode)EditorGUILayout.EnumPopup("打包策略", folderPackMode);
        EditorGUILayout.HelpBox(
            "IndividualFiles - 每个文件单独打包\n" +
            "MergeByFolder - 整个文件夹打包为一个AB包\n" +
            "Both - 同时生成两种模式", 
            MessageType.Info);
        
        // 高级设置（可折叠）
        showAdvancedSettings = EditorGUILayout.Foldout(showAdvancedSettings, "高级设置");
        if (showAdvancedSettings)
        {
            EditorGUI.indentLevel++;
            clearNamesBeforeBuild = EditorGUILayout.Toggle("清理旧名称", clearNamesBeforeBuild);
            
            // 新增：MD5清单选项
            generateMD5Manifest = EditorGUILayout.Toggle("生成MD5清单", generateMD5Manifest);
            if (generateMD5Manifest)
            {
                EditorGUILayout.HelpBox("将为每个AB包文件生成MD5校验码，并创建manifest.json清单文件", MessageType.Info);
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("AB包命名规则", EditorStyles.boldLabel);
            namingRule = EditorGUILayout.TextField("命名格式", namingRule);
            nameCase = (NameCase)EditorGUILayout.EnumPopup("大小写规则", nameCase);
            EditorGUILayout.HelpBox("可用变量:\n" +
                                    "{path} - 完整相对路径\n" +
                                    "{folder} - 所属文件夹名\n" +
                                    "{filename} - 文件名", 
                MessageType.Info);
            
            fileSuffix = EditorGUILayout.TextField("文件后缀", fileSuffix);
            if (!fileSuffix.StartsWith(".") && !string.IsNullOrEmpty(fileSuffix))
            {
                fileSuffix = "." + fileSuffix;
            }
            EditorGUI.indentLevel--;
        }
        
        // 操作按钮
        EditorGUILayout.Space();
        if (GUILayout.Button("构建AB包", GUILayout.Height(40)))
        {
            BuildAssetBundles();
        }
        
        EditorGUILayout.EndScrollView();
    }

    private void BuildAssetBundles()
    {
        try
        {
            // 清理旧名称
            if (clearNamesBeforeBuild)
            {
                ClearAllAssetBundleNames();
                Debug.Log("已清理旧AB包名称");
            }

            // 收集所有资源
            string[] allAssets = AssetDatabase.FindAssets("", new[] { rootFolder })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(p => !p.EndsWith(".meta"))
                .ToArray();

            // 设置AssetBundle名称
            int total = allAssets.Length;
            Dictionary<string, List<string>> folderToAssets = new Dictionary<string, List<string>>();
            
            for (int i = 0; i < allAssets.Length; i++)
            {
                string assetPath = allAssets[i];
                
                // 根据模式设置AB包名称
                switch (folderPackMode)
                {
                    case FolderPackMode.IndividualFiles:
                        SetIndividualAssetBundleName(assetPath);
                        break;
                        
                    case FolderPackMode.MergeByFolder:
                        AddToFolderGroup(assetPath, folderToAssets);
                        break;
                        
                    case FolderPackMode.Both:
                        SetIndividualAssetBundleName(assetPath);
                        AddToFolderGroup(assetPath, folderToAssets);
                        break;
                }
                
                // 显示进度
                EditorUtility.DisplayProgressBar("设置AB包名称", 
                    $"[{i+1}/{total}] {assetPath}", 
                    (float)i / total);
            }
            
            // 处理文件夹合并模式
            if (folderPackMode != FolderPackMode.IndividualFiles)
            {
                int folderCount = folderToAssets.Count;
                int currentFolder = 0;
                
                foreach (var kvp in folderToAssets)
                {
                    currentFolder++;
                    string folderPath = kvp.Key;
                    string bundleName = GenerateBundleName(folderPath, isFolder: true);
                    
                    foreach (string assetPath in kvp.Value)
                    {
                        AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(
                            bundleName, 
                            folderPackMode == FolderPackMode.Both ? "folder" : "");
                    }
                    
                    EditorUtility.DisplayProgressBar("设置文件夹AB包", 
                        $"[{currentFolder}/{folderCount}] {folderPath}", 
                        (float)currentFolder / folderCount);
                }
            }

            // 构建AB包
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            BuildAssetBundleOptions options = GetCompressionOptions();
            BuildPipeline.BuildAssetBundles(outputPath, options, buildTarget);
            
            // 添加文件后缀
            AddFileSuffix();
            
            // 生成MD5清单
            if (generateMD5Manifest)
            {
                GenerateMD5Manifest();
            }

            Debug.Log($"AB包构建完成！输出目录：{outputPath}");
        }
        finally
        {
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
    }

    // 新增：生成MD5清单
    private void GenerateMD5Manifest()
    {
        try
        {
            string manifestPath = Path.Combine(outputPath, "manifest.json");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("  \"files\": [");
            
            // 获取所有AB文件（包括子目录）
            string[] allFiles = Directory.GetFiles(outputPath, "*", SearchOption.AllDirectories)
                .Where(file => 
                    !file.EndsWith(".meta") && 
                    !file.EndsWith(".manifest") && 
                    !Path.GetFileName(file).StartsWith("."))
                .ToArray();
            
            int totalFiles = allFiles.Length;
            int processed = 0;
            
            foreach (string filePath in allFiles)
            {
                processed++;
                string relativePath = filePath.Substring(outputPath.Length).TrimStart(Path.DirectorySeparatorChar);
                string md5 = CalculateMD5(filePath);
                
                sb.Append("    { ");
                sb.Append($"\"path\": \"{relativePath.Replace("\\", "/")}\", ");
                sb.Append($"\"md5\": \"{md5}\"");
                
                // 添加逗号（最后一个元素不加）
                if (processed < totalFiles)
                {
                    sb.AppendLine(" },");
                }
                else
                {
                    sb.AppendLine(" }");
                }
                
                EditorUtility.DisplayProgressBar("生成MD5清单", 
                    $"[{processed}/{totalFiles}] {Path.GetFileName(filePath)}", 
                    (float)processed / totalFiles);
            }
            
            sb.AppendLine("  ]");
            sb.AppendLine("}");
            
            File.WriteAllText(manifestPath, sb.ToString());
            Debug.Log($"MD5清单已生成: {manifestPath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"生成MD5清单失败: {e.Message}");
        }
    }
    
    // 新增：计算文件的MD5值
    private string CalculateMD5(string filePath)
    {
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);
                return System.BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }

    private void SetIndividualAssetBundleName(string assetPath)
    {
        string bundleName = GenerateBundleName(assetPath);
        AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(
            bundleName, 
            folderPackMode == FolderPackMode.Both ? "file" : "");
    }

    private void AddToFolderGroup(string assetPath, Dictionary<string, List<string>> folderGroups)
    {
        string folderPath = Path.GetDirectoryName(assetPath);
        
        if (!folderGroups.ContainsKey(folderPath))
        {
            folderGroups[folderPath] = new List<string>();
        }
        
        folderGroups[folderPath].Add(assetPath);
    }

    private string GenerateBundleName(string path, bool isFolder = false)
    {
        // 获取相对路径
        string relativePath = path.Substring(rootFolder.Length + 1);
        string dirPath = Path.GetDirectoryName(relativePath);
        string fileName = isFolder ? Path.GetFileName(relativePath) : Path.GetFileNameWithoutExtension(relativePath);

        // 替换变量
        string result = namingRule
            .Replace("{path}", (dirPath != null ? dirPath + "/" : "") + fileName)
            .Replace("{folder}", Path.GetFileName(dirPath) ?? fileName)
            .Replace("{filename}", fileName);

        // 格式化路径
        result = result.Replace("/", "_").Replace("\\", "_");
        
        // 处理大小写
        switch (nameCase)
        {
            case NameCase.LowerCase:
                result = result.ToLower();
                break;
            case NameCase.UpperCase:
                result = result.ToUpper();
                break;
        }

        // 添加后缀
        if (!string.IsNullOrEmpty(fileSuffix) && !isFolder)
        {
            result += fileSuffix;
        }

        return result;
    }

    private BuildAssetBundleOptions GetCompressionOptions()
    {
        switch (compressionType)
        {
            case CompressionType.Uncompressed:
                return BuildAssetBundleOptions.UncompressedAssetBundle;
            case CompressionType.LZMA:
                return BuildAssetBundleOptions.None;
            case CompressionType.LZ4:
                return BuildAssetBundleOptions.ChunkBasedCompression;
            default:
                return BuildAssetBundleOptions.ChunkBasedCompression;
        }
    }

    private void AddFileSuffix()
    {
        string platformFolder = buildTarget.ToString();
        string fullOutputPath = Path.Combine(outputPath, platformFolder);
        
        if (Directory.Exists(fullOutputPath))
        {
            foreach (string file in Directory.GetFiles(fullOutputPath))
            {
                if (!file.Contains(".") && !file.EndsWith(".manifest"))
                {
                    string newPath = file + fileSuffix;
                    File.Move(file, newPath);
                    File.Move(file + ".manifest", newPath + ".manifest");
                }
            }
        }
    }

    private static void FolderSelectionField(string label, ref string path)
    {
        EditorGUILayout.BeginHorizontal();
        path = EditorGUILayout.TextField(label, path);
        if (GUILayout.Button("选择", GUILayout.Width(50)))
        {
            string newPath = EditorUtility.OpenFolderPanel("选择文件夹", 
                Path.GetDirectoryName(path), "");
            if (!string.IsNullOrEmpty(newPath))
            {
                path = "Assets" + newPath.Replace(Application.dataPath, "");
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private static void ClearAllAssetBundleNames()
    {
        foreach (var name in AssetDatabase.GetAllAssetBundleNames())
        {
            AssetDatabase.RemoveAssetBundleName(name, true);
        }
    }
}
#endif