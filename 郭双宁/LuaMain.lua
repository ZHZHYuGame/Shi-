
function LuaStart()
    print("游戏入口---Lua部分生命周期Start方法随着游戏运行Stare开始")
end

require("LuaCollision")

require("Common/head")
require("Common/BaseClass")
require("Common/LuaEventTriggerType")
require("Framework/UIEnum")
UBase = require("Framework/UIBase")

local uMgr=require("Framework/UIManager")
uiManager=uMgr.New()
--uiManager:OpenUI(uiEnum.Login)


function AddNum(a,b)
    return a+b
end

function  SayHello(name)
    return 'Hello,'..name
end

--Lua有编译顺序，有编译顺序代表执行前必须生命好
--声明变量有两种情况1.local  2.不加local

s=1
local a=12



CS.TestLua.TestFunCShap()

local class=CS.TestLua()
--class.TestFun1(class)




local cube = GameObject.Find("Cube")

function Test1()
    print("Lua 里执行点击逻辑")
end

UIEventListener.AddObjEvent(cube):AddEventListener(LuaEventTriggerType.PointerClick, Test1)

local player=GameObject.Find("Player")

local moveComponent=CS.UnityEngine.GameObject.Find("Player"):GetComponent("MoveComponent")

--[[function LuaUpdate()

    local h=CS.UnityEngine.Input.GetAxis("Horizontal")
    local v=CS.UnityEngine.Input.GetAxis("Vertical")
    moveComponent:Move(h,v)
end]]--

function LuaUpdate()
    print("进行移动")
    local h=CS.UnityEngine.Input.GetAxis("Horizontal")
    local v=CS.UnityEngine.Input.GetAxis("Vertical")
    if h~=0 or v~=0 then
        player:GetComponent("MoveComponent"):Move(h,v)
    end

end

--UIEventListener.AddObjEvent(player):AddEventListener(LuaEventTriggerType.Move,PlayerMove)


