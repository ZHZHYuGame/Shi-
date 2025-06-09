local loginConfig = {
    uiId = "Bag", --唯一id

    controllerMod = require("UI/Bag/BagController"), -- C层模块

    proxyMod = require("UI/Bag/BagProxy"), --M层模块

    viewMod = require("UI/Bag/BagView"), --V层模块

    uiPrefab = "Bag", --预制体路径

    uiLayer = "WindowLayer"
}


return loginConfig