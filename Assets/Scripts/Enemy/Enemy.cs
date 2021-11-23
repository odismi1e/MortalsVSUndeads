using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private bool _attack = true;

    private Coroutine coroutine;

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
    }

    private void FixedUpdate()
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
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - Time.fixedDeltaTime * _speed*GameController.Instance.MagnificationFactor, gameObject.transform.position.y,
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
    private void OnTriggerEnter2D(Collider2D collider)
    {
        CheckUnit();
    }
    private IEnumerator Attack()
    {
        _attack = false;
        yield return new WaitForSeconds(_attackSpeed);
        _unit.ApplyDamage(_damage);
        coroutine = StartCoroutine(Attack());
    }
    private Entity SearchEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, gameObject.transform.localScale.x/1.9f);
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, gameObject.transform.localScale.x/1.9f);
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
