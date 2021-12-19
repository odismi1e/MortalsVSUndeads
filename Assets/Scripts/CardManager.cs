using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image[] images;
    private TMP_Text[] tMP_Text;
    private CardHolderManager _cardHolderManager;

    private GameObject _draggingBuilding;
    private GameObject _draggingCard;

    private float _heightGrid;
    private float _widthGrid;
    private int _horizontalCount;
    private int _verticalCount;
    private Vector2 _centreGrid;

    private bool _isAvailableToBuild;

    private bool MakeCardInvisible = true;

    private int xGrid;
    private int yGrid;

    private float _y1;
    private float _y2;

    public Card CardSO;
   // public GameObject CardHolder;

    private int _mana;

    public GameObject CardGrid;
    public Transform _cardHolderPosition;

    private void Awake()
    {
        if(GridController.Instance.Grid==null)
        {
            GridController.Instance.Grid = new GameObject[_verticalCount, _horizontalCount];
        }
        _heightGrid = GridController.Instance.HeightGrid;
        _widthGrid = GridController.Instance.WidthGrid;
        _horizontalCount = GridController.Instance.HorizontalCount;
        _verticalCount = GridController.Instance.VerticalCount;
        _centreGrid = GridController.Instance.CentreGrid;

        _cardHolderManager = GridController.Instance._cardHolderManager;
    }
    private void Start()
    {
        _y1 = gameObject.transform.localPosition.y*.75f;
        _y2 = 70;
        StartCoroutine(RefundCard());
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_draggingCard != null && CheckForDurability())
        {
            if (MakeCardInvisible)
            {
                SetCardInvisible(_cardHolderManager.SpawnCards[SelectedCard(gameObject, false)]);
                MakeCardInvisible = false;
            }
            ReturnTheSelectedCardsToTheDeck();

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10));
            float x = worldPosition.x;
            float y = worldPosition.y;

            xGrid = (int)(((worldPosition.x - (_centreGrid.x - _widthGrid / 2) + (_widthGrid / _verticalCount * 2))) / (_widthGrid / _verticalCount)) - 2;
            yGrid = (int)(((worldPosition.y - (_centreGrid.y - _heightGrid / 2) + (_heightGrid / _horizontalCount * 2))) / (_heightGrid / _horizontalCount)) - 2;

            CheckingTheGridBoundaries(x, y);
            LinkingTheCardToTheGrid(eventData);
            CheckingTheGridForEmployment();
            SetColor(_isAvailableToBuild);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CheckForDurability())
        {
            _draggingCard = Instantiate(CardGrid, Vector3.zero, Quaternion.identity);
            CreationCard(_draggingCard);
            _draggingCard.transform.localScale = new Vector3(.8f, .8f, 1);

            _draggingCard.transform.position = new Vector3(-10, 30, 0);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CheckForDurability())
        {
            int indexCard = SelectedCard(gameObject, false);
            if (!_isAvailableToBuild)
            {
                Destroy(_draggingCard);
                SetCardVisible(_cardHolderManager.SpawnCards[SelectedCard(gameObject, false)]);
                MakeCardInvisible = true;
                SelectedCard(gameObject);
            }
            else
            {
                _draggingBuilding = Instantiate(CardSO.Prefab, 
                    new Vector3(xGrid * (_widthGrid / _verticalCount) + (_centreGrid.x - _widthGrid / 2) + (_widthGrid / (_verticalCount * 2)),
                    yGrid * (_heightGrid / _horizontalCount) + (_centreGrid.y - _heightGrid / 2) + (_heightGrid / (_horizontalCount * 2)), 0),
                    Quaternion.identity);
                _draggingBuilding.transform.position = new Vector3(xGrid * (_widthGrid / _verticalCount) + (_centreGrid.x - _widthGrid / 2) + (_widthGrid / (_verticalCount * 2)), yGrid * (_heightGrid / _horizontalCount) + (_centreGrid.y - _heightGrid / 2) + (_heightGrid / (_horizontalCount * 2)), 0);
                if (CardSO.Unit)
                {
                    GridController.Instance.Grid[(int)(((_draggingBuilding.transform.position.x - (_centreGrid.x - _widthGrid / 2) + (_widthGrid / _verticalCount * 2))) / (_widthGrid / _verticalCount)) - 2,
                        (int)(((_draggingBuilding.transform.position.y - (_centreGrid.y - _heightGrid / 2) + (_heightGrid / _horizontalCount * 2))) / (_heightGrid / _horizontalCount)) - 2] = _draggingBuilding;
                }

                _cardHolderManager.SpawnCards[indexCard] = null;

                for (int i = 0; i < GameManager.Instance.NumberCardsHand; i++)
                {
                    if (_cardHolderManager.SpawnCards[i] != null)
                    {
                        _cardHolderManager.SpawnCards[i].GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
                    }
                }

                GridController.Instance._cardHolderManager.CreateCard(indexCard);

                ResourceCounter.Instance.WasteOfResources(_mana);

                Destroy(gameObject);
                Destroy(_draggingCard);

            }
        }
        _isAvailableToBuild = false;
    }

    private bool IsPlaceTaken(int x, int y)
    {
        if (GridController.Instance.Grid[x, y]!=null)
        {
            return true;
        }
        return false;
    }
    private int SelectedCard(GameObject card,bool ChooseCard = true)
    {
        int index =-1;
        if (ChooseCard)
        {
            for (int i = 0; i < GameManager.Instance.NumberCardsHand; i++)
            {
                if (_cardHolderManager.SpawnCards[i] == card)
                {
                    _cardHolderManager.SpawnCards[i].GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
                    _cardHolderManager.SpawnCards[i].transform.localPosition = new Vector3(_cardHolderManager.SpawnCards[i].transform.localPosition.x,
                       _y2, _cardHolderManager.SpawnCards[i].transform.localPosition.z);
                    //_cardHolderManager.SpawnCards[i].transform.localScale = new Vector3(1.5f,1.5f,1);
                    index  = i;
                }
                else
                {
                    if (_cardHolderManager.SpawnCards[i] != null)
                    {
                        _cardHolderManager.SpawnCards[i].GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
                        _cardHolderManager.SpawnCards[i].transform.localPosition = new Vector3(_cardHolderManager.SpawnCards[i].transform.localPosition.x,
                        _y1, _cardHolderManager.SpawnCards[i].transform.localPosition.z);
                        //_cardHolderManager.SpawnCards[i].transform.localScale = new Vector3(1f, 1f, 1);
                    }
                }
            }
            return index ;
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.NumberCardsHand; i++)
            {
                if (_cardHolderManager.SpawnCards[i] == card)
                {
                    index  = i;
                }
            }
            return index ;
        }
    }
    private void SetColor(bool isAvailableToBuild)
    {
        if (isAvailableToBuild)
        {
            _draggingCard.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.1f, 1, 0, .2f);
        }
        else if(CardSO.Unit)
        {
            _draggingCard.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1, 0, 0, .2f);
        }
        else
        {
            _draggingCard.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.1f, 1, 0, .2f);
        }           
    }
    private IEnumerator RefundCard()
    {
        if(_cardHolderManager.CheckForDurability(gameObject) != true)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
             _y1, gameObject.transform.localPosition.z);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(RefundCard());
    }
    public bool CheckForDurability()
    {
        if (_mana > ResourceCounter.Instance.Resources)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void CreationCard(GameObject gameObject)
    {
        gameObject.GetComponentInChildren<Image>().sprite = CardSO.Icon;
        gameObject.GetComponentInChildren<TMP_Text>().text = _mana.ToString();

    }
    private void SetCardInvisible(GameObject card)
    {
        images = card.GetComponents<Image>();
        tMP_Text = card.GetComponentsInChildren<TMP_Text>();
        for(int i=0;i<images.Length;i++)
        {
            images[i].enabled = false;
        }
        foreach(var item in tMP_Text)
        {
            item.enabled = false;
        }
    }
    private void SetCardVisible(GameObject card)
    {
        images = card.GetComponents<Image>();
        tMP_Text = card.GetComponentsInChildren<TMP_Text>();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = true;
        }
        foreach (var item in tMP_Text)
        {
            item.enabled = true;
        }
    }
    private void ReturnTheSelectedCardsToTheDeck()
    {
        for (int i = 0; i < GameManager.Instance.NumberCardsHand; i++)
        {
            if (_cardHolderManager.SpawnCards[i] != null)
            {
                _cardHolderManager.SpawnCards[i].GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
                _cardHolderManager.SpawnCards[i].transform.localPosition = new Vector3(_cardHolderManager.SpawnCards[i].transform.localPosition.x,
                    _y1,
                    _cardHolderManager.SpawnCards[i].transform.localPosition.z);
               // _cardHolderManager.SpawnCards[i].transform.localScale = new Vector3(1f, 1f, 1);
            }
        }
    }
    private void CheckingTheGridBoundaries(float x,float y)
    {
        if (x > (_centreGrid.x + _widthGrid / 2) || x < (_centreGrid.x - _widthGrid / 2))
        {
            _isAvailableToBuild = false;
        }
        else
        {
            if (y > (_centreGrid.y + _heightGrid / 2) || y < (_centreGrid.y - _heightGrid / 2))
            {
                _isAvailableToBuild = false;
            }
            else
            {
                _isAvailableToBuild = true;
            }
        }
    }
    private void LinkingTheCardToTheGrid(PointerEventData eventData)
    {
        if (xGrid >= 0 && xGrid <= _verticalCount - 1 && yGrid >= 0 && yGrid <= _horizontalCount - 1)
        {
            _draggingCard.transform.position = new Vector3(xGrid * (_widthGrid / _verticalCount) + (_centreGrid.x - _widthGrid / 2) + (_widthGrid / (_verticalCount * 2)), yGrid * (_heightGrid / _horizontalCount) + (_centreGrid.y - _heightGrid / 2) + (_heightGrid / (_horizontalCount * 2)), 0);
        }
        else
        {
            _draggingCard.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10));
        }
    }
    private void CheckingTheGridForEmployment()
    {
        if ( CardSO.Unit && _isAvailableToBuild && IsPlaceTaken((int)(((_draggingCard.transform.position.x - (_centreGrid.x - _widthGrid / 2) + (_widthGrid / _verticalCount * 2))) / (_widthGrid / _verticalCount)) - 2,
            (int)(((_draggingCard.transform.position.y - (_centreGrid.y - _heightGrid / 2) + (_heightGrid / _horizontalCount * 2))) / (_heightGrid / _horizontalCount)) - 2))
        {
            _isAvailableToBuild = false;
        }
    }
    public void SetMana(int mana)
    {
        _mana = mana;
    }
    public int GetMana()
    {
        return _mana;
    }
}
 