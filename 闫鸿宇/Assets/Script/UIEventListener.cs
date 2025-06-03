using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventListener : EventTrigger
{

    Dictionary<int, Action> dict = new Dictionary<int, Action>();

    public static UIEventListener AddObjEvent(GameObject obj)
    {
        UIEventListener u = obj.GetComponent<UIEventListener>();
        if (!u)
        {
            u = obj.AddComponent<UIEventListener>();
        }
        return u;
    }

    public void AddEventListener(int id, Action act)
    {
        if (!dict.ContainsKey(id))
        {
            dict.Add(id, act);
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (dict.ContainsKey((int)EventTriggerType.PointerClick))
        {
            dict[(int)EventTriggerType.PointerClick]?.Invoke();
        }
    }
    
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }
}
