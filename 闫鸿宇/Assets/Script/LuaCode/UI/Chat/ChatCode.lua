local chatCode = BaseClass("chatCode", UBase)

function chatCode:__init( obj )
    --进入功能的预制件绑定
    self.gameObject = obj
    self.scrollview=self.gameObject.transform:Find("ChatView"):GetComponent("ScrollRect");
    self.input_Chat=self.gameObject.transform:Find("Input_Chat"):GetComponent("InputField");
    self.content=self.gameObject.transform:Find("ChatView/Viewport/Content");
    self.btn_send=self.gameObject.transform:Find("Btn_Send"):GetComponent("Button");
    self.input_Chat_Text=self.gameObject.transform:Find("Input_Chat/Text"):GetComponent("Text");
    self.btn_send.onClick:AddListener(function ()
        if self.input_Chat_Text:GetComponent("Text").text ~=""then
           uiManager:OpenUI(uiEnum.ChatItem).transform:SetParent(self.content);
        end
    end)
end

return chatCode