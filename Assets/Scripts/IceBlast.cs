using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlast : MonoBehaviour
{
    private Entity entityEnemy;
    private void Start()
    {//Action

        Collider2D[] colliders = Action();
        foreach (Collider2D a in colliders)
        {
            if (a.gameObject.tag == "Enemy")
            {
                entityEnemy = a.gameObject.GetComponent<Entity>();
                entityEnemy.SetHealtNow(entityEnemy.GetHealthNow() - GameManager.Instance.SpellsCardManager.IceBlastDamage);
                entityEnemy.HpBar();
                StartCoroutine(SpeedDebuff(entityEnemy));
            }
        }
        Destroy(gameObject, GameManager.Instance.SpellsCardManager.IceBlastDuration + .1f);

    }
    private IEnumerator SpeedDebuff(Entity enemy)
    {
        float speedNow = enemy.GetSpeed();
        enemy.SetSpeed(enemy.GetSpeed()*((100f-GameManager.Instance.SpellsCardManager.IceBlastSpeedDebuff)/100));
        yield return new WaitForSeconds(GameManager.Instance.SpellsCardManager.IceBlastDuration);
        enemy.SetSpeed(speedNow);
    }
    private Collider2D[] Action()
    {
        return Physics2D.OverlapBoxAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
     new Vector2((GridController.Instance.WidthGrid / GridController.Instance.VerticalCount) * GameManager.Instance.SpellsCardManager.IceBlastWidth,
     (GridController.Instance.HeightGrid / GridController.Instance.HorizontalCount) * GameManager.Instance.SpellsCardManager.IceBlastHeight), 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0),
         new Vector3((GridController.Instance.WidthGrid / GridController.Instance.VerticalCount) * GameManager.Instance.SpellsCardManager.IceBlastWidth,
         (GridController.Instance.HeightGrid / GridController.Instance.HorizontalCount) * GameManager.Instance.SpellsCardManager.IceBlastHeight, 1));
    }
}
