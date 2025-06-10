local messageCnter={}
local messDic={}

--消息中心事件注册id消息号 action事件
function messageCnter:AddListener(id,action)
    if(self.actionList==nil) then
        -- 初始化实例的 actionList（错误：与 messDic 无关）
        self.actionList={}
    end
    -- 将事件添加到实例的 actionList 中（错误：所有 id 共享同一个列表）
    table.insert(self.actionList,action)
    -- 将 messDic[id] 指向实例的 actionList（错误：不同 id 共享同一列表）
    messDic[id]=self.actionList
end
--消息中心事件删除id消息号 action事件
function messageCnter:RemoveListener(id)
    if(messDic[id]~=nil) then
        messDic[id]=nil  -- 直接清空该 id 的所有事件（无法精准移除单个 action）
    end
end
--消息中心派发数据
function messageCnter:Send(id,...)
    local aList=messDic[id]  -- 获取该 id 对应的事件列表（但可能包含其他 id 的事件）
    if aList~=nil then
        for index, value in ipairs(aList) do
            value(...)  -- 触发所有事件（可能包含不相关的事件）
        end
    end
end
return messageCnter