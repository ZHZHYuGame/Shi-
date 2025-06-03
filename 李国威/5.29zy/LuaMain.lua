

function LuaStart()
    print("游戏入口---Lua部分生命周期Start方法随着游戏运行Start开始")
end

function LuaUpdate()

end
--Lua 有编译顺序，有编译顺序代表调用执行前必须声明好
--声明变量有两种情况1.local, 2.不加local
--1.string 
local s = "aaa"
s1 = '111'
s2 = s .. s1
--print("s2 = ", s2)
--2.number
local n = 5/1
if n == 5 then
    --print("n = ", n)
end
--3.bool

--4.function 


function TestFun(a, b, c)
    return 1,2
end

local a = 1
local b = "sdf"

a,b = b,a

--print("a = ",a, "  b = ",b)
--5.nil
if a ~= nil then

end
--6.************table *************
local tab = {}
tab.name = "2210"
tab[1] = "15"
tab["Key"] = {}
--7.thread
--8.userdata
--传统自增、自减式循环
for i = 10, 1 , -3 do
    
end
local list = {}
list[1] = 1
list[2] = 2
list[3] = 3
list[4] = 4
list[5] = nil
list[6] = 6
list["abc"] = "dd"
list["fgh"] = "vv"
--传统线性列表式循环，Lua中Index从1开始
for index, value in ipairs(list) do
    --print("index = ", index, "   value = ", value)
end

local dict = {}
dict["a"] = a
dict["震"] = "震"
dict["遥"] = "遥"
dict["b"] = b
dict["城"] = "城"
--传统哈希键盘式循环
--哈希字典能否有顺序遍历？
for key, value in pairs(list) do
    --print("key = ", key, "  value = ", value)
end
--ipairs与pairs的底层原理是什么？
CS.TestLua.TestFunCShap()

local class = CS.TestLua()
class.TestFun1(class)