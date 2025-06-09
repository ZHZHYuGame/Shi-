local bagControll=BaseClass("bagControll")
function bagControll:__init()
   -- local config=require("UI/Bag/BagConfig")
    --print(config.name)
    --bagview=config.ViewCode
    --print(bagview.name)
    --Tran_BagItemBase=bagview.Tran_BagItemBase
    --print(Tran_BagItemBase.name)
    --self:CreatBagItem()
end
function bagControll:ClientAddListener()
    
end

function bagControll:NetAddListener()
    
end
function bagControll:CreatBagItem()
    
    --创建背包格子
    
    for i = 1, 16, 1 do
        local bagitem=GameObject.Instantiate(Resources.Load("BagItem"),Tran_BagItemBase.transform)
    end
end
return bagControll