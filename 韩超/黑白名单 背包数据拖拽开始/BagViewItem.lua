local bagViewItem=BaseClass("bagViewItem")
local function BeginDragHandle(pointDrag)
    print("拖拽开始---",pointDrag) 
    if(pointDrag.gameObject.transform:Find("BagItem"):GetComponent("Image").sprite~=Resources.Load("icon/bg_道具",typeof(CS.UnityEngine.Sprite)))  then
    if Imagemove==nil then
        Imagemove=GameObject.Find("UIRoot/ImageMove") 
    else
        Imagemove:SetActive(true)
    end
    Imagemove:GetComponent("Image").sprite=pointDrag.gameObject.transform:Find("BagItem"):GetComponent("Image").sprite
    end
    
end
local function DragHandle(pointDrag)
    print("拖拽中---") 
    Imagemove.transform.position=Input.mousePosition
end
local function EndDragHandle(pointDrag)
    print("拖拽结束---") 
    Imagemove:SetActive(false)
    print("end name---",pointDrag.name) 
end
function bagViewItem:__init(obj,item)
    self.gameObject=item
    
    if obj~=nil then
        Data=obj
        self.gameObject.transform:Find("BagItem").gameObject:GetComponent("Image").sprite=Resources.Load("icon/"..obj.icon,typeof(CS.UnityEngine.Sprite))
    else
        self.gameObject.transform:Find("BagItem").gameObject:GetComponent("Image").sprite=Resources.Load("icon/bg_道具",typeof(CS.UnityEngine.Sprite))
    end
    UIEventListener.AddObjEvent(self.gameObject):AddEventListeren(LuaEventTriggerType.BeginDrag,BeginDragHandle)
    UIEventListener.AddObjEvent(self.gameObject):AddEventListeren(LuaEventTriggerType.Drag,DragHandle)
    UIEventListener.AddObjEvent(self.gameObject):AddEventListeren(LuaEventTriggerType.EndDrag,EndDragHandle)
end

return bagViewItem