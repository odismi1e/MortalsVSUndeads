using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
     private float _seconds;
     private int _mana;
     private int _startMana;
    void Start()
    {
        ResourceCounter.Instance.Resources = _startMana;
        ResourceCounter.Instance.RecourcesText.text = ResourceCounter.Instance.Resources.ToString();
        StartCoroutine(AddendumMana());
    }
    private void Awake()
    {
        _seconds = GameManager.Instance.Seconds;
        _mana = GameManager.Instance.Mana;
        _startMana = GameManager.Instance.StartMana;
    }
    private IEnumerator AddendumMana()
    {
        yield return new WaitForSeconds(_seconds);
        ResourceCounter.Instance.ReceiveResources(_mana);
        StartCoroutine(AddendumMana());
    }
}
