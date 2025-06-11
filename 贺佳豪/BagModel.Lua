
local bagModel=BaseClass("bagModel")

--背包数据层
--1.背包数据的初始化（显示）
--2.背包数据的增加
--3.背包数据的变化
--4.背包数据的删除

--背包数据初始化
function bagModel:__init()
    local configManager=CS.ConfigManager()
    self.list=configManager:ReadJson()

    --角色的缓存里取背包的显示数据
    self.bagList=PlayerDataManager:GetBagData()


end
function bagModel:OnEnable()
     self.bagList=PlayerDataManager:GetBagData()
end
--背包添加物品数据
function  bagModel:BagAddData(gData)
    
end
function bagModel:BagUpdataData()
    
end
function  bagModel:AddListener()
    
end
return bagModel 