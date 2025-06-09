local bagConfig={
    --ID
    Id="bag",
    --层级
    Layer="UlWindowLayer",
    --获取脚本
    ControllCode=require("Bag/BagControll"),
    ModelCode=require("Bag/BagModel"),
    ViewCode=require("Bag/BagView"),
    
    PrefabName="Bag"
}
return bagConfig