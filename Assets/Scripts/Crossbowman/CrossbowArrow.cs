using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowArrow : MonoBehaviour
{
    private int _damage;
    private float _crossbowArrowSpeed;
    private Transform _tr;
    private int _numberGoals=1;
    private void Awake()
    {
        _tr = gameObject.transform;
        _damage = UnitManager.Instance.CrossbowArrowDamage;
        _crossbowArrowSpeed = UnitManager.Instance.CrossbowArrowSpeed;
    }
    private void FixedUpdate()
    {
        if(_tr.position.x>GridController.Instance.CentreGrid.x+1+GridController.Instance.WidthGrid/2)
        {
            Destroy(gameObject);
        }
        _tr.position = new Vector3(_tr.position.x+0.02f*_crossbowArrowSpeed, _tr.position.y,_tr.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Enemy")
        {
            if (_numberGoals > 0)
            {
                _numberGoals--;
                other.gameObject.GetComponent<Health>().DealingDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
