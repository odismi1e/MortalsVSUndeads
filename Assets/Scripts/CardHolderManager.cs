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
        SpawnCards = new GameObject[GameManager.Instance.OtherFields.NumberCardsHand];

        for (int i = 0; i < GameManager.Instance.OtherFields.NumberCardsHand; i++)
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
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Unit.CrossbowmanMana.ToString();
                cardManager.SetMana(GameManager.Instance.Unit.CrossbowmanMana);
                break;
            case (int)Cards.Swordsman:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.Unit.SwordsmanMana.ToString();
                cardManager.SetMana( GameManager.Instance.Unit.SwordsmanMana);
                break;
            case (int)Cards.Healing:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.EnhancementsCard.HealCardManaCost.ToString();
                cardManager.SetMana( GameManager.Instance.EnhancementsCard.HealCardManaCost);
                break;
            case (int)Cards.Rage:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.EnhancementsCard.RageCardManaCost.ToString();
                cardManager.SetMana( GameManager.Instance.EnhancementsCard.RageCardManaCost);
                break;
            case (int)Cards.FireExplosion:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.SpellsCard.FireExplosionManaCost.ToString();
                cardManager.SetMana( GameManager.Instance.SpellsCard.FireExplosionManaCost);
                break;
            case (int)Cards.IceBlast:
                card.GetComponentInChildren<TMP_Text>().text = GameManager.Instance.SpellsCard.IceBlastManaCost.ToString();
                cardManager.SetMana( GameManager.Instance.SpellsCard.IceBlastManaCost);
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
        for (int i = 0; i < GameManager.Instance.OtherFields.NumberCardsHand; i++)
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

