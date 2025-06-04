local playerMove=BaseClass("playerMove")
local player;

function LuaStart()
    player=CS.UnityEngine.GameObject.Find("Prefabs_Cube(Clone)")
    UIEventListener.AddObject(player):AddEventListen(LuaEventTriggerType.PointerClick,function ()
        print("点到了")
    end)
    UIEventListener.AddObject(player):AddEventListen(LuaEventTriggerType.Select,function ()
        print("碰到了")
end)
end
function LuaUpdata()
    if player~=nil then
        local h=CS.UnityEngine.Input.GetAxis("Horizontal");
        local v=CS.UnityEngine.Input.GetAxis("Vertical");
    -- if h~=nil or v~=nil then
    --     player.transform.Translate(CS.UnityEngine.Vector3.forward*5*CS.UnityEngine.Time.deltaTime);
    --     player.transform.LookAt(player.transform.position+CS.UnityEngine.Vector3(h,0,v));
    -- end
        player.transform.position=player.transform.position+CS.UnityEngine.Vector3(h,0,v)*7*CS.UnityEngine.Time.deltaTime
    end
    
    
end