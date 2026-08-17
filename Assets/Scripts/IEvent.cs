using UnityEngine;

public interface IEvent : IReseteable
{
    void Set(params object[] data);
}
