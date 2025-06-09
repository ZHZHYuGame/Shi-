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
    public void AddEventListener(int id, Action action)
    {
        if (!dict.ContainsKey(id))
        {
            dict.Add(id, action);
        }

    }

    public override void OnPointerClick(PointerEventData eventData)
    {

        if (dict.ContainsKey((int)EventTriggerType.PointerClick))
        {
            dict[(int)EventTriggerType.PointerClick]?.Invoke();
        }
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (dict.ContainsKey((int)EventTriggerType.BeginDrag))
        {
            dict[(int)EventTriggerType.BeginDrag]?.Invoke();
        }
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (dict.ContainsKey((int)EventTriggerType.Drag))
        {
            dict[(int)EventTriggerType.Drag]?.Invoke();
        }
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (dict.ContainsKey((int)EventTriggerType.EndDrag))
        {
            dict[(int)EventTriggerType.EndDrag]?.Invoke();
        }
    }
}
