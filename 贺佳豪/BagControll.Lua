local bagControll=BaseClass("bagControll")
function  bagControll.TestData()
    local gData=require("GameData/GoodData/GoodData")
    for i=1,2 do
        local img=GameObject.Find("Image"..i)
        local goodData=gData.New(i,"物品名称"..i,1,1,"1")
        local item=require("UI/Bag/BagViewItem").New(img)
    end
end
function bagControll:__init (...)
    local par={...}
    self.TestData()
    
    
end
function  bagControll:ClientAddListener()
   
end
 function bagControll:Bag_Add_ViewShow( )

 end
--背包添加物品逻辑处理
 function bagControll:Bag_Add_Handle(gData)
    --添加一个物品信息
    --1.数据层已经刷新添加的物品到整体数据中
    --self.model:BagAddData(gData)
    --2.通知View层显示出来,通过self.view拿到相关的组件，进行赋值显示
    --self:Bag_Add_ViewShow()
end
--背包删除物品逻辑处理
 function bagControll:Bag_Del_Handle(s)
    print("删除物品Del.....",self)
    print("self.model.bagList=",self.model)
end
--背包更新物品逻辑处理
 function bagControll:Bag_Updata_Handle()

end
--背包整理物品逻辑处理
 function bagControll:Bag_Clean_Handle()

end
function  bagControll:NetAddListener()
     MessageControll:AddListener(NetMessageID.S2C_Bag_Add,Bind(self,self.Bag_Add_Handle))
       MessageControll:AddListener(NetMessageID.S2C_Bag_Del,Bind(self,self.Bag_Del_Handle))
     MessageControll:AddListener(NetMessageID.S2C_Bag_Updata,Bind(self,self.Bag_Updata_Handle))
        MessageControll:AddListener(NetMessageID.S2C_Bag_Clean,Bind(self,self.Bag_Clean_Handle))
end
--显示背包列表
function  bagControll:BagInitItem(  )

    for i=1,25 do
        
        if i<self.model.list.Count then
        local item=GameObject.Instantiate(Resources.Load("Item"),self.view.bagBack.transform)
        local itemScripts= require("UI/Bag/BagViewItem") 
         item.transform:Find("BagItem").gameObject:GetComponent("Image").sprite=Resources.Load("icon/"..self.model.list[i].icon,typeof(CS.UnityEngine.Sprite))
     itemScripts.New(item)
        else
            local item=GameObject.Instantiate(Resources.Load("Item"),self.view.bagBack.transform)
             local itemScripts= require("UI/Bag/BagViewItem") 
            item.transform:Find("BagItem").gameObject:GetComponent("Image").sprite=Resources.Load("icon/bg_道具",typeof(CS.UnityEngine.Sprite))
           itemScripts.New(item)
        end
    end
end

function bagControll:__initFinish()
    self:ClientAddListener()
    self:NetAddListener()
    --初始化组件View层的第1次显示背包列表
    --self.model.bagList
    self:BagInitItem()
end

return bagControll