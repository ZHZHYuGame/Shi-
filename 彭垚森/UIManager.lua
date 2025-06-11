--模拟对象
local uiManager = BaseClass("uiManager")
local uiName="UIManager";
uiManager.uiName="uiManager"
function uiManager:__init()
    print("UI框架构造")
    self.UIDict={}
    self.canvasParent=GameObject.Find("Canvas")
end

--获取对应层所在的Canvas
function uiManager:GetLayerToCanvas(layer)
    return GameObject.Find(layer)
end
--打开UI方法
--结构是上面模拟的对象：方法
--id UI枚举
function uiManager:OpenUI(id)
    --判断UI预制体是否为空加载预制体
    if self.UIDict[id]==nil then
        --加载对应的UI预制件
        --local uiPrefab=GameObject.Instantiate(Resources.Load(id),self.canvasParent.transform)
        --[[--对应UI功能的脚本加载
        local uiLuaCode=require("UI/"..id.."/"..id.."Code")
        --对应UI脚本注册预制件绑定
        uiLuaCode.New(uiPrefab)
        --加载对应UI的Lua 脚本
        self.UIDict[id]=uiLuaCode]]--
        --[[local Control=require("UI/"..id.."/"..id.."Control")
        local Model=require("UI/"..id.."/"..id.."Model")
        local View=require("UI/"..id.."/"..id.."View")
        Control=Control.New()
        Control.model=Model.New()
        Control.view=View.New(uiPrefab)
        self.UIDict[id]=Control
        else self.UIDict[id].view.gameObject:SetActive(true)]]--   
        local config=require("UI/"..id.."/"..id.."Config")
        local uiPrefab=GameObject.Instantiate(Resources.Load(id),self:GetLayerToCanvas(config.Layer).transform)
        --local uiPrefab=GameObject.Instantiate(Resources.Load(id),self.canvasParent.transform)
        --MVC脚本1模块导入
        config.ControlCode=config.ControlCode.New()
        config.ControlCode.model=config.ModelCode.New()
        config.ControlCode.view=config.ViewCode.New(uiPrefab)
        --MVC脚本模块导入完成
        config.ControlCode:InitFinish()
        self.UIDict[id]=config
        
    end
end
function uiManager:GetUI(id)
    
end
function uiManager:CloseUI(id)
    if(self.UIDict[id]~=nil) then
        --GameObject.Destroy(self.UIDict[id])
        self.UIDict[id].view.gameObject:SetActive(false)
    end
end
return uiManager