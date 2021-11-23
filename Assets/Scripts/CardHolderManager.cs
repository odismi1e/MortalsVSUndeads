using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardHolderManager : MonoBehaviour
{
    [SerializeField] private Transform _cardHolderPosition;
    [SerializeField] private GameObject _card;
    [SerializeField] private Card[] _cardSO;
    private int _cardsAmmount;
    private int _ind = 0;

    private Sprite _icon;

    public GameObject[] SpawnCards;

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
            case (int)Cards.Crossbowman:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.UnitManager.CrossbowmanMana.ToString();
                cardManager.Mana = GameManager.Instance.UnitManager.CrossbowmanMana;
                break;
            case (int)Cards.Swordsman:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.UnitManager.SwordsmanMana.ToString();
                cardManager.Mana = GameManager.Instance.UnitManager.SwordsmanMana;
                break;
            case (int)Cards.Healing:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.EnhancementsCardManager.HealCardManaCost.ToString();
                cardManager.Mana = GameManager.Instance.EnhancementsCardManager.HealCardManaCost;
                break;
            case (int)Cards.Rage:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.EnhancementsCardManager.RageCardManaCost.ToString();
                cardManager.Mana = GameManager.Instance.EnhancementsCardManager.RageCardManaCost;
                break;
            case (int)Cards.FieryExplosion:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.EnhancementsCardManager.FieryExplosionManaCost.ToString();
                cardManager.Mana = GameManager.Instance.EnhancementsCardManager.FieryExplosionManaCost;
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

