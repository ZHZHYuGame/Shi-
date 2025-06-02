local LoginView=BaseClass("LoginView",UIBase)
local uiMgr = require("Farmework/UIManager")
-- uiManager = uiMgr.New()
function LoginView:__init(obj)
    print("进入登录界面")
    self.gameObject = obj
    self.closeBtn= self.gameObject.transform:Find("closeBtn"):GetComponent("Button");
    self.closeBtn.onClick:AddListener(function()
        uiMgr:CloseUI(UIEnum.Login);
    end)
    
end
return LoginView;