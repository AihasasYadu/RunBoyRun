using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoSingletonGeneric<EventsManager>
{
    public delegate void Executable();
    public static event Executable Collided;
    public static event Executable CoinCollected;
    public static event Executable TogglePause;
    public static event Executable PlayerDead;
    public static event Executable ResetGame;

    private void Start()
    {
        EventsManager.ResetGame += DestroyGameObject;
    }

    public void ObstacleCollision()
    {
        Collided?.Invoke();
    }

    public void UpdateScore()
    {
        CoinCollected?.Invoke();
    }

    public void PlayerDeathEvent()
    {
        PlayerDead?.Invoke();
    }

    public void ToggleGamePause()
    {
        TogglePause?.Invoke();
    }

    public void RestartGame()
    {
        ResetGame?.Invoke();
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ResetGame -= DestroyGameObject;
    }
}