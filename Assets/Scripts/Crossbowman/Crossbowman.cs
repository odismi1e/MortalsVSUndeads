using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbowman : Health
{
    [SerializeField] private Transform _transformCrossbow;
    [SerializeField] private GameObject _crossbowArrow;

    private bool _attack = true;
    private bool checkSecurity = true;

    private Coroutine coroutine;

    private float _attackSpeed;
    private int _state;
    private void Start()
    {
        _healthMax = UnitManager.Instance.SwordsmanHealth;
        _healthNow = _healthMax;
        _attackSpeed = UnitManager.Instance.CrossbowmanAttackSpeed;
        StartCoroutine(CheckSecurity());
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            if (_healthNow <= 0)
            {
                Destroy(gameObject);
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
    }
    private IEnumerator Attack()
    {
        _attack = false;
        yield return new WaitForSeconds(_attackSpeed);
        Instantiate(_crossbowArrow, _transformCrossbow.position,Quaternion.identity);
        coroutine = StartCoroutine(Attack());
    }
    private int QuantityEnemies()
    {
        int quantityEnemies = 0;
        Collider[] colliders = Physics.OverlapBox(new Vector3(GridController.Instance.CentreGrid.x,gameObject.transform.position.y,0)
            , new Vector3(GridController.Instance.WidthGrid, .2f, 0.2f));
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
        if (sie >= 1)
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
        yield return new WaitForSeconds(.5f);
        CheckEnemy();
        checkSecurity = true;
    }
}
