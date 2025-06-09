local BagModel=BaseClass("BagModel");
function BagModel:__init()
    print("已进入BagModel")
    self.BagDict = {}
    self:InitBagData();
end

function BagModel:InitBagData()
    for i = 1, 5, 1 do
        -- BagData =require("BagData")
        -- self.BagDict[i] = i;
        -- sel
    end

end
return BagModel;