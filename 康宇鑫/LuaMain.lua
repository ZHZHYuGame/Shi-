function LuaStart ()
    --print("游戏入口——Lua部分生命周期Start方法随着游戏运行Start开始");
end
function LuaUptade()
    
end

require("Common/head")
require("Common/BaseClass")
require("Common/LuaEventTtriggerType")
require("Framework/UIEnum")    
uiBase=require("Framework/UIManager")
local uMgr=require("Framework/UIManager") --加载UIManager.lua脚本
uiManager=uMgr.New()
uiManager:OpenUI(uiEnum.Login)
--lua有编译顺序，有编译顺序代表调用执行前必须声明好
--生命变量有两种情况：1.local 2.不加local
--1.string
local s="aa"
s1="111"
s2=s..s1
--print("s2=",s2);
--2.number
local n=5/1
if (n==5) then
  --  print("n=",n);
end
--3.bool

--4.function
function TestFun(a,b,c)
    return 1,2;
end
local x,y,z=TestFun();
--print("TestFun return value1=",x,"value2=",y,"value3=",z);

local a=1
local b=2
a,b=b,a
--print("a=",a,"b=",b);
--5.nil
if(a~=nil)  then

end
--6.*********************table*********************
local tab={}
tab.name="韩超傻逼"--对象
tab[1]="250"--数组/集合
tab["Key"]={}--字典--输出的是表在内存中的地址

tab.VoidFun=function()
    
end

--print("tab.name=",tab.name,"tab[1]=",tab[1],"tab[Key]=",tab["Key"]);

--7.thread
--8.userdata
--传统自增、自减式循环
for i = 1, 10, 3 do
    --print("i=",i);
end
local list={}
list[0]=0
list[1]=1
list[2]=2
list[3]=3
list[4]=4
list[6]=6
--传统线性列表式循环(遇空则断，不管空的是键还是值)
--lua中数组索引通常从1开始
--print("ipairs");
-- for index, value in ipairs(list) do
--     print("index=",index,"value=",value);   
-- end
--传统哈希键盘式循环
local dic={}
dic["a"]="a";
dic["d"]="d";
dic["c"]="c";
dic["b"]="b";
dic["e"]="e";
dic["f"]="f";
print("pairs");
-- for index, value in pairs(dic) do
--     print("index=",index,"value=",value);
-- end
--静态
CS.TestLua.TestFunCShap()
--非静态
local class=CS.TestLua()
class.TestFunCShap1(class)
class:TestFunCShap1()

--lua中没有对象的概念，官方的翻译是模块


--print("调用前  uiName",uMgr.uiName)
--uMgr:OpenUI(42326) --调用UIManager.lua脚本中的OpenUI方法
--uMgr.OpenUI(42323) --调用UIManager.lua脚本中的OpenUI方法
--print("uMgr=",uMgr);
--uMgr:OpenUI(43426)
--print("调用后  uiName",uMgr.uiName)


local canvas=CS.UnityEngine.GameObject.Find("Canvas")
canvas.gameObject:AddComponent(typeof(CS.TestCode))
--print("canvas=",canvas);    

local cube=GameObject.Find("Cube")  
function Test1()
    --print(":Lua里点击执行的逻辑")
end

UIEventListener.AddObjEvent(cube):AddEventListeren(EventTriggerType.PointerClick,Test1)

local tab1={}
local tab2={}
tab2.tabName="tab2"
--用元方法的形式将tab1与tab2进行属性与方法的关联绑定
--设置普通元表、元方法进行一个特性的赋予
local meatTables={__index=tab2}
local sTab=setmetatable(tab1, meatTables) 

--print("tab1.tabName=",tab1.tabName)

local tab3=getmetatable(sTab)
--[[print("tab1",tab1)
print("tab2",tab2)
print("tab3",tab3) 
print("meatTables",meatTables) 
print("tab1 is tab3?= ",tab1==tab3)
print("tab2 is tab3?= ",tab2==tab3)
print("meatTables is tab3?= ",meatTables==tab3)]]--
