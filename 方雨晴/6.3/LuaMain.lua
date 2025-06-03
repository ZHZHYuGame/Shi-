
function LuaStart()
    print("游戏入口---Lua部分生命周期Start方法随着游戏运行Stare开始")
end


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
class.TestFun1(class)




local cube = GameObject.Find("Cube")

function Test1()
    print("Lua 里执行点击逻辑")
end

UIEventListener.AddObjEvent(cube):AddEventListener(LuaEventTriggerType.PointerClick, Test1)
--找到玩家身上的脚本
local Heromove=CS.UnityEngine.GameObject.Find("Hero"):GetComponent("HeroMove")

function  LuaUpdate()
    --玩家移动
    local h= CS.UnityEngine.Input.GetAxis("Horizontal")
    local v=CS.UnityEngine.Input.GetAxis("Vertical")
    Heromove:Move(h,v)
end