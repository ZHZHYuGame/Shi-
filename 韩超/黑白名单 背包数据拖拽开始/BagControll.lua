local bagControll=BaseClass("bagControll")
function bagControll:__init()
    
end
function bagControll:clientAddListen()
    MessagerController:AddListeren(ClientSystemEvent.ClientToClient1,function ()
        print("客户端监听消息1---Client")
    end)
    self:InitBagItem()
end
function bagControll:NetAddListen()
    MessagerController:AddListeren(ClientSystemEvent.ClientToClient1,function ()
        print("客户端监听消息1---Net")
    end)
end
function bagControll:InitBagItem()
    for i = 1, 25  do
        local bagitem=GameObject.Instantiate(Resources.Load("BagitemBack"),self.view.bagBack.transform)
       bagitem.name=bagitem.name..i
        if i<self.model.list.Count then
            bagitem=require("/Bag/BagViewItem").New(self.model.list[i],bagitem)
        else
            bagitem=require("/Bag/BagViewItem").New(nil,bagitem)
        end
        -- if i<self.model.list.Count then
        --     local bagitem=GameObject.Instantiate(Resources.Load("BagitemBack"),self.view.bagBack.transform)
        --     bagitem.transform:Find("BagItem").gameObject:GetComponent("Image").sprite=Resources.Load("icon/"..self.model.list[i].icon,typeof(CS.UnityEngine.Sprite))
        -- else
        --     local bagitem=GameObject.Instantiate(Resources.Load("BagitemBack"),self.view.bagBack.transform)
        --     bagitem.transform:Find("BagItem").gameObject:GetComponent("Image").sprite=Resources.Load("icon/bg_道具",typeof(CS.UnityEngine.Sprite))
        -- end
    end
    
end
function bagControll:__initFinish()
    self:clientAddListen()
    self:NetAddListen()
end
return bagControll