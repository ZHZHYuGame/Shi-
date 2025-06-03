local luaManager = CS.LuaMgr

-- 模拟数据库存储
local userDatabase = {
    -- [username] = password
    admin = "123456"
}

-- 注册函数
function Register(username, password)
    if userDatabase[username] then
        print("用户名已存在")
        return false
    end
    
    if string.len(password) < 6 then
        print("密码长度不能少于6位")
        return false
    end
    
    userDatabase[username] = password
    luaManager.RegisterNewUser(username, password)
    print("注册成功")
    return true
end

-- 登录函数
function Login(username, password)
    if not userDatabase[username] then
        print("用户名不存在")
        return false
    end
    
    if userDatabase[username] ~= password then
        print("密码错误")
        return false
    end
    
    local success = luaManager.ValidateLogin(username, password)
    if success then
        print("登录成功")
        return true
    else
        print("登录验证失败")
        return false
    end
end