using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static public GameController Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            MagnificationFactor = (float)Screen.currentResolution.width / (float)1920f;
        }
    }
    public List<Entity> Unit;
    public float MagnificationFactor;
    public void UnitDeleteList(GameObject gameObject)
    {
        var entity = gameObject.GetComponent<Entity>();
        for(int i=0;i<Unit.Count;i++)
        {
            if(entity==Unit[i])
            {
                Unit.RemoveAt(i);
                Destroy(gameObject);
                break;
            }
        }
    }
  }
