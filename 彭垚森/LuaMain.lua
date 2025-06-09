CS=CS or {}
function LuaStart()
    print("进入LuaStart脚本");
end

require("head")
require("BaseClass")
require("Framework/UIEnum")
require("Net/NetMessageID")
UIBase=require("Framework/UIBase")
--Lua面向对象
local mgr=require("Framework/UIManager");
uiManager=mgr.New()
--uiManager:OpenUI(uiEnum.Login)

local dic={}
dic[1]="q"
dic[2]="w"
dic[3]="e"
dic[5]="r"
--[[for index, value in ipairs(dic) do
    print(" Key=",index," Value=",value);
end]]--


--CS.Test.TestFun1()
--CS.Test.TestFun2(4,3)
local Cube=UnityEngine.GameObject.Find("SS")
Cube:AddComponent(typeof(CS.Player))
--CS.Player.Test3()
--lua调用c#事件点击
require("EventTriggerType")
function TestClik()
    print("点击逻辑");
end
 CS.UIEventListener.AddObjEvent(Cube):AddEventListener(EventTriggerType.PointerClick, TestClik)
function RayClik()
    print("点击")
end
function LuaUpdata()
    --print("进入LuaUpdata脚本");
    --local h=Input.GetAxis("Horizontal")
    --local v=Input.GetAxis("Vertical")
    local h=CS.MoveTrigger.BackX()
    local v=CS.MoveTrigger.BackY()
    Cube.transform.position=Cube.transform.position+Vector3(h,0,v)*Time.deltaTime*3
end