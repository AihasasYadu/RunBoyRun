using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoSingletonGeneric<EventsManager>
{
    public delegate void Executable();
    public static event Executable Collided;
    public static event Executable PlayerDead;

    public void ObstacleCollision()
    {
        Collided?.Invoke();
    }

    public void PlayerDeathEvent()
    {
        PlayerDead?.Invoke();
    }
}