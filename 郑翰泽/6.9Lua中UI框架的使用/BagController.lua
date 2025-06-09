---@diagnostic disable: undefined-global
local BagController=BaseClass("BagController")

function BagController:__init()

    print("进入BagController");
end


function BagController:__initFinish()
    self:AddEventListener();
    self:InitBagCell()
    -- self:Add
end
function BagController:InitBagCell()
    for i = 1, 5, 1 do
        local bagItemObj = GameObject.Instantiate(Resources.Load("BagItem"), self.view.BagItemBox);
        local BagItemCode = require("UI/Bag/BagItem")
        self.model.BagDict[i]=bagItemObj
        BagItemCode.New(bagItemObj);
        
    end
end

function BagController:AddEventListener()
    self.view.closeBtn.onClick:AddListener(function()
        uiManager:CloseUI(UIEnum.Bag);
    end)
    
end




return BagController;