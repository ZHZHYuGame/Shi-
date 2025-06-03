print("启动GameInit.lua");

require("CtrlMgr")

GameInit = {};
local this = GameInit;
function GameInit.InitViews()
    for i = 1, #ViewName do
        require("Modules/UIRoot/"..tostring(ViewName[i]));
    end
end

function GameInit.Init()
    this.InitViews();
    CtrlMgr.Init();
    GameInit.LoadView(CtrlName.UIRootCtrl);
end
function GameInit.LoadView(type)
    local ctrl = CtrlMgr.GetCtrl(type);
    if(ctrl~=nil)then
        ctrl.Awake();
    end
end

