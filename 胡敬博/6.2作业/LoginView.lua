local loginView = BaseClass("loginView",UIBase)
loginView.name = "loginView"

local account = {}
account["yabo666"] = "123456"

local ani

function loginView:__init(obj)
    self.gameObject = obj
    self.input_account = obj.gameObject.transform:Find("Account"):GetComponent("InputField")
    self.input_password = obj.gameObject.transform:Find("Password"):GetComponent("InputField")
    self.btn_login = obj.gameObject.transform:Find("Button (Legacy)"):GetComponent("Button")
    ani = obj:GetComponent("Animator")

    self.btn_login.onClick:AddListener(function()
     loginView:function_name(self.input_account.text,self.input_password.text)
    end)

end

function loginView:CallBack()
    ani:SetBool("FadeOut",true)
    ani:SetBool("FadeIn",false)
end

 function loginView:function_name(acc,pass)
    if acc == "yabo666" and pass == account["yabo666"] then
        print("登录成功")
        ani:SetBool("FadeIn",true)
    
    else
    print("账号或密码错误")
    end
end



return loginView