using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    private class FunctionNotFound : Exception
    {
        public FunctionNotFound(string message) : base(message)
        {

        }
    }

    private Dictionary<Type, List<Delegate>> statesToInstance;

    public EventBus()
    {
        statesToInstance = new Dictionary<Type, List<Delegate>>();
    }

    public void Subscribe<T>(Delegate function) where T : IEvent
    {
        if (statesToInstance.TryGetValue(typeof(T), out var delegates))
        {
            delegates.Add(function);
        }
        else
        {
            List<Delegate> newList = new List<Delegate>();
            newList.Add(function);

            statesToInstance.Add(typeof(T), newList);
        }
    }

    public void Unsubscribe<T>(Delegate function)
    {
        try
        {
            if (statesToInstance.TryGetValue(typeof(T), out var delegates) && delegates.Count > 0)
            {
                delegates.Remove(function);
            }
            else
            {
                throw new FunctionNotFound("Tried to unsubscribe a non existing function");
            }
        }
        catch (FunctionNotFound exception)
        {
            Debug.LogError(exception.Message);
        }
    }

    public void Raise<T>(params object[] data) where T : IEvent, new()
    {
        if (statesToInstance.TryGetValue(typeof(T), out var delegates))
        {
            if (data.Length <= 0f)
            {
                foreach (Delegate delegatesIter in delegates)
                {
                    delegatesIter?.DynamicInvoke();
                }
            }
            else
            {
                T newEvent = ServiceLocator.Instance.GetService<CompositePool>().GetItemFromPool<T>();

                foreach (Delegate delegatesIter in delegates)
                {
                    delegatesIter?.DynamicInvoke(newEvent);
                }
            }
        }
    }

    public void Clear()
    {
        statesToInstance.Clear();
    }
}
