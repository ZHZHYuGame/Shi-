local bagview=BaseClass("bagview")
local names={"全部","装备","药品","宝箱"}
local nameLength=#names
local typelist={}
function bagview:InitTypeBtn()
    print(self.TypeBtnBack)
    for i = 1, nameLength do
        print(1111)
       local typebtn=GameObject.Instantiate(Resources.Load("Type_Btn"),self.TypeBtnBack.transform)
       --print(typebtn.transform:GetComponent("Button").transform:GetComponentInChildren("Text"))
      -- typebtn.gameObject.transform:GetComponentInChildren("Text").text=names[i]
    end
end
function bagview:__init(prefebObj)
    self.gameObject=prefebObj
    self.bagBack=self.gameObject.transform:Find("BagBack")
    self.TypeBtnBack=self.gameObject.transform:Find("TypeBtnObj")
    self:InitTypeBtn()
end

return bagview