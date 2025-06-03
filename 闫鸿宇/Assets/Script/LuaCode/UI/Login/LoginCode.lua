
local loginCode = BaseClass("loginCode", UBase)

function loginCode:__init( obj )
    --进入功能的预制件绑定
    self.gameObject = obj
    self.input_Account = self.gameObject.transform:Find("Input_Account"):GetComponent("InputField");
    self.input_Account_Text=self.gameObject.transform:Find("Input_Account/Text_Account"):GetComponent("Text");
    self.input_Password = self.gameObject.transform:Find("Input_Password"):GetComponent("InputField");
    self.input_Password_Text=self.gameObject.transform:Find("Input_Password/Text_Password"):GetComponent("Text");
    self.btn_GoGame = self.gameObject.transform:Find("Button"):GetComponent("Button")
    self.btn_GoGame.onClick:AddListener(function()
        if self.input_Account_Text:GetComponent("Text").text =="123456" and self.input_Password_Text:GetComponent("Text").text =="123456" then
            self.gameObject:SetActive(false);
            uiManager:OpenUI(uiEnum.Chat);
        end
    end)
end

return loginCode