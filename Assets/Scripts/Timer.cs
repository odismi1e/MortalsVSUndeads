using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image _timer;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private int _updateFrequency;
    [SerializeField] private float _timeUpdate;
    [SerializeField] private GameObject[] _WaveTimer; 

    private int _enemiesQuantity;
    private int _waveCost;
    private int _currentWaveIndex=0;

    private void Start()
    {
        _waveCost = _waveSpawner.GetWaves().Length;
        for (int i = 1; i < _waveCost; i++)
        {
            _WaveTimer[i - 1].SetActive(true);
            _WaveTimer[i - 1].transform.Rotate(new Vector3(0,0,-i*180f/_waveCost));
        }
        if(_waveCost>0)
        {
            _enemiesQuantity = _waveSpawner.GetWaves()[0].WaveSettings.Length;
        }
        _currentWaveIndex = 0;
    }
    public void CountingEnemies()
    {
            _currentWaveIndex++;
            _enemiesQuantity = _waveSpawner.GetWaves()[_currentWaveIndex].WaveSettings.Length; 
    }
    public IEnumerator TimerDeltaCoroutine()
    {
        int enemiesQuantity = _enemiesQuantity;
        for(int i=0; i<_updateFrequency;i++)
        {
            _timer.fillAmount += (float)(1f /(float) (_waveCost*enemiesQuantity*_updateFrequency));
            yield return new WaitForSeconds(_timeUpdate / _updateFrequency);
        }
    }
    public void TimerDelta()
    {
        StartCoroutine(TimerDeltaCoroutine());
    }
}
