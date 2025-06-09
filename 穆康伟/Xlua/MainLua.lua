CS = CS or {}
require("Common/head")
require("Common/BaseClass")
require("Common/LuaEventTriggerType")
require("Farmework/UIEnum")
require("Farmework/UIBase")
local uiMgr = require("Farmework/UIManager")
print(uiMgr);
uiManager = uiMgr.New()
local Player=GameObject.Find("Player")
local NPC=GameObject.Find("NPC")

function Start()
    --uiManager:OpenUI(UIEnum.Main);
    print(Player.name);
end
function UpDate()
    h = Input.GetAxis("Horizontal");
    v = Input.GetAxis("Vertical");
    if h > 0 then
        Player.gameObject.transform.position = Player.gameObject.transform.position + Vector3(1, 0, 0) * Time.deltaTime *
            5;
    elseif h < 0 then
        Player.gameObject.transform.position = Player.gameObject.transform.position +
            Vector3(-1, 0, 0) * Time.deltaTime *
            5;
    elseif v > 0 then
        Player.gameObject.transform.position = Player.gameObject.transform.position + Vector3(0, 0, 1) * Time.deltaTime *
            5;
    elseif v < 0 then
        Player.gameObject.transform.position = Player.gameObject.transform.position +
            Vector3(0, 0, -1) * Time.deltaTime *
            5;
    end
     local ray = Camera.main:ScreenPointToRay(Input.mousePosition);
      Debug.Log(ray);
    CS.TestLua:CallCSharp(ray);
end
function OnClickNPC()
    GameObject.Instantiate(Resources.Load("NPCTips"), GameObject.Find("Canvas").transform);
end