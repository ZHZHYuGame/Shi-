PlayerDataManager={}

function PlayerDataManager:__init(...)
    local par={...}
    self.BagList={}
    self.PlayerEquipList={}
    self.YplList={}
    self.SkillList={}
    self.MailList={}
end

function PlayerDataManager:SetBagData(list)
    self.BagList=list
end
function PlayerDataManager:AddBagData(gData)
    table.insert(self.BagList,gData)
end
function PlayerDataManager:DelBagData(gridId)
    for index, value in ipairs(self.BagList) do
        if index==gridId then
            table.remove(self.BagList,gridId)
        end
    end
    --table.remove(self.BagList,gData)
end
function PlayerDataManager:GetBagData()
    return self.BagList
end
function PlayerDataManager:SetPlayerEquipList(list)
    self.PlayerEquipList=list
end
function PlayerDataManager:GetPlayerEquipList()
    return self.PlayerEquipList
end
