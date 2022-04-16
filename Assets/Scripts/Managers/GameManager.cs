using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; private set => _instance = value; }

    public ManaManager ManaSystem;
    public OtherFieldsManager OtherFields;
    public UnitManager Unit;
    public EnemyUnitManager EnemyUnit;
    public EnhancementsCardManager EnhancementsCard;
    public SpellsCardManager SpellsCard;
    void Awake()
    {
        InitalizeSingleton();        
    }
    void Start()
    {
        InitializeGameManagerFields();
    }
    private void InitializeGameManagerFields()
    {

    }

    public void ScreenScaleFactor()
    {
        Unit.SwordsmanAttackDistance *= GameController.Instance.ScreenScaleFactor;
        Unit.CrossbowArrowSpeed *= GameController.Instance.ScreenScaleFactor;

        EnemyUnit.EnemyAttackDistance *= GameController.Instance.ScreenScaleFactor;
        EnemyUnit.EnemySpeed *= GameController.Instance.ScreenScaleFactor;
    }
    private void InitalizeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
}

[System.Serializable]
public class OtherFieldsManager
{
    public int NumberCardsHand;
    public float MeleeAtackRange;
    public float MinAttackSpeed;
}
[System.Serializable]
public class ManaManager
{
    public float ManaRegenerationRate;
    public int ManaPerTick;
    public int StartMana;
}
[System.Serializable]
public class UnitManager
{
    [Header("Swordsman")]
    public float SwordsmanHealth;
    public float SwordsmanDamage;
    public float SwordsmanAttackSpeed;
    public float SwordsmanAttackDistance;
    public float SwordsmanDamageAbsorption;
    public int SwordsmanMana;

    [Header("Crossbowman")]
    public float CrossbowmanHealth;
    public float CrossbowArrowDamage;
    public float CrossbowmanAttackSpeed;
    public float CrossbowArrowSpeed;
    public float CrossbowmanIgnoringArmor;
    public int CrossbowmanMana;
}
[System.Serializable]
public class EnemyUnitManager
{
    [Header("Enemy")]
    public float EnemyHealth;
    public float EnemyDamage;
    public float EnemyAttackSpeed;
    public float EnemyAttackDistance;
    public float EnemySpeed;
    public float Armor;
}
[System.Serializable]
public class EnhancementsCardManager
{
    [Header("Healing")]
    public float HealQuantity;
    public int HealCardManaCost;

    [Header("Rage")]
    public float RageAttackSpeed;
    public float RageDuration;
    public int RageCardManaCost;
}
[System.Serializable]
public class SpellsCardManager
{
    [Header("FireExplosion")]
    public int FireExplosionWidth;
    public int FireExplosionHeight;
    public float FireExplosionDamage;
    public float DOTFireExplosionDamage;
    public int FireExplosionDuration;
    public int FireExplosionManaCost;

    [Header("IceBlast")]
    public int IceBlastWidth;
    public int IceBlastHeight;
    public float IceBlastDamage;
    public float IceBlastSpeedDebuff;
    public float IceBlastDuration;
    public int IceBlastManaCost;
}
