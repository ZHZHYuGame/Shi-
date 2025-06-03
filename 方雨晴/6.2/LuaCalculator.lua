-- 引入C#命名空间
CS = CS
Calculator = CS.Calculator  -- 引用C#的Calculator类

-- 初始化计算器实例
local calculator = Calculator()

-- 执行计算
function Calculate(operation, a, b)
    if operation == '+' then
        return calculator:Add(a, b)
    elseif operation == '-' then
        return calculator:Subtract(a, b)
    elseif operation == '*' then
        return calculator:Multiply(a, b)
    elseif operation == '/' then
        return calculator:Divide(a, b)
    else
        error('未知操作符: ' .. operation)
    end
end

-- 格式化计算结果
function FormatResult(op, a, b, result)
    return string.format('{0} {1} {2} = {3}', a, op, b, result)
end

-- 导出公共接口
return {
    Calculate = Calculate,
    FormatResult = FormatResult
}