

local bagController = BaseClass("bagController")
bagController.name = "loginController"

local view,proxy

function  bagController:BindUIEvent()  --ui绑定事件
    view.btn_login.onClick:AddListener(function()
    bagController:OnclickLoginBtn(view.input_account.text,view.input_password.text)
    end)

end
function bagController:__init(_view,_proxy)
    view,proxy = _view,_proxy
    bagController:BindUIEvent()
end 








return bagController