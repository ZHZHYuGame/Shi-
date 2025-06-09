local bagView=BaseClass("bagView")
function bagView:__init(obj)
    
    self.gameObject=obj
    --self.btn_Close=self.gameObject.transform:Find("Close"):GetComponent("Button")
    self.content=self.gameObject.transform:Find("Content").transform
    self.imgMove=self.gameObject.transform:Find("ImageMove").gameObject
    --self.item=self.gameObject.transform:Find("Content/Item")
end
return bagView