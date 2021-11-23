using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : Entity
{
    private bool _attack=true;
    private bool checkSecurity = true;

    private Coroutine coroutine;

    private Entity _enemy;
    private int _state;

    private void Start()
    {
        _healthMax = GameManager.Instance.UnitManager.SwordsmanHealth;
        _healthNow = _healthMax;
        _damage = GameManager.Instance.UnitManager.SwordsmanDamage;
        _attackSpeed = GameManager.Instance.UnitManager.SwordsmanAttackSpeed;

        GameController.Instance.Unit.Add(gameObject.GetComponent<Entity>());
    }

    private void Update()
    {
            if (_healthNow <= 0)
            {
                GameController.Instance.UnitDeleteList(gameObject);
            }
            if(_state==2 && checkSecurity)
            {
                StartCoroutine(CheckSecurity());
            }
            if(_state!=0 && _enemy==null)
            {
                CheckEnemy();
            }
            switch (_state)
            {
                case 0:
                    _damageAbsorption = 0;
                    if (!_attack)
                    {
                        StopCoroutine(coroutine);
                    }
                    _attack = true;
                    break;
                case 1:
                    _damageAbsorption = 0;
                    if (_attack)
                    {
                        coroutine=StartCoroutine(Attack());
                    }
                    break;
                case 2:
                    _damageAbsorption = GameManager.Instance.UnitManager.SwordsmanDamageAbsorption;
                    if (!_attack)
                    {
                        StopCoroutine(coroutine);
                    }
                    _attack = true;
                    break;
            }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        CheckEnemy();
    }
    private IEnumerator Attack()
    {
        _attack = false;
        yield return new WaitForSeconds(_attackSpeed);
        _enemy.ApplyDamage(_damage);
        coroutine = StartCoroutine(Attack());
    }
    private Entity SearchEnemies()
    {
        Collider2D[] colliders =Physics2D.OverlapCircleAll(gameObject.transform.position, gameObject.transform.localScale.x);
        for(int i=0;i<colliders.Length;i++)
        {
            if(colliders[i].gameObject.tag=="Enemy")
            {
                return colliders[i].gameObject.GetComponent<Entity>();
            }
        }
        return null;
    }
    private int QuantityEnemies()
    {
        int quantityEnemies = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, gameObject.transform.localScale.x);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy")
            {
                quantityEnemies++;
            }
        }
        return quantityEnemies;
    }
    private void CheckEnemy()
    {
        int size = QuantityEnemies();
        Entity searchEnemies = SearchEnemies();
        if (size == 1)
        {
            _state = 1;
            _enemy = searchEnemies;
        }
        else
        {
            if (size >= 2)
            {
                _state = 2;
                _enemy = searchEnemies;
            }
            else
            {
                _state = 0;
            }
        }
    }
    private IEnumerator CheckSecurity()
    {
        checkSecurity = false;
        CheckEnemy();
        yield return new WaitForSeconds(.5f);
        checkSecurity = true;
    }
}