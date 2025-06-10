local BagData = {}

function BagData.new(name, iconPath)
    local instance =
    {
        name = name,
        iconPath = iconPath
    }
    return instance
end

return BagData

