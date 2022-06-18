using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static readonly UnityEvent OnEnemyBorderPassed = new UnityEvent();

    public static void InvokeOnEnemyBorderPassed()
    {
        OnEnemyBorderPassed?.Invoke();
        Debug.Log("OnEvent");
    }
}
