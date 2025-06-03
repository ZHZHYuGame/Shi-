UIRootView = {};

local this = UIRootView;

local transform;
local gameObject;
function UIRootView.Awake(obj)
    gameObject = obj;
    transform = obj.transform;
    this.InitView();
end

function UIRootView.InitView()
    --找UI组件
    print("进入" .. "UIRootView.InitView");

    this.TaskBtn=transform:Find("TaskBtn"):GetComponent("UnityEngine.UI.Button");

end

function UIRootView.Start()
    print("进入Start");
end
function UIRootView.Update()
 print("进入Update");
end
function UIRootView.OnDestroy()
 print("进入OnDestroy");
end