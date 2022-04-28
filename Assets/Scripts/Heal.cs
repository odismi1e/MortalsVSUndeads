using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private void Update()
    {
            foreach (Entity a in GameController.Instance.Unit)
            {
                a.SetHealtNow(GameManager.Instance.Enhancements.HealQuantity + a.GetHealthNow());
                a.HpBar();
            }
            Destroy(gameObject);
    }
}
