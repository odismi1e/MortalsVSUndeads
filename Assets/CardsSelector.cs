using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardsSelector : MonoBehaviour
{
    [SerializeField] private CardHolderManager CHM;
    [SerializeField] private Card CardSO;
    [SerializeField] private Total Total;
    [SerializeField] private TextMeshProUGUI TotalTMP;
    [SerializeField] private TextMeshProUGUI QuantityTMP;
    private int quantityCount = 0;
    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        
    }

    public void AddCardToDeck()
    {
        if (CHM._cardsList.Count < Total.MaxCards)
        {
            CHM.AddCard(CardSO);
            Total.TotalSumOfSelectedCards++;
            TotalTMP.text = "Total: " + Total.TotalSumOfSelectedCards;
            quantityCount++;
            QuantityTMP.text = quantityCount.ToString();
        }
    }
    public void RemoveCardFromDeck()
    {
        if (CHM._cardsList.Exists(element => element == CardSO))
        {
            CHM.RemoveCard(CardSO);
            Total.TotalSumOfSelectedCards--;
            TotalTMP.text = "Total: " + Total.TotalSumOfSelectedCards;
            quantityCount--;
            QuantityTMP.text = quantityCount.ToString();
        }
    }
}
