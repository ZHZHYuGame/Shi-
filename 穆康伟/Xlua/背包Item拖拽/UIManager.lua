local UIManager = BaseClass("UIManager");
function UIManager:__init()
    print("执行UIManager！！");
    self.UIDict = {}
    self.canvasParent = GameObject.Find("UIRoot")
end

function UIManager:OpenUI(prefabName)
    if self.UIDict[prefabName] == nil then
        local uiPrefab = GameObject.Instantiate(Resources.Load(prefabName), self.canvasParent.transform);

        local controller = require("UI/" .. prefabName .. "/" .. prefabName .. "Controller")
        local model = require("UI/" .. prefabName .. "/" .. prefabName .. "Model")
        local view = require("UI/" .. prefabName .. "/" .. prefabName .. "View")
        controller = controller.New();
        controller.model = model.New();
        controller.view = view.New(uiPrefab);
        controller:__initFinish();
        self.UIDict[prefabName] = controller;
    else
        self.UIDict[prefabName].view.gameObject:SetActive(true)
    end
end

function UIManager:CloseUI(prefabName)
    if self.UIDict[prefabName] ~= nil then
        self.UIDict[prefabName].view.gameObject:SetActive(false)
    end
end 
return UIManager
