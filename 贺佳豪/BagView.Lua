
local bagView=BaseClass("bagView")
function bagView:__init(prefabObj)
    
    self.bagBack=prefabObj.gameObject.transform:Find("Centent")
    --self.bagBack=prefabObj.gameObject.transform 

end
return bagView