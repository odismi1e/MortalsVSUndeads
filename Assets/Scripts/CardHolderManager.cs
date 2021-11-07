using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardHolderManager : MonoBehaviour
{
    [Header("Card Holder Parameters")]
    [SerializeField] private Transform _cardHolderPosition;
    [SerializeField] private GameObject _card;
    [SerializeField] private Card[] _cardSO;
    private int _cardsAmmount;
    private int _ind = 0;

    [Header("Card Parameters")]
    [SerializeField] public GameObject[] SpawnCards;
    private Sprite _icon;

    void Start()
    {
        _cardsAmmount = _cardSO.Length;
        SpawnCards = new GameObject[GameManager.Instance.NumberCardsHand];

        for (int i = 0; i < GameManager.Instance.NumberCardsHand; i++)
        {
            CreateCard(i);
        }
        StartCoroutine(CardsCheckForDurability());
    }

    public void CreateCard(int i=0)
    {
        if(_ind==_cardsAmmount)
        {
            return;
        }
        var card = Instantiate(_card, _cardHolderPosition);
        CardManager cardManager = card.GetComponent<CardManager>();

        cardManager.CardSO = _cardSO[_ind];
        cardManager.CardHolder = card;
        cardManager._cardHolderPosition = card.transform;

        switch(_cardSO[_ind].id)
        {
            case (int)Unit.Crossbowman:
                card.GetComponentInChildren<TMP_Text>().text = UnitManager.Instance.CrossbowmanMana.ToString();
                cardManager.Mana = UnitManager.Instance.CrossbowmanMana;
                break;
            case (int)Unit.Swordsman:
                card.GetComponentInChildren<TMP_Text>().text = UnitManager.Instance.SwordsmanMana.ToString();
                cardManager.Mana = UnitManager.Instance.SwordsmanMana;
                break;
        }

        card.GetComponent<Image>().sprite = _cardSO[_ind].Icon;

        SpawnCards[i] = card;
        _ind++;
    }
    public bool CheckForDurability(GameObject gameObject)
    {
        if (gameObject.GetComponent<CardManager>().Mana > ResourceCounter.Instance.Resources)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private IEnumerator CardsCheckForDurability()
    {
        for (int i = 0; i < GameManager.Instance.NumberCardsHand; i++)
        {
            if (SpawnCards[i] != null)
            {
                Image image = SpawnCards[i].GetComponentInChildren<Image>();
                if (CheckForDurability(SpawnCards[i]) != true)
                {
                    image.color = new Color(.43f, .52f, .2f, image.color.a);
                }
                else
                {
                    image.color = new Color(1, 1, 1, image.color.a);
                }
            }
        }
        yield return new WaitForSeconds(.5f);
        StartCoroutine(CardsCheckForDurability());
    }
}

