using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter");

        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            EventsManager.InvokeOnEnemyBorderPassed();
        }
        else
        {
            return;
        }
    }
}
