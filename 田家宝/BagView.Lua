local bagView=BaseClass("bagView")

function bagView:__init(prefabObj)
    print(prefabObj.name)
    self.gameObject=prefabObj
    self.input_Account=self.gameObject.transform:Find("Input_Account"):GetComponent("InputField")
    self.Tran_BagItemBase=self.gameObject.transform:Find("BagItemBase"):GetComponent("Image")
    for i = 1, 16, 1 do
        local bagitem=GameObject.Instantiate(Resources.Load("BagItem"), self.Tran_BagItemBase.transform)
    end
    
end
return bagView