local BagView=BaseClass("BagView")

function BagView:__init(obj)
    self.gameObject = obj
    self.closeBtn = self.gameObject.transform:Find("closeBtn"):GetComponent("Button");
    self.BagItemBox = self.gameObject.transform:Find("BagItemBox").transform
    self.DragBagItem = self.gameObject.transform:Find("DragBagItem");
    self.DragBagItem.gameObject:SetActive(false);
end
return BagView;