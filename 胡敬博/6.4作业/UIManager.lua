--模拟对象
local uiManager = BaseClass("uiManager")
uiManager.name = "uiManager"

local view,proxy
function uiManager:__init()

    self.uiDic = {}
    self.canvas = GameObject.Find("Canvas")
    self.ani = GameObject.Find("bg"):GetComponent("Animator")
end

function uiManager:GetLayerByID(layer)
    return GameObject.Find(layer)
end

function uiManager:OpenUI(id)
    
    
    if self.uiDic[id]==nil then

        
       local config = require("UI/"..id.."/"..id.."Config")

       local uiPrefab = GameObject.Instantiate(Resources.Load(config.uiPrefab),self:GetLayerByID(config.uiLayer).transform)

       config.controllerMod.New(config.viewMod.New(uiPrefab), config.proxyMod.New())
       
        self.uiDic[id] = config.viewMod.New(uiPrefab)

    else 
        self.uiDic[id].prefab.gameObject:SetActive(true)
    end

end 

function uiManager:CloseUI(id)
    
    if self.uiDic[id] ~= nil then
        print("关闭"..id)
        self.uiDic[id].prefab.gameObject:SetActive(false)
    end

end

return uiManager