--[[ 
       main入口
]]
function LuaStart() 
      print("lua 开始");
        -- 获取主相机（缓存以提高性能）
    --self.mainCamera = CS.UnityEngine.Camera.main
    -- 默认射线长度
    --self.defaultDistance = 100
end
 local moveComponent = CS.UnityEngine.GameObject.Find('Player'):GetComponent('MoveComponent')
function LuaUpdate()
      print("lua 更新");
    local h = CS.UnityEngine.Input.GetAxis("Horizontal")
    local v = CS.UnityEngine.Input.GetAxis("Vertical")
    moveComponent:Move(h, v)

    --local hit = CS.UnityEngine.RaycastHit()
    --local hasHit = CS.RaycastHelper.ScreenPointRaycast(self.mainCamera, screenPos, hit)
    
    --if hasHit then
        --return {
            --collider = hit.collider,
            --point = hit.point,
            --normal = hit.normal,
            --distance = hit.distance,
            --transform = hit.transform,
            --gameObject = hit.collider.gameObject
            
            
        --}
    --end
    --print("当前点击的位置：", point);
end
function ShowMsg(msg)
    print("lua 显示消息: ",  msg);
end
ShowMsg("张一铭是男娘");
function ShowFunc(func,msg)
    func(msg);
end
ShowFunc(ShowMsg,"李国伟是战神");