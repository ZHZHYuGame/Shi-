local UIManager = BaseClass("UIManager");
function UIManager:__init()
    print("执行UIManager！！");
    self.UIDict = {}
    self.canvasParent = GameObject.Find("Canvas")
end

function UIManager:OpenUI(prefabName)
    if self.UIDict[prefabName]==nil then
        local uiPrefab = GameObject.Instantiate(Resources.Load(prefabName), self.canvasParent.transform);
        
        local uiLuaCode=require("UI/"..prefabName)

        uiLuaCode.New(uiPrefab);

        self.UIDict[prefabName] = uiLuaCode;
    end
end

-- function UIManager:CloseUI(prefabName)
--    print(self.UIDict[prefabName])
--     -- if self.UIDict[prefabName] ~= nil then

--     --     self.UIDict[prefabName]:CloseUI();
        
        
--     -- end
-- end 
return UIManager
