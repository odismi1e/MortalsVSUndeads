using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : MonoBehaviour
{
    private bool Active=true;
    private void Update()
    {
        if (Active)
        {
            foreach (Entity a in LevelController.Instance.Unit)
            {
                StartCoroutine(RageCoroutin(a));
            }
            Active = false;
            Destroy(gameObject, GameManager.Instance.Enhancements.RageDuration + .1f);
        }
    }
    IEnumerator RageCoroutin(Entity entity)
    {
        float attackSpeedNow = entity.GetAttackSpeed();
        entity.SetAttackSpeed(entity.GetAttackSpeed() + GameManager.Instance.Enhancements.RageAttackSpeed);
        yield return new WaitForSeconds(GameManager.Instance.Enhancements.RageDuration);
        entity.SetAttackSpeed(attackSpeedNow);
    }
}
