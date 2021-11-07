using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] public float HeightGrid;
    [SerializeField] public float WidthGrid;
    [SerializeField] public int HorizontalCount;
    [SerializeField] public int VerticalCount;
    [SerializeField] public Vector2 CentreGrid;
    [SerializeField] private GameObject _gridTransform;


    public CardHolderManager _cardHolderManager;
    public GameObject CardHolder;
    private static GridController _instance;

    public static GridController Instance { get { return _instance; } }

    private GameObject[,] _grid;

    public GameObject[,] Grid
    {
        get => _grid;
        set
        {
            _grid = value;
        }
    }   

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this; 
    _grid= new GameObject[VerticalCount, HorizontalCount];
        GridTransform(_gridTransform);
    }

    public void GridTransform(GameObject gameObject)
    {
        HeightGrid = gameObject.transform.localScale.y*((float)Screen.currentResolution.height/(float)1080f);
        WidthGrid = gameObject.transform.localScale.x*((float)Screen.currentResolution.width/(float)1920f);
        CentreGrid = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * ((float)Screen.currentResolution.width / (float)1920f),
            gameObject.transform.localScale.y * ((float)Screen.currentResolution.height / (float)1080f));
    }
}
