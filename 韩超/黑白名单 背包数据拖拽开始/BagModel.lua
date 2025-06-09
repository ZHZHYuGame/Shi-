local bagModel = BaseClass("bagModel")
--初始化 增加 变化 删除
function bagModel:__init()
    local configManager=CS.ConfigManager()
    self.list=configManager:ReadJson()
    self.bagList=playerDataManager:GetBagList()
end
function bagModel:onEnable()
    self.bagList=playerDataManager:GetBagList()
end
function bagModel:BagupdataData()
    
end
function bagModel:AddListener()
    
end
return bagModel