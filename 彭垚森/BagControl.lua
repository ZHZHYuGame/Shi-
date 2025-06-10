--local MessageCenter = require("Assets.Scripts.LuaCode.MessageCenter")
local bagControl=BaseClass("bagControl")

function bagControl:__init()
    --self.model:SetData()
end
function bagControl:BagInitItem()
    
    for i = 1, 12, 1 do
        local item=GameObject.Instantiate(Resources.Load("BagItem"),self.view.content)
        local itemScript=require("UI/Bag/BagItem") 
        itemScript.New(item,self.view.imgMove,self.model.BagData[i],i)
        itemScript.BagItemData=self.model.BagData[i]
        if(self.model.BagData[i]~=nil) then 
            item.transform:Find("icon"):GetComponent("Image").sprite=Resources.Load(self.model.BagData[i].icon,typeof(CS.UnityEngine.Sprite))
            item.transform:Find("name"):GetComponent("Text").text=self.model.BagData[i].name
            
        end
    end
end
-- 新增：交换两个格子的数据
function bagControl:ChangeItemData(targetBagItem)
    local bagList = PlayerDataManager:GetBagData()  -- 获取背包数据列表
    -- 交换当前格子（self.gridId）和目标格子（targetBagItem.gridId）的数据
    bagList[self.gridId], bagList[targetBagItem.gridId] = bagList[targetBagItem.gridId], bagList[self.gridId]
    PlayerDataManager:SetBagData(bagList)  -- 更新背包数据
    
    
end
function bagControl:ClientAddListener()
    
end
function bagControl:Bag_Add_ViewShow()
    
end
--背包添加物品逻辑处理
function bagControl:Bag_Add_Handle(gData)
    --添加一个物品
    --1.数据层已经刷新添加的物品到整体数据里
    --self.model:BagAddData(gData)
    --2.通知View层显示出来，通过self.view拿到相关的组件，进行赋值显示
    --self:Bag_Add_ViewShow()
end
--背包删除物品逻辑处理
function bagControl:Bag_Del_Handle()
    print("删除")
end
--背包更新物品逻辑处理
function bagControl:Bag_Update_Handle()
    
end
--背包整理物品逻辑处理
function bagControl:Bag_Clean_Handle(as)
    
end
function bagControl:NetAddListener()
    MessageCenter:AddListener(NetMessageID.S2C_Bag_Add,Bind(self,self.Bag_Add_Handle))
    MessageCenter:AddListener(NetMessageID.S2C_Bag_Del,Bind(self,self.Bag_Del_Handle))
    MessageCenter:AddListener(NetMessageID.S2C_Bag_Updata,Bind(self,self.Bag_Update_Handle))
    MessageCenter:AddListener(NetMessageID.S2C_Bag_Clean,Bind(self,self.Bag_Clean_Handle))
    --MessageCenter:AddListener(ClientSystemEvent.BagData_Update, Bind(self,self.ChangeItemData))
end
function bagControl:InitFinish()
    --初始化拿到model层数据
    self.model:SetData()
    self:ClientAddListener()
    self:NetAddListener()
    --初始化背包格子
    self:BagInitItem()
    
end
return bagControl