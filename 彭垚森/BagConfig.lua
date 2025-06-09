local bagconfig={
    --背包id
    Id="bag",
    --背包面板层级
    Layer="UIWindowLayer",
    --背包面板C层脚本
    ControlCode=require("UI/Bag/BagControl"),
    --背包面板M层脚本
    ModelCode=require("UI/Bag/BagModel"),
    --背包面板V层脚本
    ViewCode=require("UI/Bag/BagView"),
    --背包面板预制体
    PrefabName="Bag"
}
return bagconfig