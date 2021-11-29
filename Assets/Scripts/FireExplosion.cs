using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosion:MonoBehaviour
{
    private Entity entityEnemy;
    private void Start()
    {

        Collider2D[] colliders = Action();
            foreach(Collider2D a in colliders)
            {
                if (a.gameObject.tag == "Enemy")
                {
                    entityEnemy = a.gameObject.GetComponent<Entity>();
                    entityEnemy.SetHealtNow(entityEnemy.GetHealthNow()-GameManager.Instance.SpellsCardManager.FireExplosionDamage);
                    entityEnemy.HpBar();
                    StartCoroutine(DOT(entityEnemy));
                }
            }
            Destroy(gameObject, GameManager.Instance.SpellsCardManager.FireExplosionDuration+.1f);

    }
    private IEnumerator DOT(Entity enemy)
    {
        for (int i = 0; i < GameManager.Instance.SpellsCardManager.FireExplosionDuration; i++)
        {
            if (enemy != null)
            {
                yield return new WaitForSeconds(1);
                enemy.SetHealtNow(enemy.GetHealthNow() -  GameManager.Instance.SpellsCardManager.DOTFireExplosionDamage);
                enemy.HpBar();
            }
        }
    }
    private Collider2D[] Action()
    {
        return Physics2D.OverlapBoxAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
         new Vector2((GridController.Instance.WidthGrid / GridController.Instance.VerticalCount) * GameManager.Instance.SpellsCardManager.FireExplosionWidth,
         (GridController.Instance.HeightGrid / GridController.Instance.HorizontalCount) * GameManager.Instance.SpellsCardManager.FireExplosionHeight), 0); ;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,0),
         new Vector3((GridController.Instance.WidthGrid / GridController.Instance.VerticalCount) * GameManager.Instance.SpellsCardManager.FireExplosionWidth,
         (GridController.Instance.HeightGrid / GridController.Instance.HorizontalCount) * GameManager.Instance.SpellsCardManager.FireExplosionHeight,1));
    }
}
