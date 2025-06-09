local mainConfig = {
    uiId = "Main", --唯一id

    controllerMod = require("UI/Main/MainController"), -- C层模块

    proxyMod = require("UI/Main/MainProxy"), --M层模块

    viewMod = require("UI/Main/MainView"), --V层模块

    uiPrefab = "Main", --预制体路径

    uiLayer = "GameMenuLayer" --UI层级
}


return mainConfig