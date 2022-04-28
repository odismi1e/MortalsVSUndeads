using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlast : MonoBehaviour
{
    private Entity entityEnemy;
    [SerializeField] private ParticleSystem _particleSystem;
    private void Start()
    {
        Invoke("Action", _particleSystem.main.startLifetime.constant);
    }
    private IEnumerator SpeedDebuff(Entity enemy)
    {
        float speedNow = enemy.GetSpeed();
        enemy.SetSpeed(enemy.GetSpeed()*((100f-GameManager.Instance.Spells.IceBlastSpeedDebuff)/100));
        yield return new WaitForSeconds(GameManager.Instance.Spells.IceBlastDuration);
        enemy.SetSpeed(speedNow);
    }
    private void Action()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),   
     (GridController.Instance.HeightGrid / GridController.Instance.HorizontalCount) * GameManager.Instance.Spells.IceBlastHeight);
        foreach (Collider2D a in colliders)
        {
            if (a.gameObject.tag == "Enemy")
            {
                entityEnemy = a.gameObject.GetComponent<Entity>();
                entityEnemy.SetHealtNow(entityEnemy.GetHealthNow() - GameManager.Instance.Spells.IceBlastDamage);
                entityEnemy.HpBar();
                StartCoroutine(SpeedDebuff(entityEnemy));
            }
        }
        Destroy(gameObject, GameManager.Instance.Spells.IceBlastDuration + .1f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0),
         (GridController.Instance.HeightGrid / GridController.Instance.HorizontalCount) * GameManager.Instance.Spells.IceBlastHeight/2f);
    }
}
