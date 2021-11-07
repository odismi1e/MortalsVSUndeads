using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitManager : MonoBehaviour
{
    static public EnemyUnitManager Instance;
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
    [Header("Enemy")]
    public int EnemyHealth;
    public int EnemyDamage;
    public float EnemyAttackSpeed;
    public float EnemySpeed;
}
