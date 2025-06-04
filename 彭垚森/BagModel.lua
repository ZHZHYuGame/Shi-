local bagModel=BaseClass("bagModel")
local BagData={
    [1]={
        id=101,
        name="头盔",
        icon="1",
    },
    [2]={
        id=102,
        name="戒指",
        icon="2",
    },
    [3]={
        id=103,
        name="手镯",
        icon="3",
    },
    [4]={
        id=104,
        name="武器",
        icon="4",
    },
    [5]={
        id=105,
        name="盔甲",
        icon="5",
    }
}
function bagModel:__init()
    
end
function bagModel:SetData()
    self.AllData=BagData
end
return bagModel