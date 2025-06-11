local BagController=BaseClass("BagController")

function BagController:__init()

    print("进入BagController");
    self.BagItemDic={}
    self.BagItemObj = {}
    self.id=0;
end


function BagController:__initFinish()
    self:AddEventListener();
    self:InitBagCell()
    self:InitBagData();
end
function BagController:InitBagData()
    
end
function BagController:InitBagCell()

    local BagItemCode = require("UI/Bag/BagItem")
    for i = 1, 20, 1 do
        local bagItemObj = GameObject.Instantiate(Resources.Load("BagItem"), self.view.BagItemBox);
        bagItemObj.name = i;
        self.BagItemDic[i] = BagItemCode
        self.BagItemObj[i] = bagItemObj
    end
    for i = 1, 20, 1 do
        self.id = self.id + 1;
        if #self.model.BagDict >= i then
            self.BagItemDic[i].New(self.BagItemObj[i], self.model.BagDict[i], self.id, self.view.DragBagItem);
        else
            self.BagItemDic[i].New(self.BagItemObj[i], "", self.id, self.view.DragBagItem);
        end
    end
    BagController.BagItemDic = self.BagItemDic;
    BagController.BagItemObj=self.BagItemObj;
    BagController.model = self.model;
    BagController.view = self.view;
    for index, value in ipairs(self.BagItemDic) do
        print(index, value);
    end
end
function BagController:RefrshData()
    self.id=0;
    for i = 1, #self.BagItemDic, 1 do
        self.BagItemDic[i] = nil;
    end
     local BagItemCode = require("UI/Bag/BagItem")
    for i = 1, 20, 1 do
       
        self.BagItemDic[i] = BagItemCode
    end
    for i = 1, 20, 1 do
        self.id = self.id + 1;
        if #self.model.BagDict >= i then
            self.BagItemDic[i].New(self.BagItemObj[i], self.model.BagDict[i], self.id, self.view.DragBagItem);
        else
            self.BagItemDic[i].New(self.BagItemObj[i], "", self.id, self.view.DragBagItem);
        end
    end
end
function BagController:AddEventListener()
    self.view.closeBtn.onClick:AddListener(function()
        uiManager:CloseUI(UIEnum.Bag);
    end)
    
end




return BagController;