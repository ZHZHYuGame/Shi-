--模拟对象
local uiManager =BaseClass("uiManager")
local uiName="uiManager"
uiManager.uiName="uiManager"

function uiManager:__init()
    print("UI框架的构造执行")
    self.UIDict={}
    self.canvasParent=GameObject.Find("Canvas")
end

--打开UI方法3
--结构式上面 对象：方法
--id UI枚举

function uiManager:OpenUI(id)
    --1.加载UI预制件
    if self.UIDict[id]==nil then
        --加载对应的UI预制件
        local uiPrefab=GameObject.Instantiate(Resources.Load(id),self.canvasParent.transform)
        --对应UI功能的脚本加载
        local uiLuaCode=require("UI/"..id.."/"..id.."Code")
        --对应UI脚本注册预制件绑定
        uiLuaCode.New(uiPrefab)
        --加载对应的UI的Lua Code
        self.UIDict[id]=uiLuaCode
    end

end


function uiManager:GetUI(id)
    
end

function uiManager:CloseUI(id)

    
end

return uiManager