
local loginCode = BaseClass("loginCode", UBase)

function loginCode:__init( obj )
    --进入功能的预制件绑定
    self.gameObject = obj
    self.input_Account = self.gameObject.transform:Find("Input_Account"):GetComponent("InputField");
    self.btn_GoGame = self.gameObject.transform:Find("Button"):GetComponent("Button")
    print("self.btn_GoGame = ", self.btn_GoGame)
    self.btn_GoGame.onClick:AddListener(function()
        print("点击进入游戏按钮。。。。。。。。。。。")
    end)
end

return loginCode