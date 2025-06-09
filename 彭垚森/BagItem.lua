local BagItem=BaseClass("BagItem")
function BagItem:__init(Obj)
    self.gameObject=Obj;
    self.icon=Obj.gameObject.transform:Find("ssss"):GetComponent("Image")
    self.name_text=Obj.gameObject.transform:Find("name"):GetComponent("Text")
end
function BagItem:InitData(data)
    
end
return BagItem  