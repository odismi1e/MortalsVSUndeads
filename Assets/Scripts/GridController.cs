using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameObject _gridTransform;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private GameObject _spawner;

    private float _scaleSpawner;

    public static GridController Instance;

    public float HeightGrid;
    public float WidthGrid;
    public int HorizontalCount;
    public int VerticalCount;
    public Vector2 CentreGrid;

    public GameObject[,] Grid;

    public CardHolderManager _cardHolderManager;

    public RectTransform RectTransform;

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
        Grid= new GameObject[VerticalCount, HorizontalCount];
        GridTransform(_gridTransform);
    }
    private void Start()
    {
        GameController.Instance.MagnificationFactor = (float)WidthGrid / (float)_gridTransform.transform.localScale.x;
        _waveSpawner.SpawnersPosition(GridController.Instance.CentreGrid, GridController.Instance.HorizontalCount, GridController.Instance.HeightGrid,_scaleSpawner* GameController.Instance.MagnificationFactor-_scaleSpawner);
    }

    public void GridTransform(GameObject gameObject)
    {
        Vector3[] v = new Vector3[4];
        RectTransform.GetWorldCorners(v);

        HeightGrid = v[1].y - v[0].y;
        WidthGrid = v[2].x - v[0].x;
        CentreGrid = RectTransform.position;

        _scaleSpawner = _spawner.transform.position.x - v[0].x;
    }
}
