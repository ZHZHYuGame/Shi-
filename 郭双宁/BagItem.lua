local BagItem = BaseClass("BagItem")

function BagItem:__init(obj, data, id, DragBagItem)
    self.gameObject = obj
    self.data = data;
    self.name_Text = self.gameObject.transform:Find("bg/name_Text");
    self.bg = self.gameObject.transform:Find("bg");
    self.id = id
    self.DragBagItem = DragBagItem
    if (data ~= "") then
        self.bg.gameObject:SetActive(true);
        self.name_Text:GetComponent("Text").text = data.name;
        self.bg.gameObject:GetComponent("Image").sprite = Resources.Load(data.iconPath, typeof(CS.UnityEngine.Sprite));
    elseif data == "" then
        self.bg.gameObject:SetActive(false);
    end
    UIEventListener.AddObjEvent(obj):AddEventListener(LuaEventTriggerType.BeginDrag, function()
        if (data ~= "") then
            self.DragBagItem.gameObject:SetActive(true);
            self.bg.gameObject:SetActive(false);
            self.DragBagItem.gameObject:GetComponent("Image").sprite=self.bg.gameObject:GetComponent("Image").sprite;
        end
    end)

    UIEventListener.AddObjEvent(obj):AddEventListener(LuaEventTriggerType.Drag, function()
        if (data ~= "") then
            self.DragBagItem.transform.position=Input.mousePosition;
        end
    end)
    local function OnDragEnd(obj)
        if (data ~= "") then
            self.DragBagItem.gameObject:SetActive(false);
            self.bg.gameObject:SetActive(true);
            local myID=tonumber(self.gameObject.name) 
            local failID = tonumber(obj.name)
            --交换数
            
            BagController.BagItemDic[myID], BagController.BagItemDic[failID] = BagController.BagItemDic[failID], BagController.BagItemDic[myID]
            --刷新数据
            BagController:RefrshData();
            -- if eventData.pointerCurrentRaycast.gameObject.GetComponent("") == nil
        end
    end
    UIEventListener.AddObjEvent(obj):AddEventListener(LuaEventTriggerType.EndDrag,OnDragEnd)
end
return BagItem;