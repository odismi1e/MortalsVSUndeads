using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : Health
{
    private bool _attack=true;
    private bool checkSecurity = true;

    private Coroutine coroutine;

    private int _damage;
    private float _attackSpeed;
    private Health _enemy;
    private int _state;
    private void Start()
    {
        _healthMax = UnitManager.Instance.SwordsmanHealth;
        _healthNow = _healthMax;
        _damage = UnitManager.Instance.SwordsmanDamage;
        _attackSpeed = UnitManager.Instance.SwordsmanAttackSpeed;
    }

    private void Update()
    {
        if(Active)
        {
            if (_healthNow <= 0)
            {
                Destroy(gameObject);
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
                    _damageAbsorption = 1;
                    if (!_attack)
                    {
                        StopCoroutine(coroutine);
                    }
                    _attack = true;
                    break;
                case 1:
                    _damageAbsorption = 1;
                    if (_attack)
                    {
                        coroutine=StartCoroutine(Attack());
                    }
                    break;
                case 2:
                    _damageAbsorption = UnitManager.Instance.SwordsmanDamageAbsorption;
                    if (!_attack)
                    {
                        StopCoroutine(coroutine);
                    }
                    _attack = true;
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        CheckEnemy();
    }
    private IEnumerator Attack()
    {
        _attack = false;
        yield return new WaitForSeconds(_attackSpeed);
        _enemy.DealingDamage(_damage);
        coroutine = StartCoroutine(Attack());
    }
    private Health SearchEnemies()
    {
        Collider[] colliders =Physics.OverlapSphere(gameObject.transform.position, gameObject.transform.localScale.x);
        for(int i=0;i<colliders.Length;i++)
        {
            if(colliders[i].gameObject.tag=="Enemy")
            {
                return colliders[i].gameObject.GetComponent<Health>();
            }
        }
        return null;
    }
    private int QuantityEnemies()
    {
        int quantityEnemies = 0;
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, gameObject.transform.localScale.x);
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
        int sie = QuantityEnemies();
        Health searchEnemies = SearchEnemies();
        if (sie == 1)
        {
            _state = 1;
            _enemy = searchEnemies;
        }
        else
        {
            if (sie >= 2)
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