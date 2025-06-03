local UIBase = BaseClass("UIBase");

function UIBase:OpenUI(obj)
    self.gameObject = obj
    self.gameObject.SetActive(true);
end

function UIBase:CloseUI(obj)
    self.gameObject = obj
    self.gameObject.SetActive(false);
     print("已关闭"..self.gameObject.name)
end
return UIBase;