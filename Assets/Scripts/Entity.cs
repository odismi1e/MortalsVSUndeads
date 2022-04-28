using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected AnimationClip[] _animations;

    protected float _healthMax;
    protected float _healthNow;
    protected float _damageAbsorption = 0;

    protected float _damage;
    protected float _attackSpeed;
    protected float _speed=0;

    [SerializeField] private GameObject HPBar;
    [SerializeField] private Transform Hp;

    public WaveSpawner waveSpawner;

    private void Awake()
    {
        if (HPBar != null)
        {
            HPBar.SetActive(false);
        }
    }
    public void ApplyDamage(float damage,float ignoringArmor=0)
    {
        if (ignoringArmor > 0)
        {
            _healthNow = _healthNow - damage * (1 - (_damageAbsorption / 10f) * (1 - ignoringArmor / 100f));
        }
        else
        {
            _healthNow = _healthNow - damage * (1 - _damageAbsorption / 10f);
        }

        HpBar();

        //if (HPBar != null)
        //{
        //    if (_healthNow < _healthMax)
        //    {
        //        HPBar.SetActive(true);
        //    }
        //}
    }
    public void HpBar()
    {
        if (Hp != null)
        {
            if (_healthNow < _healthMax)
            {
                HPBar.SetActive(true);
            }
            if (_healthNow / _healthMax < 0)
            {
                Hp.localScale = new Vector3(0, 1, 1);
            }
            else
            {
                Hp.localScale = new Vector3(_healthNow / _healthMax, 1, 1);
            }
            if(_healthNow==_healthMax)
            {
                HPBar.SetActive(false);
            }
            else
            {
                HPBar.SetActive(true);
            }  
            if(_healthNow <= 0)
            {
                HPBar.SetActive(false);
            }
        }
    }
    public float GetHealthNow()
    {
        return _healthNow;
    }
    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }
    public float GetSpeed()
    {
        return _speed;
    }
    public GameObject GetHPBar()
    {
        return HPBar;
    }
    public void SetSpeed(float value)
    {
        if(value<=0)
        {
            _speed = 0;
        }
        else
        {
            _speed = value;
        }
    }
    public void SetHealtNow(float value)
    {
        if( value >= _healthMax)
        {
            _healthNow = _healthMax;
        }
        else
        {
            _healthNow = value;
        }
    }
    public void SetAttackSpeed(float value)
    {
        if(value<=GameManager.Instance.Global.MinAttackSpeed)
        {
            _attackSpeed = GameManager.Instance.Global.MinAttackSpeed;
        }
        else
        {
            _attackSpeed = value;
        }
    }
}
