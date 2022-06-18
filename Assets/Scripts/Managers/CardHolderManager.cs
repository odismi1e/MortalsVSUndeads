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
    public List<Card> _cardsList; // TEST
    private int _cardsAmmount;
    private int _ind = 0;

    private Sprite _icon;

    public GameObject[] SpawnCards;

    void Start()
    {
        _cardSO = new Card[_cardsList.Count];
        for (int i = 0; i < _cardsList.Count; i++)
        {
            _cardSO[i] = _cardsList[i];
        }

        _cardsAmmount = _cardSO.Length;
        SpawnCards = new GameObject[GameManager.Instance.Global.MaxNumCardsInHand];

        for (int i = 0; i < GameManager.Instance.Global.MaxNumCardsInHand; i++)
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
        //cardManager.CardHolder = card;
        cardManager._cardHolderPosition = card.transform;

        switch(_cardSO[_ind].id)
        {
            case (int)Cards.Crossbowman:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Units.CrossbowmanManaCost.ToString();
                cardManager.SetMana(GameManager.Instance.Units.CrossbowmanManaCost);
                break;
            case (int)Cards.Swordsman:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Units.SwordsmanManaCost.ToString();
                cardManager.SetMana( GameManager.Instance.Units.SwordsmanManaCost);
                break;
            case (int)Cards.Healing:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Buffs.HealCardManaCost.ToString();
                cardManager.SetMana( GameManager.Instance.Buffs.HealCardManaCost);
                break;
            case (int)Cards.Rage:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Buffs.RageCardManaCost.ToString();
                cardManager.SetMana( GameManager.Instance.Buffs.RageCardManaCost);
                break;
            case (int)Cards.FireExplosion:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Spells.FireExplosionManaCost.ToString();
                cardManager.SetMana( GameManager.Instance.Spells.FireExplosionManaCost);
                break;
            case (int)Cards.IceBlast:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Spells.IceBlastManaCost.ToString();
                cardManager.SetMana( GameManager.Instance.Spells.IceBlastManaCost);
                break;
        }

        card.GetComponent<Image>().sprite = _cardSO[_ind].Icon;

        SpawnCards[i] = card;
        _ind++;
    }
    public bool CheckForDurability(GameObject gameObject)
    {
        if (gameObject.GetComponent<CardManager>().GetMana() > ResourceCounter.Instance.Resources)
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
        for (int i = 0; i < GameManager.Instance.Global.MaxNumCardsInHand; i++)
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

    public void AddCard(Card CardSO) // TEST
    {
        _cardsList.Add(CardSO);
    }
    public void RemoveCard(Card CardSO) // TEST
    {
        _cardsList.Remove(CardSO);
    }
}

