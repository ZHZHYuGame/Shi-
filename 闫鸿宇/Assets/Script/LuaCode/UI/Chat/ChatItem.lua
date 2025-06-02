local ChatItem = BaseClass("ChatItem", UBase)

function ChatItem:__init( obj )
    --进入功能的预制件绑定
    self.gameObject = obj
end

return ChatItem