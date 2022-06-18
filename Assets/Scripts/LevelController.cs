using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    static public LevelController Instance;
    public Timer Timer;
    public int NumberOfPassedUnitsBeforeDefeat;
    [HideInInspector] public List<Entity> Unit;
    [HideInInspector] public float ScreenScaleFactor;
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
    }
    public void UnitDeleteList(GameObject gameObject,float duration=0)
    {
        var entity = gameObject.GetComponent<Entity>();
        for(int i=0;i<Unit.Count;i++)
        {
            if(entity==Unit[i])
            {
                Unit.RemoveAt(i);
                Destroy(gameObject,duration);
                break;
            }
        }
    }
  }
