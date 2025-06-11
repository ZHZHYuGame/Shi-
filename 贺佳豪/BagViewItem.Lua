local bagViewItem=BaseClass("bagViewItem")

function GetImageToName(imgName)
    
end
  function bagViewItem:BeginDragHandle(pointDrag)
    print("拖拽中。。。。。。。",pointDrag.name,"   ",self.goodData.goodName)
    if moveImg==nil then
          moveImg=GameObject.Find("ImageMove")
    else
          moveImg:SetActive(true)
    end 

   -- moveImg.GetComponent("Image").sprite=Resources.Load(GetImageToName(pointDrag.name),typeof(CS.UnityEngine.Sprite))
end
  function bagViewItem:DragHandle(podragObj)
    moveImg.transform.position = Input.mousePosition
end
  function bagViewItem:EndDragHandle(podragObj)
    print("拖拽结束。。。。。。。","   ",self.goodData.goodName)
     
    moveImg:SetActive(false)
end
  
function  bagViewItem:__init(obj,data)
    self.img=obj
    self.goodData=data
    UIEventListener.AddObjEvent(self.img):AddEventListener(LuaEventTriggerType.BeginDrag,Bind(self,BeginDragHandle ))

    UIEventListener.AddObjEvent(self.img):AddEventListener(LuaEventTriggerType.Drag,Bind(self,DragHandle ))
    UIEventListener.AddObjEvent(self.img):AddEventListener(LuaEventTriggerType.EndDrag,Bind(self,EndDragHandle ))
end
return bagViewItem