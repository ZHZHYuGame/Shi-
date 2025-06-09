local playerMove = BaseClass("PlayerMove")  

local player=nil;
function LuaStart()
    player=CS.UnityEngine.GameObject.FindWithTag("Player")
    if player~=nil then
        print("找到玩家")
    end

    for i = 0, 10 do
        local sphere=CS.UnityEngine.GameObject.CreatePrimitive(CS.UnityEngine.PrimitiveType.Sphere)
        local pos=CS.UnityEngine.Random.insideUnitCircle*10
        sphere.transform.position=CS.UnityEngine.Vector3(pos.x,0,pos.y)
        sphere:GetComponent(typeof(CS.UnityEngine.SphereCollider)).isTrigger=true
        AddClickListeren(sphere)
    end
end
function AddClickListeren(sphere)
    UIEventListener.AddObjEvent(sphere):AddEventListeren(LuaEventTriggerType.PointerClick,function ()
        print("点击了球")
        sphere:GetComponent(typeof(CS.UnityEngine.Renderer)).material.color=CS.UnityEngine.Random.ColorHSV()
    end)
end

function LuaUptade()
    if player~=nil then
        local speed=5
        local inputX=CS.UnityEngine.Input.GetAxis("Horizontal")
        local inputY=CS.UnityEngine.Input.GetAxis("Vertical")
        player.transform.position=player.transform.position+CS.UnityEngine.Vector3(inputX,0,inputY)*speed*CS.UnityEngine.Time.deltaTime
    end
end

function playerMove:TiggerObject()
end


return playerMove   