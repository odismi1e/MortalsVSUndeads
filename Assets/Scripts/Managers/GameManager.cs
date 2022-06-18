using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; private set => _instance = value; }

    [SerializeField] private bool useTablesData = true;
    public ManaData Mana;
    public GlobalData Global;
    public UnitsData Units;
    public EnemyUnitsData EnemyUnits;
    public BuffsData Buffs;
    public SpellsData Spells;
    void Awake()
    {
        InitalizeSingleton();
        InitializeGameManagerFields();
    }
    void Start()
    {
        
    }
    private void InitializeGameManagerFields()
    {
        if (useTablesData)
        {
            Mana.ManaRegenerationRate = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Mana, DataFieldKey.ManaRegenerationRate));
            Mana.ManaPerTick = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Mana, DataFieldKey.ManaPerTick));
            Mana.ManaStartValue = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Mana, DataFieldKey.ManaStartValue));

            Global.MaxNumCardsInHand = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Global, DataFieldKey.MaxNumCardsInHand));
            Global.MeleeAtackRange = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Global, DataFieldKey.MeleeAtackRange));
            Global.MinAttackSpeed = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Global, DataFieldKey.MinAttackSpeed));

            Units.SwordsmanAttackDistance = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.SwordsmanAttackDistance));
            Units.SwordsmanAttackSpeed = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.SwordsmanAttackSpeed));
            Units.SwordsmanDamage = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.SwordsmanDamage));
            Units.SwordsmanDamageAbsorption = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.SwordsmanDamageAbsorption));
            Units.SwordsmanHealth = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.SwordsmanHealth));
            Units.SwordsmanManaCost = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.SwordsmanManaCost));

            Units.CrossbowmanArmorPenetration = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.CrossbowmanArmorPenetration));
            Units.CrossbowmanArrowSpeed = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.CrossbowmanArrowSpeed));
            Units.CrossbowmanAttackSpeed = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.CrossbowmanAttackSpeed));
            Units.CrossbowmanDamage = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.CrossbowmanDamage));
            Units.CrossbowmanHealth = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.CrossbowmanHealth));
            Units.CrossbowmanManaCost = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Units, DataFieldKey.CrossbowmanManaCost));

            EnemyUnits.SkeletonArmor = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.EnemyUnits, DataFieldKey.SkeletonArmor));
            EnemyUnits.SkeletonAttackDistance = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.EnemyUnits, DataFieldKey.SkeletonAttackDistance));
            EnemyUnits.SkeletonAttackSpeed = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.EnemyUnits, DataFieldKey.SkeletonAttackSpeed));
            EnemyUnits.SkeletonDamage = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.EnemyUnits, DataFieldKey.SkeletonDamage));
            EnemyUnits.SkeletonHealth = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.EnemyUnits, DataFieldKey.SkeletonHealth));
            EnemyUnits.SkeletonSpeed = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.EnemyUnits, DataFieldKey.SkeletonSpeed));

            Buffs.HealCardManaCost = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Buff, DataFieldKey.HealCardManaCost));
            Buffs.HealQuantity = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Buff, DataFieldKey.HealQuantity));

            Buffs.RageAttackSpeed = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Buff, DataFieldKey.RageAttackSpeed));
            Buffs.RageCardManaCost = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Buff, DataFieldKey.RageCardManaCost));
            Buffs.RageDuration = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Buff, DataFieldKey.RageDuration));

            Spells.FireExplosionDamage = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.FireExplosionDamage));
            Spells.FireExplosionDOTDamage = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.FireExplosionDOTDamage));
            Spells.FireExplosionDuration = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.FireExplosionDuration));
            Spells.FireExplosionHeight = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.FireExplosionHeight));
            Spells.FireExplosionWidth = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.FireExplosionWidth));
            Spells.FireExplosionManaCost = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.FireExplosionManaCost));

            Spells.IceBlastDamage = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.IceBlastDamage));
            Spells.IceBlastDuration = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.IceBlastDuration));
            Spells.IceBlastHeight = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.IceBlastHeight));
            Spells.IceBlastWidth = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.IceBlastWidth));
            Spells.IceBlastSpeedDebuff = DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.IceBlastSpeedDebuff));
            Spells.IceBlastManaCost = (int)DataManager.Parse(DataManager.JoinStringsWithDot(DataPrimaryKey.Spells, DataFieldKey.IceBlastManaCost));
        }
    }

    public void ScreenScaleFactor()
    {
        Units.SwordsmanAttackDistance *= LevelController.Instance.ScreenScaleFactor;
        Units.CrossbowmanArrowSpeed *= LevelController.Instance.ScreenScaleFactor;

        EnemyUnits.SkeletonAttackDistance *= LevelController.Instance.ScreenScaleFactor;
        EnemyUnits.SkeletonSpeed *= LevelController.Instance.ScreenScaleFactor;

        Global.MeleeAtackRange *= LevelController.Instance.ScreenScaleFactor;
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
public class ManaData
{
    public float ManaRegenerationRate;
    public int ManaPerTick;
    public int ManaStartValue;
}
[System.Serializable]
public class GlobalData
{
    public int MaxNumCardsInHand;
    public float MeleeAtackRange;
    public float MinAttackSpeed;
}
[System.Serializable]
public class UnitsData
{
        [Header("Swordsman")]
        public float SwordsmanHealth;
        public float SwordsmanDamage;
        public float SwordsmanAttackSpeed;
        public float SwordsmanAttackDistance;
        public float SwordsmanDamageAbsorption;
        public int SwordsmanManaCost;

        [Header("Crossbowman")]
        public float CrossbowmanHealth;
        public float CrossbowmanDamage;
        public float CrossbowmanAttackSpeed;
        public float CrossbowmanArrowSpeed;
        public float CrossbowmanArmorPenetration;
        public int CrossbowmanManaCost;
}
[System.Serializable]
public class EnemyUnitsData
{
    [Header("Skeleton")]
    public float SkeletonHealth;
    public float SkeletonDamage;
    public float SkeletonAttackSpeed;
    public float SkeletonAttackDistance;
    public float SkeletonSpeed;
    public float SkeletonArmor;
}
[System.Serializable]
public class BuffsData
{
    [Header("Heal")]
    public float HealQuantity;
    public int HealCardManaCost;

    [Header("Rage")]
    public float RageAttackSpeed;
    public float RageDuration;
    public int RageCardManaCost;
}
[System.Serializable]
public class SpellsData
{
    [Header("FireExplosion")]
    public int FireExplosionWidth;
    public int FireExplosionHeight;
    public float FireExplosionDamage;
    public float FireExplosionDOTDamage;
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
