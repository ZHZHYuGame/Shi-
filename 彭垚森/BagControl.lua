local bagControl=BaseClass("bagControl")

function bagControl:__init()
    --self.model:SetData()
end
function bagControl:BagInitItem()
    
    for i = 1, 12, 1 do
        local item=GameObject.Instantiate(Resources.Load("BagItem"),self.view.content)
        local itemScript=require("UI/Bag/BagItem")
        itemScript.New(item)
        --itemScript.icon.sprite=Resources.Load("2")
        --itemScript.name_text.text="测试"
        --print(self.model.AllData[i])
        --itemScript:Init(self.model.AllData[i])
    end
end
function bagControl:ClientAddListener()
    
end
function bagControl:NetAddListener()
    
end
function bagControl:InitFinish()
    self:ClientAddListener()
    self:NetAddListener()
    self:BagInitItem()
    self.model:SetData()
end
return bagControl