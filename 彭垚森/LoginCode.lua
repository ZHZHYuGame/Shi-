local loginCode=BaseClass("loginCode",UIBase)

function loginCode:__init(obj)
    --进入功能的预制件绑定
    self.gameObject=obj;
    self.input_Account=self.gameObject.transform:Find("InputAccount"):GetComponent("InputField")
    self.input_Pwd=self.gameObject.transform:Find("InputPwd"):GetComponent("InputField")
    self.btn_GoGame=self.gameObject.transform:Find("Sure"):GetComponent("Button")
    self.btn_GoGame.onClick:AddListener(
        function ()
            print("按钮")
            uiManager:OpenUI(uiEnum.Chat)
        end
    )
end
return loginCode