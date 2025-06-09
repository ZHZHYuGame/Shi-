local player = BaseClass("playerCOntroller")
player.name = "playerCOntroller"

local entity,ani
function player:__init(obj)
    self.gameObject = obj;
    entity = obj
    ani = obj:GetComponent("Animator")
end

local v3 = {x=0.0,y=0.0,z=0.0}
function player:Update()
    local x = Input.GetAxis("Horizontal")
    local y = Input.GetAxis("Vertical")
    if x~=0 or y~=0 then
        v3.x=entity.transform.position.x+x
        v3.z = entity.transform.position.z+y
        entity.transform:LookAt(v3)
       entity.transform:Translate(Vector3.forward*Time.deltaTime*3)
       ani:SetBool("Move",true)
    else
        ani:SetBool("Move",false)
    end
end

return player