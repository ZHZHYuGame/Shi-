local mainController = BaseClass("mainController")
mainController.name = "mainController"

local view,proxy

function mainController:BindUIEvent()

    print(view.btn_BagItem)
    view.btn_BagItem.onClick:AddListener(function ()
        UIManager:OpenUI(UIEnum.bag)

    end)
end

function mainController:__init(_view,_proxy)
    
     view = _view
    proxy = _proxy

    
    mainController:BindUIEvent()
end

return mainController