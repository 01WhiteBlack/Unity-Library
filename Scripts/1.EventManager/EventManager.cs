using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 事件管理
/// </summary>
public class EventManager
{
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }

    private Dictionary<string, EventHandler> eventHandlerDic = new Dictionary<string, EventHandler>();

    //添加一个监听者
    public void AddListener(string eventName, EventHandler handler)
    {
        if (eventHandlerDic.ContainsKey(eventName))
        {
            eventHandlerDic[eventName] += handler;
        }
        else
        {
            eventHandlerDic.Add(eventName, handler);
        }
    }
    //移除一个监听者
    public void RemoveListener(string eventName, EventHandler eventHandler)
    {
        if (eventHandlerDic.ContainsKey(eventName))
        {
            eventHandlerDic[eventName] -= eventHandler;
        }
    }
    //触发无参数 事件
    public void TriggerEvent(string evnetName, object sender)
    {
        if (eventHandlerDic.ContainsKey(evnetName))
        {
            eventHandlerDic[evnetName]?.Invoke(sender, EventArgs.Empty);
        }
    }
    //触发有参数事件
    public void TriggerEvent(string evnetName, object sender, EventArgs eventArgs)
    {
        if (eventHandlerDic.ContainsKey(evnetName))
        {
            eventHandlerDic[evnetName]?.Invoke(sender, eventArgs);
        }
    }
    //清空所有事件
    public void ClearAllEvent()
    {
        eventHandlerDic.Clear();
    }
}
/// <summary>
/// 事件管理拓展类 便于触发事件
/// </summary>
public static class EventMangerExapnd
{
    public static void TriggerEvent(this object sender, string eventName)
    {
        EventManager.Instance.TriggerEvent(eventName, sender);
    }
    public static void TriggerEvent(this object sender, string eventName, EventArgs eventArgs)
    {
        EventManager.Instance.TriggerEvent(eventName, sender, eventArgs);
    }
}

public static class EventName
{
    public const string MainToLibraryView = nameof(MainToLibraryView);
}

/// <summary>
/// 自定义参数 作为函数参数使用
/// </summary>
public class CusontsEventArgs : EventArgs
{
    public string customsArgs;
}
