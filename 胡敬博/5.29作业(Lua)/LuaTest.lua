local CS = CS or {}

local function  Start() --模拟unity中生命周期函数——Start
    print("lua Start...")
end

local function Update() -- 模拟unity中生命周期函数——Update
    print("lua Update...")
end

local LuaTest = CS.LuaTest

LuaTest.LuaStaticFunction() --Cshap中静态方法

CS.LuaTest():LuaFuction() --Cshap中非静态方法


local table = {1,nil,3,4,5}

for index, value in ipairs(table) do
    if index ~= nil then
        print("index",index,"value",value)
    end
end

for key, value in pairs(table) do
    
end

--ipairs 只要遍历的table中存在nil就会立刻结束循环
--pairs  会遍历table中所有不为nil的元素