function LuaStart()
    print("进入");
end


function LuaUpdate()
   --Update 每一帧执行
end

--声明变量有两种情况1.local("局部变量"), 2.不加local("全局变量")
--1.string 
local s="cys"
s1="zym"
s2=s..s1
--".."在lua里面是+
--print("s2=",s2)
--输出s2= cyszym


--2.number双精度浮点
local n=5/1
--then=={}
if n==5 then
   -- print("n=",n)
   --n=5.0
end


--3.bool
local b=true
if b then
   -- print("b=",b)
   --b=true
end

--4.function==void 方法名
function Init (a,b,c)
    return 1,2
end
Init(a,b,c)
--print(Init(a,b,c))
local a = 1
local b = "sdf"
a,b = b,a
--print("a = ",a, "  b = ",b)
--a=sbf b=1


--5.nil==null
--"~""=="!"
if a ~= nil then
    --print("a")
    --a
end

--6.************table *************
--集合，对象，字典
local tab={}
--对象
tab.name="车延槊"
--集合
tab[1]="18"
--字典
tab["key"]={}
--print(tab["key"],tab.name,tab[1])
--0.~~~ 车延槊 18


--7.thread
--8.userdata


--传统自增
for i = 1, 10, 1 do
    
end
--自减式循环
for i = 10,1, -2 do
  --  print(i);
end

--集合 只有连续的输出 如果为nil不输出 不连续不输出
local list = {}
list[1] = 1
list[2] = 2
list[3] = 3
list[4] = nil
list[5] = 5
list[6] = 6
list["abc"] = "dd"
list["fgh"] = "vv"
--传统线性列表式循环，Lua中Index从1开始
for index, value in ipairs(list) do
    --print("index = ", index, "   value = ", value)
end
--字典 乱序
local dict = {}
dict["a"] = a
dict["震"] = "震"
dict["遥"] = "遥"
dict["b"] = b
dict["城"] = "城"
for key, value in pairs(dict) do
   -- print("key = ", key, "  value = ", value)
end

--调方法 静态
CS.TestLua.TestFunCShap()
--非静态
local class = CS.TestLua()
--1
class.TestFun1(class)
--2
class:TestFun1()
