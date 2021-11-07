using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject HPBar;
    [SerializeField] Image Hp;

    public bool Active;
    public WaveSpawner waveSpawner;

    protected int _healthMax;
    protected int _healthNow;
    protected float _damageAbsorption=1;

    private void Awake()
    {
        if (HPBar != null)
        {
            HPBar.SetActive(false);
        }
    }
    public  void DealingDamage(int damage)
    {
        _healthNow = _healthNow - (int)(damage*_damageAbsorption);
        if(Hp!=null)
        {
            Hp.fillAmount = (float)_healthNow / (float)_healthMax;
        }
        if (HPBar != null)
        {
            if (_healthNow < _healthMax)
            {
                HPBar.SetActive(true);
            }
        }
    }
}
