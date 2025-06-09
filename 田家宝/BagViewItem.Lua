local bagViewItem=BaseClass("bagViewItem")


function bagViewItem:InitBagItemIcon(id)
       
        icon:GetComponent("Image").sprite=Resources.Load(("icon/"..id),typeof(CS.UnityEngine.Sprite))
end
--拖拽开始
local function BeginDragHandle(pointDrag)
    print("拖拽开始。。。。"..pointDrag.name)
    if moveImg == nil then
        moveImg=GameObject.Find("ImageMove")
        moveImg.transform.parent=GameObject.Find("UI_Bag(Clone)").transform
      
    else
        moveImg:SetActive(true)
    end

    --moveImg:GetComponent("Image").sprite=Resources.Load(GetImageToName(pointDrag.name),typeof(CS.UnityEngine.Sprite))
    beginicon=pointDrag.gameObject.transform:Find("icon"):GetComponent("Image")
    moveImg:GetComponent("Image").sprite=beginicon:GetComponent("Image").sprite
end
--拖拽结束
local function EndDragHandle(pointDrag)
    moveImg:GetComponent("Image").raycastTarget=false
    moveImg:SetActive(false)
    local endicon=pointDrag:GetComponent("Image").sprite
    if pointDrag.transform. name=="icon" then
        if moveImg:GetComponent("Image").sprite~=pointDrag:GetComponent("Image").sprite then
           
        pointDrag:GetComponent("Image").sprite=beginicon:GetComponent("Image").sprite
        beginicon:GetComponent("Image").sprite=endicon
    end
    end
    
end
--拖拽过程中
local function DragHandle(pointDrag)
    moveImg.transform.position=Input.mousePosition
end
    
function bagViewItem:__init(obj)
        self.img=obj
        self.gameObject=obj
        UIEventListener.AddObjEvent(self.img):AddEventListener(LuaEventTriggerType.BeginDrag,BeginDragHandle)
        UIEventListener.AddObjEvent(self.img):AddEventListener(LuaEventTriggerType.EndDrag,EndDragHandle)
        UIEventListener.AddObjEvent(self.img):AddEventListener(LuaEventTriggerType.Drag,DragHandle)
        icon=self.gameObject.transform:Find("icon"):GetComponent("Image")
      
    end
return bagViewItem