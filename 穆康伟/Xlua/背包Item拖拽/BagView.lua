local BagView=BaseClass("BagView")

function BagView:__init(obj)
    self.gameObject = obj
    self.closeBtn = self.gameObject.transform:Find("closeBtn"):GetComponent("Button");
    self.BagItemBox=self.gameObject.transform:Find("BagItemBox").transform
end
return BagView;