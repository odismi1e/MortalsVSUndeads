using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    static public UnitManager Instance;
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
    [Header("Swordsman")]
    public int SwordsmanHealth;
    public int SwordsmanDamage;
    public float SwordsmanAttackSpeed;
    public float SwordsmanDamageAbsorption;
    public int SwordsmanMana;
    [Header("Crossbowman")]
    public int CrossbowmanHealth;
    public int CrossbowArrowDamage;
    public float CrossbowmanAttackSpeed;
    public float CrossbowArrowSpeed;
    public int CrossbowmanMana;
}
