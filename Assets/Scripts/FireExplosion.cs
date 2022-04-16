using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosion:MonoBehaviour
{
    private Entity entityEnemy;
    [SerializeField] private ParticleSystem _particleSystem;
    private void Start()
    {
        Invoke("Action", _particleSystem.main.startLifetime.constant);
    }
    private IEnumerator DOT(Entity enemy)
    {
        for (int i = 0; i < GameManager.Instance.SpellsCard.FireExplosionDuration; i++)
        {
            if (enemy != null)
            {
                yield return new WaitForSeconds(1);
                enemy.SetHealtNow(enemy.GetHealthNow() -  GameManager.Instance.SpellsCard.DOTFireExplosionDamage);
                enemy.HpBar();
            }
        }
    }
    private void Action()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
         (GridController.Instance.HeightGrid / GridController.Instance.HorizontalCount) * GameManager.Instance.SpellsCard.FireExplosionHeight/2f);
        foreach (Collider2D a in colliders)
        {
            if (a.gameObject.tag == "Enemy")
            {
                entityEnemy = a.gameObject.GetComponent<Entity>();
                entityEnemy.SetHealtNow(entityEnemy.GetHealthNow() - GameManager.Instance.SpellsCard.FireExplosionDamage);
                entityEnemy.HpBar();
                StartCoroutine(DOT(entityEnemy));
            }
        }
        Destroy(gameObject, GameManager.Instance.SpellsCard.FireExplosionDuration + .1f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,0),
         (GridController.Instance.HeightGrid / GridController.Instance.HorizontalCount) * GameManager.Instance.SpellsCard.FireExplosionHeight/2f);
    }
}
