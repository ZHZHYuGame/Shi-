--背包功能数据配置，来动态驱动整个功能显示初始化（除了面板界面的动态数据显示外）
local BagConfig={
    --背包id
    Id="Bag",
    --背包面板层级
    Layer="UIWindowLayer",
    --背包面板C层脚本
    ControllCode=require("UI/Bag/BagControll"),
    --背包面板M层脚本
    ModelCode=require("UI/Bag/BagModel"),
    --背包面板V层脚本
    ViewCode=require("UI/Bag/BagView"),
    --背包面板预制体
    PrefabName="Bag"
}
return BagConfig