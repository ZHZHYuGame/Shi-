require("Common/head")
require("Common/BaseClass")
require("Common/LuaEventTtriggerType")
UIBase = require("UIBase")
require("UIEnum")
local uiMgr =  require("UIManager")
UIManager = uiMgr:New()
--  local player =  require("PlayerController")
--  player.New()

local player = require("Common/PlayerController")
function  Start() --模拟unity中生命周期函数——Start

   
end

function Update() -- 模拟unity中生命周期函数——Update
    --print("lua Update...")
        player:Update()
     
end

--ipairs 只要遍历的table中存在nil就会立刻结束循环
--pairs  会遍历table中所有不为nil的元素
UIManager:OpenUI(UIEnum.login)

function GameStart()
     --生成玩家
    local prefabPlayer = GameObject.Instantiate(Resources.Load("treasure_kingskull"))

    player.New(prefabPlayer)
    UIManager:CloseUI(UIEnum.login)
    UIManager:OpenUI(UIEnum.main)
end