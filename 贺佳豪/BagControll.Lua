local bagControll=BaseClass("bagControll")
function bagControll:__init()
    
end
function  bagControll:ClientAddListener()
    
end

function  bagControll:NetAddListener()
    
end
function  bagControll:BagInitItem()
    for i=1,10 do
        local item=GameObject.Instantiate(Resources.Load("Item"),self.view.centent)
         
    end
end

function bagControll:__initFinish()
    self:ClientAddListener()
    self:NetAddListener()
    self:BagInitItem()
end

return bagControll