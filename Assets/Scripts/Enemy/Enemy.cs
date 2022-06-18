using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private bool _attack = true;
    private bool _alive=true;

    private Coroutine _attackCoroutine1;

    private Entity _unit;
    private int _state;

    private void Start()
    {
        _healthMax = GameManager.Instance.EnemyUnits.SkeletonHealth;
        _healthNow = _healthMax;
        _damage = GameManager.Instance.EnemyUnits.SkeletonDamage;
        _attackSpeed = GameManager.Instance.EnemyUnits.SkeletonAttackSpeed;
        _speed = GameManager.Instance.EnemyUnits.SkeletonSpeed;
        _damageAbsorption = GameManager.Instance.EnemyUnits.SkeletonArmor;

        StartCoroutine(CheckNearestUnitToAttack());

    }

    private void FixedUpdate()
    {
        AnimatorSpeed();
        if (_alive)
        {
            //if (_healthNow <= 0)
            //{
            //    waveSpawner.NumberOfLiveEnemies--;
            //    GameController.Instance.Timer.TimerDelta();
            //    GetHPBar().SetActive(false);
            //    if (_attackCoroutine1 != null)
            //    {
            //        StopCoroutine(_attackCoroutine1);
            //    }
            //    gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            //    _alive = false;
            //    _state = 99;
            //    _animator.SetInteger("state",3);
            //    Destroy(gameObject, _animations[0].length + 3);
            //}
            if (_state != 0 && _unit == null)
            {
                _state = 0;
            }
            switch (_state)
            {
                case 0:
                    _animator.SetInteger("state", 0);
                    if (!_attack)
                    {
                        StopCoroutine(_attackCoroutine1);
                        _attack = true;
                    }

                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - Time.fixedDeltaTime * _speed, gameObject.transform.position.y,
                        gameObject.transform.position.z);
                    break;
                case 1:
                    if (_attack)
                    {
                        _attackCoroutine1 = StartCoroutine(Attack());
                    }
                    break;
            }
            if (_healthNow <= 0)
            {
                waveSpawner.NumberOfLiveEnemies--;
                LevelController.Instance.Timer.TimerDelta();
                GetHPBar().SetActive(false);
                if (_attackCoroutine1 != null)
                {
                    StopCoroutine(_attackCoroutine1);
                }
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                _alive = false;
                _state = 99;
                _animator.SetInteger("state", 3);
                Destroy(gameObject, _animations[0].length + 3);
            }
        }
    }
    private IEnumerator Attack()
    {
        AnimatorSpeed();
        _attack = false;
        switch (Random.Range(0,2))
        {
            case 0:
                _animator.Play("Attack_1");
                break;
            case 1:
                _animator.Play("Attack_2");
                break;
        }
        _animator.SetInteger("state", 1);
        yield return new WaitForSeconds(1f/_attackSpeed);
        _unit.ApplyDamage(_damage);
        _attack = true;
    }
    private Entity SearchEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll
            (new Vector2(gameObject.transform.position.x - (GameManager.Instance.Global.MeleeAtackRange / 2), gameObject.transform.position.y),
            new Vector2(GameManager.Instance.Global.MeleeAtackRange, 0.1f), 0);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Unit")
            {
                return colliders[i].gameObject.GetComponent<Entity>();
            }
        }
        return null;
    }
    private int QuantityUnits()
    {
        int quantityEnemies = 0;
        Collider2D[] colliders = Physics2D.OverlapBoxAll
           (new Vector2(gameObject.transform.position.x - (GameManager.Instance.Global.MeleeAtackRange / 2), gameObject.transform.position.y),
           new Vector2(GameManager.Instance.Global.MeleeAtackRange, 0.1f), 0);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Unit")
            {
                quantityEnemies++;
            }
        }
        return quantityEnemies;
    }
    private void CheckUnit()
    {
        int sie = QuantityUnits();
        if (sie >= 1)
        {
            _state = 1;
            _unit = SearchEnemies();
        }
        else
        {
            if (sie == 0)
            {
                _state = 0;
                _unit = null;
            }
        }
    }
    private IEnumerator CheckNearestUnitToAttack()
    {
        CheckUnit();
        yield return new WaitForSeconds(.25f);
        StartCoroutine(CheckNearestUnitToAttack());
    }
    private void AnimatorSpeed()
    {
        _animator.SetFloat("speed Attack_1", _attackSpeed);
        _animator.SetFloat("speed Attack_2", _attackSpeed);
        _animator.SetFloat("speed Walk", _speed);
    }
}
