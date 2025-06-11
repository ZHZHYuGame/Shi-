---@diagnostic disable: undefined-global
local BagItem = BaseClass("BagItem")

function BagItem:__init(obj)
    self.gameObject = obj
    print(obj);
    UIEventListener.AddObjEvent(obj):AddEventListener(LuaEventTriggerType.BeginDrag, function()
        print("拖拽开始")
        
    end)
    UIEventListener.AddObjEvent(obj):AddEventListener(LuaEventTriggerType.Drag, function()
        print("拖z中")
        obj.transform.position=Input.mousePosition;
        
    end)
    UIEventListener.AddObjEvent(obj):AddEventListener(LuaEventTriggerType.EndDrag,function()
        print("拖拽结束")
    end)
end
return BagItem;