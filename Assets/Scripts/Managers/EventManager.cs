using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EventManager
{
    public delegate void EventReceiver(params object[] parameter);

    static Dictionary<string, EventReceiver> _events = new Dictionary<string, EventReceiver>();

    public static void Subscribe(string eventType, EventReceiver method)
    {
        if (!_events.ContainsKey(eventType))
            _events.Add(eventType, method);
        else
            _events[eventType] += method;
    }

    public static void UnSubscribe(string eventType, EventReceiver method)
    {
        if (_events.ContainsKey(eventType))
        {
            _events[eventType] -= method;

            if (_events[eventType] == null)
                _events.Remove(eventType);
        }
    }

    public static void Trigger(string eventType, params object[] parameters)
    {
        if (_events.ContainsKey(eventType))
            _events[eventType](parameters);
    }

    public static void ResetEventDictionary()
    {
        _events = new Dictionary<string, EventReceiver>();
    }
}

