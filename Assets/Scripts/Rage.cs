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
            Destroy(gameObject, GameManager.Instance.EnhancementsCardManager.RageDuration + .1f);
        }
    }
    IEnumerator RageCoroutin(Entity entity)
    {
        float attackSpeedNow = entity.GetAttackSpeed();
        entity.SetAttackSpeed(entity.GetAttackSpeed() + GameManager.Instance.EnhancementsCardManager.RageAttackSpeed);
        yield return new WaitForSeconds(GameManager.Instance.EnhancementsCardManager.RageDuration);
        entity.SetAttackSpeed(attackSpeedNow);
    }
}
