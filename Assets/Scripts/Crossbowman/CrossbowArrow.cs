using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowArrow : MonoBehaviour
{
    private float _damage;
    private float _crossbowArrowSpeed;
    private Transform _tr;
    private int _numberGoals=1;
    private void Awake()
    {
        _tr = gameObject.transform;
        _damage = GameManager.Instance.UnitManager.CrossbowArrowDamage;
        _crossbowArrowSpeed = GameManager.Instance.UnitManager.CrossbowArrowSpeed;
    }
    private void FixedUpdate()
    {
        if (_tr.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.currentResolution.width,0, 10)).x+1)
        {
            Destroy(gameObject);
        }
        _tr.position = new Vector3(_tr.position.x+Time.fixedDeltaTime* _crossbowArrowSpeed, _tr.position.y,_tr.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Enemy")
        {
            if (_numberGoals > 0)
            {
                _numberGoals--;
                other.gameObject.GetComponent<Entity>().ApplyDamage(_damage, GameManager.Instance.UnitManager.CrossbowmanIgnoringArmor);
                Destroy(gameObject);
            }
        }
    }
}
