using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
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
    [Header("Mana recovery rate.")]
    public float Seconds;
    [Header("The amount of mana received.")]
    public int Mana;
    [Header("Initial amount of mana.")]
    public int StartMana;
    [Header("The number of cards in the hand.")]
    public int NumberCardsHand;
    [Header("Minimum attack speed")]
    public float MinAttackSpeed;


    public UnitManager UnitManager;
    public EnemyUnitManager EnemyUnitManager;
    public EnhancementsCardManager EnhancementsCardManager;
    public SpellsCardManager SpellsCardManager;

    public void ScreenScaleFactor()
    {
        UnitManager.SwordsmanAttackDistance *= GameController.Instance.ScreenScaleFactor;
        UnitManager.CrossbowArrowSpeed*= GameController.Instance.ScreenScaleFactor;

        EnemyUnitManager.EnemyAttackDistance*= GameController.Instance.ScreenScaleFactor;
        EnemyUnitManager.EnemySpeed*= GameController.Instance.ScreenScaleFactor;
    }


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
