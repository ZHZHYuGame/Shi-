
local loginCode=BaseClass("loginCode",UIBase)

function loginCode:__init(obj)
    --进入功能的预制件绑定
    self.gameObject=obj
    self.input_Account=self.gameObject.transform:Find("Input_Account"):GetComponent("InputField")
    self.btn_GoGame=self.gameObject.transform:Find("Button"):GetComponent("Button")
    
    self.btn_GoGame.onClick:AddListener(function ()
        uiManager:OpenUI(uiEnum.Chat)
        print("点击进入游戏按钮")
    end)
end


return loginCode