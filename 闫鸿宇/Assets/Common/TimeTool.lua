timeTool = {}
--延时函数管理器
timeTool.InvokeList = {}
--循环函数管理器
timeTool.InvokeRepeatingList = {}

--延时函数：N秒后执行
--handle:回调方法
--delayTime:延时时间
function timeTool:Invoke(handle, delayTime)
    local invokeTimeData = {}
    --这个函数的开始计时时间
    invokeTimeData.startTime = DateTime.Now.Ticks
    --延时时间
    invokeTimeData.delayTime = delayTime
    --执行效果函数
    invokeTimeData.handle = handle

    table.insert( timeTool.InvokeList , invokeTimeData ) 
end

--定时循环函数
--handle:回调方法
--delayTime:延时时间
--key:功能类型
function timeTool:InvokeRepeating(handle, delayTime, key)
    local invokeTimeData = {}
    --这个函数的开始计时时间
    invokeTimeData.startTime = DateTime.Now.Ticks
    --延时时间
    invokeTimeData.delayTime = delayTime
    --执行效果函数
    invokeTimeData.handle = handle

    invokeTimeData.key = key

    table.insert(timeTool.InvokeRepeatingList, invokeTimeData)
end

--定时循环函数，多少次结束
--handle:回调方法
--delayTime:延时时间
--key:功能类型
--count:多少次后停止
function timeTool:InvokeRepeatings(handle, delayTime, key, count)
    local invokeTimeData = {}
    invokeTimeData.startTime = DateTime.Now.Ticks
    invokeTimeData.delayTime = delayTime
    invokeTimeData.handle = handle
    invokeTimeData.key = key
    invokeTimeData.count = count
    invokeTimeData.countCurr = 0

    table.insert(timeTool.InvokeRepeatingList, invokeTimeData)
end

--停止一个时间函数运行
--handle:回调方法
function timeTool:RemoveInvoke(handle)
    for index, value in ipairs(timeTool.InvokeRepeatingList) do
        if value.handle == handle then
            table.remove( timeTool.InvokeRepeatingList, index )
            break;
        end
    end
end

--停止一个时间函数运行
--key:功能类型
function timeTool:RemoveInvoke(key)
    for index, value in ipairs(timeTool.InvokeRepeatingList) do
        if value.key == key then
            table.remove( timeTool.InvokeRepeatingList, index )
            break;
        end
    end
end

--做所有跟时间记录有关的计时计算
function timeTool:Updata()

    for index, value in ipairs(timeTool.InvokeList) do
        --当前时间
        local currTime = DateTime.Now.Ticks
        --通过时间戳间的差值并换算后，得到相隔时间
        local delay = (currTime - value.startTime)/10000000
        
        --当相隔时间达到这个函数的延时时间要求时
        if delay >= value.delayTime then
            --执行回调方法
            value.handle()
            --移除这个时间函数
            table.remove(timeTool.InvokeList,index)
            --置空释放掉这个函数
            value = nil
        end
    end
    for index, value in ipairs(timeTool.InvokeRepeatingList) do
        --当前时间
        local currTime = DateTime.Now.Ticks
        --通过时间戳间的差值并换算后，得到相隔时间
        local delay = (currTime - value.startTime)/10000000
        --当相隔时间达到这个函数的延时时间要求时
        if delay >= value.delayTime then
            --判断是否循环的函数有执行次数的设置
            if value.count ~= nil then
                --次数加1
                value.countCurr = value.countCurr + 1
                --判断次数是否达到要求，达到移除这个时间函数
                if value.countCurr > value.count then
                    table.remove(timeTool.InvokeRepeatingList, index)
                else
                    --执行回调方法
                    value.handle()
                    value.startTime = DateTime.Now.Ticks
                end
            else
                --执行回调方法
                value.handle()
                value.startTime = DateTime.Now.Ticks
            end
        end
    end
end
