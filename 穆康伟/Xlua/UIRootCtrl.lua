UIRootCtrl = {};
CS = CS or {}
local this = UIRootCtrl;
local root;
local transform;
local gameObject;

function UIRootCtrl.New()
    print("主界面 启动了！")
    ---@diagnostic disable-next-line: undefined-global
    --CS.LuaHelper.Instance:OpenUI(nil);
    CS.LuaHelper.Instance:OpenUI(this.OnCreat);
end

function UIRootCtrl.OnCreat(obj)
    print("克隆完毕!");

    local taskBtn = UIRootView.TaskBtn;
    taskBtn.onClick:AddListener(this.onClickTaskBtn)
end
function UIRootCtrl.onClickTaskBtn()
    print("该按钮点击了!")
end