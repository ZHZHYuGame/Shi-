local chatCode=BaseClass("chatCode",UIBase)

function chatCode:__init(Obj)
    self.gameObject=Obj;
    print("进入聊天")
end
return chatCode