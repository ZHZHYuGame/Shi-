local mainView = BaseClass("mainView",UIBase)
mainView.name = "mainView"

function mainView:__init(prefab)

    self.prefab = prefab
    self.btn_BagItem = prefab.gameObject.transform:Find("BagBtn"):GetComponent("Button")
end



return mainView