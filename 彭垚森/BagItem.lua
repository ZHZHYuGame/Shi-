local BagItem = BaseClass("BagItem")
local function BeginDragHandle(self, point)
    if (self.BagItemData == nil) then return end
    self.imgMove:SetActive(true)
    self.go=self.gameObject.transform:Find("icon")
    self.go.gameObject:SetActive(false)
    self.imgMove:GetComponent("Image").sprite = Resources.Load(self.BagItemData.icon, typeof(CS.UnityEngine.Sprite))
    local data=PlayerDataManager:GetBagData()
end
local function DragHandle(self, point)
    -- 拖动图片跟随鼠标位置
    self.imgMove.transform.position = Input.mousePosition
end

local function EndDragHandle(self, point)
    self.imgMove:SetActive(false)
    self.go.gameObject:SetActive(true)
    -- 通知背包界面刷新（假设通过MessageCenter发送事件）
    --MessageCenter:Send(ClientSystemEvent.BagData_Update) 
end
function BagItem:__init(Obj, img, data,gridId)
    self.gameObject = Obj
    --self.imgMove=img
    self.imgMove = img
    self.BagItemData = data
    self.gridId = gridId
    -- 绑定 self 到事件处理函数
    local function bind(self, func)
        return function(...)
            return func(self, ...)
        end
    end
    --Icon=Obj.transform:Find("icon").gameObject:GetComponent("Image")
    UIEventListener.AddObjEvent(Obj):AddEventListener(EventTriggerType.BeginDrag, Bind(self, BeginDragHandle))
    UIEventListener.AddObjEvent(Obj):AddEventListener(EventTriggerType.Drag, Bind(self, DragHandle))
    UIEventListener.AddObjEvent(Obj):AddEventListener(EventTriggerType.EndDrag, Bind(self, EndDragHandle))
end

function BagItem:InitData(data)

end

return BagItem
