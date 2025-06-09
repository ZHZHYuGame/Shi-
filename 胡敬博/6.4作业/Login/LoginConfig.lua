local loginConfig = {
    uiId = "Login", --唯一id

    controllerMod = require("UI/Login/LoginController"), -- C层模块

    proxyMod = require("UI/Login/LoginProxy"), --M层模块

    viewMod = require("UI/Login/LoginView"), --V层模块

    uiPrefab = "Login", --预制体路径

    uiLayer = "WindowLayer"
}


return loginConfig