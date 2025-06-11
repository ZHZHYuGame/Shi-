local bagModel = BaseClass("bagModel")
--背包数据层
--1.初始化数据
--2.获取数据
--3.设置数据
--4.修改数据
--5.删除数据
local BagData = {}
function bagModel:__init()
    --self.bagList=PlayerDataManager:GetBagList()
end
function bagModel:OnEnable()
    --self.bagList=PlayerDataManager:GetBagList()
end
function bagModel:AddListener()
    
end
function bagModel:SetData()
    self.BagData = {
        [1] = {
            id = 101,
            name = "头盔",
            icon = "1",
        },
        [2] = {
            id = 102,
            name = "戒指",
            icon = "2",
        },
        [3] = {
            id = 103,
            name = "手镯",
            icon = "3",
        },
        [4] = {
            id = 104,
            name = "武器",
            icon = "4",
        },
        [5] = {
            id = 105,
            name = "盔甲",
            icon = "5",
        }
    }
    PlayerDataManager:SetBagData(self.BagData)
end

return bagModel
