print("这是一个Lua");
a = { "1", "2", 3, 4, "穆康伟" };

--[[for i, v in ipairs(a) do
    print(i, v);
end--]]

_G[1] = false;
print(_G[1]);
function TestNumber(t)
    return t * 2;
end
print("得到的值为:"..TestNumber(10));

function TestString(str)
    -- return string.len(str);
    -- return string.lower(str);
    -- return string.upper(str);
    -- return #str;
    --return string.find(str,"C");
    -- return string.format("你好%s,谢谢%s",str,str);
    return string.rep(str,5);
end
--print("字符串的长度为：" .. TestString("穆康伟"));
-- print("字符串的小写为：" .. TestString("AbC"));
-- print("字符串的大写为："..TestString("AbC"));
--print("找到的字符串C的位置为："..TestString("AbC"));
print(TestString("穆康伟"));
function TestDoLoop()
    for i = 10, 1, -2 do
        print(i);
    end
end
TestDoLoop();

function TestTable()
    local myTable = {};
    myTable[#myTable + 1] = "你好";
    myTable[#myTable + 1] = "穆康伟";
    myTable[#myTable + 1] = "你真帅";
    table.remove(myTable, 1);
    table.insert(myTable,1,"太好了");
    print(table.concat(myTable, "|"));
end
TestTable();





