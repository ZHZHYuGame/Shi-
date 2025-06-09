local loginView = BaseClass("loginView",UIBase)
loginView.name = "loginView"

local account = {}
account["yabo666"] = "C7D5E4"

function loginView:__init(obj)
    -- loginView.New(obj)
    self.prefab = obj
    self.input_account = obj.gameObject.transform:Find("Account"):GetComponent("InputField")
    self.input_password = obj.gameObject.transform:Find("Password"):GetComponent("InputField")
    self.btn_login = obj.gameObject.transform:Find("Button (Legacy)"):GetComponent("Button")
    -- self.btn_login.onClick:AddListener(function()
    --  loginView:function_name(self.input_account.text,self.input_password.text)
    -- end)

    --UnityEngine:DontDestroyOnLoad(GameObject.Find("Canvas"))

end

return loginView
 