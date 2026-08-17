using System;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] private int id;

    private EventBus eventBus;

    private void Start()
    {
        eventBus = ServiceLocator.Instance.GetService<EventBus>();

        eventBus.Subscribe<Events.OnPlayerJump>((Action<Events.OnPlayerJump>)HandlePlayerJump);
        eventBus.Subscribe<Events.OnPlayerMove>((Action)HandlePlayerMove);
    }

    private void HandlePlayerJump(Events.OnPlayerJump onPlayerJumpData)
    {
        Debug.Log("Tester id: " + id);
        Debug.Log("Player just jumped with a jump force of " + onPlayerJumpData.jumpImpulse);
    }

    private void HandlePlayerMove()
    {
        Debug.Log("Player just moved");
    }

    private void OnDestroy()
    {
        eventBus.Unsubscribe<Events.OnPlayerJump>((Action<Events.OnPlayerJump>)HandlePlayerJump);
        eventBus.Unsubscribe<Events.OnPlayerMove>((Action)HandlePlayerMove);
    }
}
