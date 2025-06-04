function  LuaStart()
    print("游戏入口---Lua部分生命周期Start方法随着游戏运行Start开始")
end


function  LuaUpdate()
    local Inputx=CS.UnityEngine.Input.GetAxis("Horizontal")
    local Inputy=CS.UnityEngine.Input.GetAxis("Vertical")
    local player=GameObject.Find("Player")
    if Inputx~=0 or Inputy~=0 then
        --print("x=",Inputx,"y=",Inputy)
        player:GetComponent("PlayerMove"):Move(CS.UnityEngine.Vector3(Inputx,0,Inputy))
    end
end

require("Common/head")
require("Common/BaseClass")
require("Common/LuaEventTtriggerType")
require("Framework/UIEnum")
UBase = require("Framework/UIBase")
require("Common/PathTitle")
require("Net/NetMessageID")
MessageController=require("MessageController")
require("ClientSystemEvent")

local uMgr=require("Framework/UIManager")
UIManager=uMgr.New()

UIManager:OpenUI(uiEnum.Login)

--Lua 有编译顺序，有编译顺序代表调用执行前必须声明好
--声明变量有两种情况1.local 局部 2.不加local 全局
--1.string
local s="aaa" 
s1='111'
s2=s..s1
-- +在Lua中用..来表示
--print("s=",s2)
--2.number
local n=5/1;
--  if n==5 then
--      print("n=5")
--  else
--     print("n!=5")
--  end
--print("n=",n);
--3.boolean

--4.function
function  TestFun(a,b,c)
    return 1,2
end
local x,y= TestFun();
--print("x=",x,"y=",y)

--两数交换
local a=1;
local b=2;
a,b=b,a;
--print("a=",a,"b=",b)

--5.nil
-- if a~=nil then
--     print("a is not nil")
-- else
--     print("a is nil")
-- end
--6.table
local tab={}
tab.name="Fanfan"
tab[1]="7"
tab["Key"]={}
--print("tab.name=",tab.name,"tab[1]=",tab[1],"tab[Key]=",tab["Key"])

tab.VoidFun=function ()
    
end

--table的遍历
for i=1,10 do
    --print("i=",i)
end
--传统自增自减式循环
--table的遍历自增 每次加3
for i = 1, 10, 3 do
    --print("i=",i)
end
--table的遍历自减 每次减3
for i = 10, 1, -3 do
    --print("i=",i)
end

local list={}
list[1]=1;
list[2]=2;
list[3]=3;
list[4]=4;
list[6]=6;
--传统线性列表循环，Lua中Index从1开始
--遇空则断，Index为空或Value为空则不继续执行
for index, value in ipairs(list) do
    --print("index=",index,"value=",value)
end

local dic={}
dic["a"]="a"
dic["d"]="d";
dic["c"]="c"
dic["e"]="e";
dic["b"]="b"
--传统字典式循环，Lua中Index从1开始
--传统哈希键值式存贮，指针式读取 循环
for key, value in pairs(dic) do
    --print("key=",key,"value=",value)
end
--7.thread

--8.userdata

--Lua脚本调用C#脚本中的方法
--静态方法
CS.TestLua.TestFunCShap()
--非静态方法的两种
local class=CS.TestLua()
--1
class:TestFunCShapTwo()
--2
class.TestFunCShapThree(class)

--lua中没有对象的概念 官方翻译的是模块

--local uMgr=require("UIManager")
--打开UI方法1

--uMgr.OpenUI(10086);
--打开UI方法2

--uMgr:OpenUI(10086);
--print("uMgr=",uMgr)

--uMgr.OpenUI1(10086);
-- print("调用前 uMgr.uiName=",uMgr.uiName)
-- uMgr:OpenUI(10086);
-- print("调用后 uMgr.uiName=",uMgr.uiName)

--require("Common/head")
local canvas= CS.UnityEngine.GameObject.Find("Canvas")
canvas.gameObject:AddComponent(typeof(CS.TestCode))
--print("canvas=",canvas)

local cube=GameObject.Find("Cube")
local num=1;
function Test1()
    print("Lua里这里点击执行逻辑")
    num=num+1
    cube.gameObject.transform.localScale=CS.UnityEngine.Vector3(num,num,num)
end

 local function SetNewMoveDir()
        print("Lua里这里设置移动方向逻辑")
        if Inputx~=0 or Inputy~=0 then
        --print("x=",x,"y=",y)
        print("1111111111111111")
        cube.GetComponent("PlayerMove"):Move(CS.UnityEngine.Vector3(Inputx,0,Inputy))
     end
 end
--require("Common/LuaEventTtriggerType")

UIEventListener.AddObjEvent(cube):AddEventListener(LuaEventTriggerType.PointerClick,Test1)      
--UIEventListener.AddObjEvent(cube):AddEventListener(LuaEventTriggerType.Move ,SetNewMoveDir)
local tab1={}
local tab2={}
tab2.tabName="tab2"
--用元方法的形式将tab1与tab2进行属性与方法的关联绑定
--设置普通表与元表、元方法进行一个特性的赋予
local meattables={__index=tab2}
local sTab= setmetatable(tab1,meattables)

--print("tab1.tabName=",tab1.tabName)


local tab3 = getmetatable(sTab)
--[[print("tab1=",tab1)
print("tab2=",tab2)
print("tab3=",tab3)
print("meattables=",meattables)

print("tab1 is tab3?",tab1==tab3)
print("tab2 is tab3?",tab2==tab3)
print("meattables is tab3?",meattables==tab3)]]--

--require("Common/BaseClass")

 