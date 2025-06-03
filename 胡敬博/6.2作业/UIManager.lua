--模拟对象
local uiManager = BaseClass("uiManager")
uiManager.name = "uiManager"

function uiManager:__init()

    self.uiDic = {}
    self.canvas = GameObject.Find("Canvas")
end

function uiManager:OpenUI(id)

    if self.uiDic[id]==nil then

       local uiPrefab = GameObject.Instantiate(Resources.Load(id),self.canvas.transform)
        
       local uiView = require("UI/"..id)

        uiView.New(uiPrefab)

        self.uiDic[id] = uiView
    end

end 

return uiManager