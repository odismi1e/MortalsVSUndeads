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
            foreach (Entity a in GameController.Instance.Unit)
            {
                StartCoroutine(RageCoroutin(a));
            }
            Active = false;
            Destroy(gameObject, GameManager.Instance.EnhancementsCard.RageDuration + .1f);
        }
    }
    IEnumerator RageCoroutin(Entity entity)
    {
        float attackSpeedNow = entity.GetAttackSpeed();
        entity.SetAttackSpeed(entity.GetAttackSpeed() + GameManager.Instance.EnhancementsCard.RageAttackSpeed);
        yield return new WaitForSeconds(GameManager.Instance.EnhancementsCard.RageDuration);
        entity.SetAttackSpeed(attackSpeedNow);
    }
}
