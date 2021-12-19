using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card", fileName = "new Card", order = 51)]
public class Card : ScriptableObject
{
    public bool Unit;
    public Sprite Icon;
    public GameObject Prefab;
    public int id;
}
