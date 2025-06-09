--背包功能数据配置，来动态驱动整个功能的显示初始化（除面板界面的动态数据显示外）
local bagConfig={
    --背包ID
    Id="bag",
    --背包面板所在显示层级
    Layer="",
    --背包面板C层脚本
    ControllCode=require("UI/Bag/BagControll"),
    --背包面板M层脚本
    ModelCode=require("UI/Bag/BagModel"),
    --背包面板V层脚本
    ViewCode=require("UI/Bag/BagView"),
    --背包面板预制件
    PrefabName="Bag"

}
return bagConfig