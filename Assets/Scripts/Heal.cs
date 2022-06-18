using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private void Update()
    {
            foreach (Entity a in LevelController.Instance.Unit)
            {
                a.SetHealtNow(GameManager.Instance.Buffs.HealQuantity + a.GetHealthNow());
                a.HpBar();
            }
            Destroy(gameObject);
    }
}
