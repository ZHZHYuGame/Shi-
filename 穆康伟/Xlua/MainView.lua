local MainView = BaseClass("MainView", UIBase)
uiMgr = require("Farmework/UIManager")
uiManager = uiMgr.New()
function MainView:__init(obj)
    self.gameObject = obj
    self.LoginBtn = self.gameObject.transform:Find("LoginBtn"):GetComponent("Button");
    self.ChatBtn = self.gameObject.transform:Find("ChatBtn"):GetComponent("Button");
    self.LoginBtn.onClick:AddListener(function()
        uiManager:OpenUI(UIEnum.Login);
    end)
    self.ChatBtn.onClick:AddListener(function()
         uiManager:OpenUI(UIEnum.Chat);
    end)
end
return MainView