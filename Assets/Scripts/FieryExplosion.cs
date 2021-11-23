using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieryExplosion:MonoBehaviour
{
    private bool Active = true;
    private Entity entityEnemy;
    private void Update()
    {
        if(Active)
        {
            Active = false;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(gameObject.transform.position.x,gameObject.transform.position.y),
         new Vector2((GridController.Instance.WidthGrid/GridController.Instance.VerticalCount)*GameManager.Instance.EnhancementsCardManager.FieryExplosionWidth,
         (GridController.Instance.HeightGrid/GridController.Instance.HorizontalCount)*GameManager.Instance.EnhancementsCardManager.FieryExplosionHeight),0);
            foreach(Collider2D a in colliders)
            {
                if (a.gameObject.tag == "Enemy")
                {
                    entityEnemy = a.gameObject.GetComponent<Entity>();
                    entityEnemy.SetHealtNow(entityEnemy.GetHealthNow()-GameManager.Instance.EnhancementsCardManager.FieryExplosionDamage);
                    entityEnemy.HpBar();
                    StartCoroutine(Gorenje(entityEnemy));
                }
            }
            Destroy(gameObject, GameManager.Instance.EnhancementsCardManager.FieryExplosionDuration+.1f);
        }
    }
    private IEnumerator Gorenje(Entity enemy)
    {
        for (int i = 0; i < GameManager.Instance.EnhancementsCardManager.FieryExplosionFrequency; i++)
        {
            if (enemy != null)
            {
                enemy.SetHealtNow(enemy.GetHealthNow() - GameManager.Instance.EnhancementsCardManager.PasivFieryExplosionDamage / GameManager.Instance.EnhancementsCardManager.FieryExplosionFrequency);
                enemy.HpBar();
                yield return new WaitForSeconds(GameManager.Instance.EnhancementsCardManager.FieryExplosionDuration / GameManager.Instance.EnhancementsCardManager.FieryExplosionFrequency);
            }
        }
    }
}
