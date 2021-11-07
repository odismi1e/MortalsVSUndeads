using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    [Header("Mana recovery rate.")]
    public float Seconds;
    [Header("The amount of mana received.")]
    public int Mana;
    [Header("Initial amount of mana.")]
    public int StartMana;
    [Header("The number of cards in the hand.")]
    public int NumberCardsHand;
}
