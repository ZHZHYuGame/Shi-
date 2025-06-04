CS = CS or {}

local loginController = BaseClass("loginController")
loginController.name = "loginController"

local view,proxy

function  loginController:BindUIEvent()  --ui绑定事件
    view.btn_login.onClick:AddListener(function()
    loginController:OnclickLoginBtn(view.input_account.text,view.input_password.text)
    end)

end
function loginController:__init(_view,_proxy)
    view,proxy = _view,_proxy
    loginController:BindUIEvent()
end 


function loginController:OnclickLoginBtn(acc,pass)  --点击登录按钮事件
    if acc == "yabo666" and pass == "123456" then
        print("登录成功")
        UIManager.ani:SetBool("FadeIn",true)

        
        CS.LuaTest():LuaFuction(UIManager.ani)


        
    else
    print("账号或密码错误")
    end
end





return loginController