using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    private bool _attack = true;

    private Coroutine coroutine;

    private int _damage;
    private float _attackSpeed;
    private float _speed;
    private Health _unit;
    private int _state;
    private void Start()
    {
        _healthMax = EnemyUnitManager.Instance.EnemyHealth;
        _healthNow = _healthMax;
        _damage = EnemyUnitManager.Instance.EnemyDamage;
        _attackSpeed = EnemyUnitManager.Instance.EnemyAttackSpeed;
        _speed = EnemyUnitManager.Instance.EnemySpeed;
        Active = true;
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            if (_healthNow <= 0)
            {
                waveSpawner.NumberOfLiveEnemies--;
                Destroy(gameObject);
            }
            if (_state != 0 && _unit == null)
            {
                _state = 0;
            }
            switch (_state)
            {
                case 0:
                    if (!_attack)
                    {
                        StopCoroutine(coroutine);
                    }
                    _attack = true;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.02f * _speed, gameObject.transform.position.y,
                        gameObject.transform.position.z);
                    break;
                case 1:
                    if (_attack)
                    {
                        coroutine = StartCoroutine(Attack());
                    }
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        CheckUnit();
    }
    private IEnumerator Attack()
    {
        _attack = false;
        yield return new WaitForSeconds(_attackSpeed);
        _unit.DealingDamage(_damage);
        coroutine = StartCoroutine(Attack());
    }
    private Health SearchEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, gameObject.transform.localScale.x);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Unit")
            {
                return colliders[i].gameObject.GetComponent<Health>();
            }
        }
        return null;
    }
    private int QuantityUnits()
    {
        int quantityEnemies = 0;
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, gameObject.transform.localScale.x);
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
}
