using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbowman : Entity
{
    [SerializeField] private Transform _transformCrossbow;
    [SerializeField] private GameObject _crossbowArrow;

    private bool _attack = true;
    private bool checkSecurity = true;

    private Coroutine coroutine;

    private int _state;
    private void Start()
    {
        _healthMax = GameManager.Instance.Unit.SwordsmanHealth;
        _healthNow = _healthMax;
        _attackSpeed = GameManager.Instance.Unit.CrossbowmanAttackSpeed;
        StartCoroutine(CheckSecurity());

        GameController.Instance.Unit.Add(gameObject.GetComponent<Entity>());
    }

    private void FixedUpdate()
    {
            if (_healthNow <= 0)
            {
                GameController.Instance.UnitDeleteList(gameObject);
            }
            if(checkSecurity)
            {
                StartCoroutine(CheckSecurity());
            }
            switch (_state)
            {
                case 0:
                    if (!_attack)
                    {
                        StopCoroutine(coroutine);
                    }
                    _attack = true;
                    break;
                case 1:
                    if (_attack)
                    {
                        coroutine = StartCoroutine(Attack());
                    }
                    break;
            }
    }
    private IEnumerator Attack()
    {
        _attack = false;
        yield return new WaitForSeconds(1f/_attackSpeed);
        Instantiate(_crossbowArrow, _transformCrossbow.position,Quaternion.identity);
        coroutine = StartCoroutine(Attack());
    }
    private int QuantityEnemies()
    {
        int quantityEnemies = 0;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(gameObject.transform.position.x+(GridController.Instance.WidthGrid / 2 + GridController.Instance.CentreGrid.x - gameObject.transform.position.x)/2,gameObject.transform.position.y),
             new Vector2(GridController.Instance.WidthGrid/2+ GridController.Instance.CentreGrid.x-gameObject.transform.position.x, .2f),0);
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
        if (size >= 1)
        {
            _state = 1;
        }
        else
        {
            _state = 0;
        }
    }
    private IEnumerator CheckSecurity()
    {
        checkSecurity = false;
        yield return new WaitForSeconds(.25f);
        CheckEnemy();
        checkSecurity = true;
    }
}
