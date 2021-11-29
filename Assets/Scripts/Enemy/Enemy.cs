using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private bool _attack = true;
    private bool _checkSecurity = true;

    private Coroutine _coroutine1;
    private Coroutine _coroutine2;

    private Entity _unit;
    private int _state;

    private void Start()
    {
        _healthMax = GameManager.Instance.EnemyUnitManager.EnemyHealth;
        _healthNow = _healthMax;
        _damage = GameManager.Instance.EnemyUnitManager.EnemyDamage;
        _attackSpeed = GameManager.Instance.EnemyUnitManager.EnemyAttackSpeed;
        _speed = GameManager.Instance.EnemyUnitManager.EnemySpeed;
        _damageAbsorption = GameManager.Instance.EnemyUnitManager.Armor;

        _coroutine2 = StartCoroutine(CheckNearestUnitToAttack());
    }

    private void FixedUpdate()
    {
            if (_healthNow <= 0)
            {
                waveSpawner.NumberOfLiveEnemies--;
                GameController.Instance.Timer.TimerDelta();
                Destroy(gameObject);
            }
            if (_state != 0 && _unit == null)
            {
              _coroutine2 = StartCoroutine(CheckNearestUnitToAttack());
            }
            switch (_state)
            {
                case 0:
                    if (!_attack)
                    {
                        StopCoroutine(_coroutine1);
                    _attack = true;
                    }
                    //_attack = true;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - Time.fixedDeltaTime * _speed, gameObject.transform.position.y,
                        gameObject.transform.position.z);
                    break;
                case 1:
                    if (_attack)
                    {
                        _coroutine1 = StartCoroutine(Attack());
                    }
                if (!_checkSecurity)
                {
                    StopCoroutine(_coroutine2);
                }
                break;
            }
    }
    private IEnumerator Attack()
    {
        _attack = false;
        yield return new WaitForSeconds(_attackSpeed);
        _unit.ApplyDamage(_damage);
        _coroutine1 = StartCoroutine(Attack());
    }
    private Entity SearchEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll
            (new Vector2(gameObject.transform.position.x - (GameManager.Instance.EnemyUnitManager.EnemyAttackDistance  / 2), gameObject.transform.position.y),
            new Vector2(GameManager.Instance.EnemyUnitManager.EnemyAttackDistance , 0.1f), 0);
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
           (new Vector2(gameObject.transform.position.x - (GameManager.Instance.EnemyUnitManager.EnemyAttackDistance  / 2), gameObject.transform.position.y),
           new Vector2(GameManager.Instance.EnemyUnitManager.EnemyAttackDistance , 0.1f), 0);
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
        _checkSecurity = false;
        CheckUnit();
        yield return new WaitForSeconds(.5f);
        _coroutine2= StartCoroutine(CheckNearestUnitToAttack());
    }
}
