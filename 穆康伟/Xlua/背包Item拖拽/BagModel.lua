local BagModel=BaseClass("BagModel");
function BagModel:__init()
    print("已进入BagModel")
    self.BagDict = {}
    self.iconPathDict={"icon/baihuwang_1b","icon/baihuwang_2b","icon/baihuwang_3b","icon/baihuwang_4b","icon/baihuwang_5b"};
    self:InitBagData();
end

function BagModel:InitBagData()
     local BagData = require("UI/Bag/BagData")
    for i = 1, 5, 1 do 
        local bagData=BagData.new(i,self.iconPathDict[i]);
        self.BagDict[i]=bagData;
    end 
    end
return BagModel;