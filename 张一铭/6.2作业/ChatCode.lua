local chatCode=BaseClass("chatCode",UIBase)

function chatCode:__init(obj)
    self.gameObject=obj
    self.input_Send=self.gameObject.transform:Find("Input_Send"):GetComponent("InputField")
    self.btnSend=self.gameObject.transform:Find("Button"):GetComponent("Button")

    self.btnSend.onClick:AddListener(function ()
        print("进行聊天")
    end)
end

return chatCode