--模拟对象
local uiManager = {}

--打开UI方法1
--大忌
--全局方法，只有在全局需要的时候才会在特殊位置这样写
function OpenUI(id)

end
--打开UI方法2
--结构是上面模拟的对象.方法
--[[function uiManager.OpenUI(id)
    print(".    self = ", self, "   id = ", id)
end]]--
--打开UI方法3
--结构是上面模拟的对象：方法
function uiManager:OpenUI(id)
    print(":    self = ", self, "   id = ", id)
end

local function OpenUI1()
   print("OpenUI11111") 
end

uiManager.OpenUI1 = OpenUI1

return uiManager