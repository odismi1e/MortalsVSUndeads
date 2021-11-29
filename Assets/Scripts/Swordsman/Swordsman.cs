using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : Entity
{
    private bool _attack=true;
    private bool _checkSecurity=true;

    private Coroutine _attackCoroutine;
    private Coroutine _checkSecurityCoroutine;

    private Entity _enemy;
    private int _state;

    private void Start()
    {
        _healthMax = GameManager.Instance.UnitManager.SwordsmanHealth;
        _healthNow = _healthMax;
        _damage = GameManager.Instance.UnitManager.SwordsmanDamage;
        _attackSpeed = GameManager.Instance.UnitManager.SwordsmanAttackSpeed;

        GameController.Instance.Unit.Add(gameObject.GetComponent<Entity>());

        _checkSecurityCoroutine = StartCoroutine(CheckSecurity());
    }

    private void Update()
    {
            if (_healthNow <= 0)
            {
                GameController.Instance.UnitDeleteList(gameObject);
            }
            if(_state!=0 && _enemy==null)
            {
              _checkSecurityCoroutine = StartCoroutine(CheckSecurity());
            }
            switch (_state)
            {
                case 0:
                    _damageAbsorption = 0;
                    if (!_attack)
                    {
                        StopCoroutine(_attackCoroutine);
                    //StopCoroutine(Attack());
                    _attack = true;
                    }
                    if(_checkSecurity)
                    {
                    _checkSecurityCoroutine = StartCoroutine(CheckSecurity());
                    }
                    _attack = true;
                    break;
                case 1:
                    _damageAbsorption = 0;
                    if (_attack)
                    {
                        _attackCoroutine=StartCoroutine(Attack());

                    }
                    break;
                case 2:
                    _damageAbsorption = GameManager.Instance.UnitManager.SwordsmanDamageAbsorption;
                    if (!_attack)
                    {
                        StopCoroutine(_attackCoroutine);
                        _attack = true;
                    }
                    //_attack = true;
                    break;
            }
    }
    private IEnumerator Attack()
    {
        _attack = false;
        yield return new WaitForSeconds(_attackSpeed);
        _enemy.ApplyDamage(_damage);
        _attackCoroutine = StartCoroutine(Attack());
    }
    private Entity SearchEnemies()
    {
        Collider2D[] colliders =Physics2D.OverlapBoxAll
            (new Vector2(gameObject.transform.position.x + (GameManager.Instance.UnitManager.SwordsmanAttackDistance / 2),gameObject.transform.position.y) ,
            new Vector2(GameManager.Instance.UnitManager.SwordsmanAttackDistance, 0.1f),0);
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
        Collider2D[] colliders = Physics2D.OverlapBoxAll
            (new Vector2(gameObject.transform.position.x + (GameManager.Instance.UnitManager.SwordsmanAttackDistance / 2), gameObject.transform.position.y),
            new Vector2(GameManager.Instance.UnitManager.SwordsmanAttackDistance, 0.1f), 0);
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
        _checkSecurity = false;
        CheckEnemy();
        yield return new WaitForSeconds(.5f);
        _checkSecurityCoroutine= StartCoroutine(CheckSecurity());
    }
}