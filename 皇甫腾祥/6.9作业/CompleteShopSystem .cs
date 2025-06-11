using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// 完整的商城系统管理器
/// 功能包括：商品展示、购买、分类筛选、搜索、数据持久化等
/// </summary>
public class CompleteShopSystem : MonoBehaviour
{
    // 单例模式，确保全局只有一个商城实例
    public static CompleteShopSystem Instance;

    /// <summary>
    /// 商品数据结构
    /// </summary>
    [System.Serializable]
    public class ShopItem
    {
        public string itemName;      // 商品名称
        public Sprite itemIcon;     // 商品图标
        public int itemPrice;       // 商品价格
        public bool isPurchased;    // 是否已购买
        public ItemCategory category; // 商品分类
    }

    /// <summary>
    /// 商品分类枚举
    /// </summary>
    public enum ItemCategory { All, Consumable, Equipment, Skin }

    // ========== UI引用部分 ==========
    [Header("UI References")]
    public GameObject shopItemPrefab;   // 商品UI预制体
    public Transform shopContent;       // 商品列表容器
    public Text coinsText;              // 金币显示文本
    public InputField searchField;     // 搜索输入框
    public Dropdown categoryDropdown;   // 分类下拉菜单

    // ========== 商城设置部分 ==========
    [Header("Shop Settings")]
    public int initialPlayerCoins = 1000;  // 初始玩家金币
    public List<ShopItem> shopItems = new List<ShopItem>(); // 商品列表

    // 私有变量
    private int playerCoins;             // 玩家当前金币
    private List<GameObject> spawnedItems = new List<GameObject>(); // 已生成的商品UI列表

    /// <summary>
    /// 初始化方法
    /// </summary>
    void Awake()
    {
        // 单例模式实现
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 初始化金币
        playerCoins = initialPlayerCoins;
        UpdateCoinsUI();

        // 设置分类下拉菜单
        SetupCategoryDropdown();
    }

    /// <summary>
    /// 游戏开始时调用
    /// </summary>
    void Start()
    {
        // 加载保存的商城数据
        LoadShopData();
        // 填充商城(默认显示所有商品)
        PopulateShop(ItemCategory.All);
        
        // 设置搜索功能
        if (searchField != null)
        {
            searchField.onValueChanged.AddListener(delegate { SearchItems(); });
        }
    }

    /// <summary>
    /// 初始化分类下拉菜单
    /// </summary>
    void SetupCategoryDropdown()
    {
        if (categoryDropdown != null)
        {
            categoryDropdown.ClearOptions();
            List<string> categories = new List<string>();
            
            // 获取所有分类枚举值
            foreach (ItemCategory category in System.Enum.GetValues(typeof(ItemCategory)))
            {
                categories.Add(category.ToString());
            }
            
            categoryDropdown.AddOptions(categories);
            categoryDropdown.onValueChanged.AddListener(delegate { FilterByCategory(); });
        }
    }

    /// <summary>
    /// 填充商城商品列表
    /// </summary>
    /// <param name="category">要显示的商品分类</param>
    public void PopulateShop(ItemCategory category)
    {
        // 清空现有商品
        ClearShop();

        // 遍历所有商品
        foreach (ShopItem item in shopItems)
        {
            // 应用分类过滤器
            if (category != ItemCategory.All && item.category != category)
                continue;

            // 实例化商品UI
            GameObject itemGO = Instantiate(shopItemPrefab, shopContent);
            spawnedItems.Add(itemGO);
            
            // 获取UI组件引用
            Text nameText = itemGO.transform.Find("NameText").GetComponent<Text>();
            Image iconImage = itemGO.transform.Find("IconImage").GetComponent<Image>();
            Text priceText = itemGO.transform.Find("PriceText").GetComponent<Text>();
            Button buyButton = itemGO.transform.Find("BuyButton").GetComponent<Button>();
            Text buttonText = buyButton.GetComponentInChildren<Text>();

            // 设置商品数据
            nameText.text = item.itemName;
            iconImage.sprite = item.itemIcon;
            priceText.text = item.itemPrice.ToString();

            // 根据购买状态设置按钮状态
            if (item.isPurchased)
            {
                buyButton.interactable = false;
                buttonText.text = "已购买";
            }
            else
            {
                buyButton.interactable = playerCoins >= item.itemPrice;
                buttonText.text = "购买";
                buyButton.onClick.AddListener(() => BuyItem(item));
            }
        }
    }

    /// <summary>
    /// 购买商品
    /// </summary>
    /// <param name="item">要购买的商品</param>
    public void BuyItem(ShopItem item)
    {
        // 检查是否可以购买
        if (playerCoins >= item.itemPrice && !item.isPurchased)
        {
            // 扣除金币
            playerCoins -= item.itemPrice;
            item.isPurchased = true;
            
            // 更新UI
            UpdateCoinsUI();
            RefreshShopUI();
            // 保存数据
            SaveShopData();
            
            Debug.Log("购买成功: " + item.itemName);
        }
        else if (item.isPurchased)
        {
            Debug.Log("已经购买过此商品");
        }
        else
        {
            Debug.Log("金币不足");
        }
    }

    /// <summary>
    /// 搜索商品
    /// </summary>
    public void SearchItems()
    {
        // 如果搜索框为空，刷新整个商城
        if (string.IsNullOrEmpty(searchField.text))
        {
            RefreshShopUI();
            return;
        }

        string searchText = searchField.text.ToLower();
        ItemCategory currentCategory = (ItemCategory)categoryDropdown.value;

        // 遍历所有生成的商品UI
        foreach (GameObject itemGO in spawnedItems)
        {
            Text nameText = itemGO.transform.Find("NameText").GetComponent<Text>();
            bool nameMatches = nameText.text.ToLower().Contains(searchText);
            
            // 检查商品是否符合搜索条件和当前分类
            bool shouldShow = nameMatches && 
                           (currentCategory == ItemCategory.All || 
                            shopItems.Exists(x => x.itemName == nameText.text && x.category == currentCategory));
            
            // 设置显示/隐藏
            itemGO.SetActive(shouldShow);
        }
    }

    /// <summary>
    /// 按分类筛选商品
    /// </summary>
    public void FilterByCategory()
    {
        ItemCategory selectedCategory = (ItemCategory)categoryDropdown.value;
        PopulateShop(selectedCategory);
    }

    /// <summary>
    /// 刷新商城UI
    /// </summary>
    void RefreshShopUI()
    {
        ItemCategory currentCategory = (ItemCategory)categoryDropdown.value;
        PopulateShop(currentCategory);
    }

    /// <summary>
    /// 清空商城
    /// </summary>
    void ClearShop()
    {
        foreach (GameObject item in spawnedItems)
        {
            Destroy(item);
        }
        spawnedItems.Clear();
    }

    /// <summary>
    /// 更新金币UI显示
    /// </summary>
    void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = playerCoins.ToString();
        }
    }

    // ========== 数据持久化部分 ==========
    #region Data Persistence

    /// <summary>
    /// 保存商城数据
    /// </summary>
    public void SaveShopData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/shopData.dat");
        
        ShopData data = new ShopData();
        data.playerCoins = playerCoins;
        data.shopItems = shopItems;
        
        bf.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// 加载商城数据
    /// </summary>
    public void LoadShopData()
    {
        if (File.Exists(Application.persistentDataPath + "/shopData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/shopData.dat", FileMode.Open);
            ShopData data = (ShopData)bf.Deserialize(file);
            file.Close();
            
            playerCoins = data.playerCoins;
            shopItems = data.shopItems;
        }
    }

    /// <summary>
    /// 商城数据存储结构
    /// </summary>
    [System.Serializable]
    class ShopData
    {
        public int playerCoins;          // 玩家金币
        public List<ShopItem> shopItems;  // 商品列表
    }

    #endregion

    // ========== 货币管理部分 ==========
    #region Currency Management

    /// <summary>
    /// 增加金币
    /// </summary>
    /// <param name="amount">增加的数量</param>
    public void AddCoins(int amount)
    {
        playerCoins += amount;
        UpdateCoinsUI();
        RefreshShopUI();
        SaveShopData();
    }

    /// <summary>
    /// 花费金币
    /// </summary>
    /// <param name="amount">花费的数量</param>
    /// <returns>是否花费成功</returns>
    public bool SpendCoins(int amount)
    {
        if (playerCoins >= amount)
        {
            playerCoins -= amount;
            UpdateCoinsUI();
            RefreshShopUI();
            SaveShopData();
            return true;
        }
        return false;
    }

    #endregion

    /// <summary>
    /// 应用退出时自动保存数据
    /// </summary>
    void OnApplicationQuit()
    {
        SaveShopData();
    }
}