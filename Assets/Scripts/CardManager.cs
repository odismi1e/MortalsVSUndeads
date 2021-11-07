using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Card _cardSO;
    public int Mana;

    private Image[] images;
    private TMP_Text[] tMP_Text;

    public Card CardSO
    {
        get => _cardSO;
        set { _cardSO = value; }
    }
    private GameObject _cardHolder;
    public GameObject CardGrid;

    public GameObject CardHolder 
            { 
        get => _cardHolder;
        set { _cardHolder = value; }
    }
    private CardHolderManager _cardHolderManager;
    public Transform _cardHolderPosition;

    private GameObject _draggingBuilding;
    private GameObject _draggingCard;

  
     private float _heightGrid;
     private float _widthGrid;
     private int _horizontalCount;
     private int _verticalCount;
     private Vector2 _centreGrid;

    private bool _isAvailableToBuild;

    private int xGrid;
    private int yGrid;

    private float _y1;
    private float _y2;



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
        _y1 = CardHolder.transform.localPosition.y*.5f;
        _y2 = 50;
        StartCoroutine(RefundCard());
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_draggingBuilding != null && CheckForDurability())
        {
            //_cardHolderManager.SpawnCards[SelectedCard(CardHolder, 1)].;
            SetCardInactive(_cardHolderManager.SpawnCards[SelectedCard(CardHolder, 1)]);
            for (int i=0;i< GameManager.Instance.NumberCardsHand; i++)
            {
                if(_cardHolderManager.SpawnCards[i]!=null)
                {
                    _cardHolderManager.SpawnCards[i].GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
                    _cardHolderManager.SpawnCards[i].transform.localPosition = new Vector3(_cardHolderManager.SpawnCards[i].transform.localPosition.x,
                        _y1,
                        _cardHolderManager.SpawnCards[i].transform.localPosition.z);
                }
            }

                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10));
                float x = worldPosition.x;
                float y = worldPosition.y;

             xGrid = (int)(((worldPosition.x - (_centreGrid.x - _widthGrid / 2) + (_widthGrid / _verticalCount * 2))) / (_widthGrid / _verticalCount)) - 2;
                 yGrid= (int)(((worldPosition.y - (_centreGrid.y - _heightGrid / 2) + (_heightGrid / _horizontalCount * 2))) / (_heightGrid / _horizontalCount)) - 2;

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
            

            if (xGrid >= 0 && xGrid <= _verticalCount-1 && yGrid >= 0 && yGrid <= _horizontalCount-1)
            {
                _draggingCard.transform.position = new Vector3(xGrid*(_widthGrid/_verticalCount)+(_centreGrid.x-_widthGrid/2)+(_widthGrid / (_verticalCount*2)),yGrid*(_heightGrid/_horizontalCount)+(_centreGrid.y-_heightGrid/2)+(_heightGrid / (_horizontalCount*2)),0);
            }
            else
            {
                _draggingCard.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10));
            }

            if (_isAvailableToBuild && IsPlaceTaken((int)(((_draggingCard.transform.position.x - (_centreGrid.x - _widthGrid / 2) + (_widthGrid / _verticalCount * 2))) / (_widthGrid / _verticalCount)) - 2,
            (int)(((_draggingCard.transform.position.y - (_centreGrid.y - _heightGrid / 2) + (_heightGrid / _horizontalCount * 2))) / (_heightGrid / _horizontalCount)) - 2))
            {
                _isAvailableToBuild = false;
            }
            SetColor(_isAvailableToBuild);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CheckForDurability())
        {
            _draggingBuilding = Instantiate(_cardSO.Prefab, Vector3.zero, Quaternion.identity);
            _draggingCard = Instantiate(CardGrid, Vector3.zero, Quaternion.identity);
            CreationCard(_draggingCard);
            _draggingCard.transform.localScale = new Vector3(.6f, .6f, 1);


            _draggingBuilding.transform.position = new Vector3(-10, 30, 0);
            _draggingCard.transform.position = new Vector3(-10, 30, 0);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CheckForDurability())
        {
            int g = SelectedCard(CardHolder, 1);
            if (!_isAvailableToBuild)
            {
                Destroy(_draggingBuilding);
                Destroy(_draggingCard);
                SetCardActive(_cardHolderManager.SpawnCards[SelectedCard(CardHolder, 1)]);
                SelectedCard(CardHolder);
            }
            else
            {
                _draggingBuilding.transform.position = new Vector3(xGrid * (_widthGrid / _verticalCount) + (_centreGrid.x - _widthGrid / 2) + (_widthGrid / (_verticalCount * 2)), yGrid * (_heightGrid / _horizontalCount) + (_centreGrid.y - _heightGrid / 2) + (_heightGrid / (_horizontalCount * 2)), 0);

                GridController.Instance.Grid[(int)(((_draggingBuilding.transform.position.x - (_centreGrid.x - _widthGrid / 2) + (_widthGrid / _verticalCount * 2))) / (_widthGrid / _verticalCount)) - 2,
                    (int)(((_draggingBuilding.transform.position.y - (_centreGrid.y - _heightGrid / 2) + (_heightGrid / _horizontalCount * 2))) / (_heightGrid / _horizontalCount)) - 2] = _draggingBuilding;
                _draggingBuilding.GetComponent<Health>().Active = true;


                _cardHolderManager.SpawnCards[g] = null;
                for (int i = 0; i < GameManager.Instance.NumberCardsHand; i++)
                {
                    if (_cardHolderManager.SpawnCards[i] != null)
                    {
                        _cardHolderManager.SpawnCards[i].GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
                    }
                }
                GridController.Instance._cardHolderManager.CreateCard(g);

                ResourceCounter.Instance.WasteOfResources(Mana);

                Destroy(CardHolder);
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
    private int SelectedCard(GameObject card,int g=0)
    {
        int a=-1;
        if (g == 0)
        {
            for (int i = 0; i < GameManager.Instance.NumberCardsHand; i++)
            {
                if (_cardHolderManager.SpawnCards[i] == card)
                {
                    _cardHolderManager.SpawnCards[i].GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
                    _cardHolderManager.SpawnCards[i].transform.localPosition = new Vector3(_cardHolderManager.SpawnCards[i].transform.localPosition.x,
                       _y2
                       , _cardHolderManager.SpawnCards[i].transform.localPosition.z);
                    a = i;
                }
                else
                {
                    if (_cardHolderManager.SpawnCards[i] != null)
                    {
                        _cardHolderManager.SpawnCards[i].GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
                        _cardHolderManager.SpawnCards[i].transform.localPosition = new Vector3(_cardHolderManager.SpawnCards[i].transform.localPosition.x,
                        _y1, 
                        _cardHolderManager.SpawnCards[i].transform.localPosition.z);
                    }
                }
            }
            return a;
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.NumberCardsHand; i++)
            {
                if (_cardHolderManager.SpawnCards[i] == card)
                {
                    a = i;
                }
            }
            return a;
        }
    }
    private void SetColor(bool isAvailableToBuild)
    {
        if (isAvailableToBuild)
            _draggingCard.transform.GetChild(0).gameObject.GetComponent<Image>().color =new Color(0.1f,1,0,.2f);
        else
            _draggingCard.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(1, 0, 0, .2f);
    }
    private IEnumerator RefundCard()
    {
        if(_cardHolderManager.CheckForDurability(CardHolder) != true)
        {
            CardHolder.transform.localPosition = new Vector3(CardHolder.transform.localPosition.x,
             _y1,CardHolder.transform.localPosition.z);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(RefundCard());
    }
    public bool CheckForDurability()
    {
        if (Mana > ResourceCounter.Instance.Resources)
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
        gameObject.GetComponentInChildren<Image>().sprite = _cardSO.Icon;
        gameObject.GetComponentInChildren<TMP_Text>().text = Mana.ToString();

    }
    private void SetCardInactive(GameObject card)
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
    private void SetCardActive(GameObject card)
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
}
