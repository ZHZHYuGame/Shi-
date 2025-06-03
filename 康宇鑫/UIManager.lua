--模拟对象
local uiManager = BaseClass("uiManager")
local uiName="UIManager"
uiManager.uiName="uiManager"
function uiManager:__init()
    print("UI框架的构造执行")
    self.UIDict = {} --UI字典   
    self.canvasParent=GameObject.Find("Canvas")
end
--打开UI方法1
--大忌  全局方法，只有在全局需要的时候才会在特殊位置这样写（不错，没有目标和明确性）
--[[function OpenUI(id)

end]]--
--打开UI方法2
--结构是上面模拟的对象，方法
-- function uiManager.OpenUI(id)
--     print("self=",self,"id=",id);
-- end
--打开UI方法3
--结构是上面模拟的对象：方法
--id UI枚举
function uiManager:OpenUI(id)
    --self.uiName="UIMgrInstance"
    --print("self=",self,"id=",id);

    --1.加载UI预制体
    if self.UIDict[id]==nil then
        --加载对应的UI预制体
        local uiPrefab=GameObject.Instantiate(Resources.Load(id),self.canvasParent.transform)
        --对应UI功能的脚本加载
        local uiLuaCode= require("UI/"..id.."/"..id.."Code")
        --对应UI脚本注册预制件绑定
        uiLuaCode.New(uiPrefab) 
        --加载对应UI的Lua Code
        self.UIDict[id]=uiPrefab
    end
    
end

function uiManager:GetUI(id)
    
end

function uiManager:CloseUI(id)
    
end


function OpenUI1()
    print("OpenUI11111");
end
uiManager.OpenUI1=OpenUI1
return uiManager

