--Lua控制器 作用就是注册所有的控制器
print("启动CtrlMgr.lua")

require("Common/Define");

require("Modules/UIRoot/UIRootCtrl");

CtrlMgr = {};
local this = CtrlMgr;
--控制器列表
local ctrlList = {};
--初始化 往列表中添加所有的控制器
function CtrlMgr.Init()
    ctrlList[CtrlName.UIRootCtrl] = UIRootCtrl.New();
    return this;
end
--根据控制器的名称 获取控制器
function CtrlMgr.GetCtrl(ctrlName)
    return ctrlList[ctrlName];
end

