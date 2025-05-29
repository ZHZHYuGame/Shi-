CS=CS or {}
function LuaStart()
    print("进入LuaStart脚本");
end
function LuaUpdata()
    --print("进入LuaUpdata脚本");
end
local dic={}
dic[1]="q"
dic[2]="w"
dic[3]="e"
dic[5]="r"
for index, value in ipairs(dic) do
    print(" Key=",index," Value=",value);
end

UnityEngine=CS.UnityEngine;
Color=UnityEngine.Color;
MeshFilter=UnityEngine.MeshFilter;
MeshRenderer=UnityEngine.MeshRenderer;

Color=Color.Green;
CS.Test.TestFun1()

CS.Test.TestFun2(2,3)