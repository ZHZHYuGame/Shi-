
local bagView=BaseClass("bagView")
function bagView:__init(prefabObj)
    
    self.centent=prefabObj.gameObject.transform:Find("Centent")

end
return bagView