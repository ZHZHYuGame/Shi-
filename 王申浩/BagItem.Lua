local bagViewItem=BaseClass("bagViewItem")


function bagViewItem:InitBagItemIcon(id)
       
        icon:GetComponent("Image").sprite=Resources.Load(("icon/"..id),typeof(CS.UnityEngine.Sprite))
end
--拖拽开始
local function BeginDragHandle(pointDrag)
    print("拖拽开始。。。。"..pointDrag.name)
    if moveImg == nil then
        moveImg=GameObject.Find("ImageMove")
    else
        moveImg:SetActive(true)
    end

     moveImg.transform.parent=GameObject.Find("UI_Bag(Clone)").transform
    --moveImg:GetComponent("Image").sprite=Resources.Load(GetImageToName(pointDrag.name),typeof(CS.UnityEngine.Sprite))
     BeginIcon=pointDrag.gameObject.transform:Find("icon"):GetComponent("Image")
    moveImg:GetComponent("Image").sprite=icon:GetComponent("Image").sprite
end
--拖拽结束
local function EndDragHandle(pointDrag)
    moveImg:GetComponent("Image").raycastTarget=false
    moveImg:SetActive(false)
    EndImag=pointDrag:GetComponent("Image").sprite
    if moveImg:GetComponent("Image").sprite~=pointDrag:GetComponent("Image").sprite then
      pointDrag:GetComponent("Image").sprite= BeginIcon:GetComponent("Image").sprite
      BeginIcon:GetComponent("Image").sprite=EndImag
    end
    
end
--拖拽过程中
local function DragHandle(pointDrag)
    moveImg.transform.position=Input.mousePosition
    pointDrag:GetComponent("Image").sprite=BeginIcon:GetComponent("Image").sprite
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