using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] _waves;
    [SerializeField] private GameObject _spawners;

    private GameObject _enemy;

    public int NumberOfLiveEnemies;

    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;
    private int _indexNextWave;

    private void Start()
    {
        _indexNextWave = 1;
        _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
        SpawnersPosition(GridController.Instance.CentreGrid, GridController.Instance.HorizontalCount, GridController.Instance.HeightGrid);
        LaunchWave();
    }
    private void FixedUpdate()
    {
        if (NumberOfLiveEnemies==0 && _currentWaveIndex==_indexNextWave)
        {
            _indexNextWave++;
            LaunchWave();
        }
    }

    private IEnumerator SpawnEnemyInWave()
    {
        if(_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(_waves[_currentWaveIndex]
                .WaveSettings[_currentEnemyIndex]
                .SpawnDelay);
            if (_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy != null &&
               _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner!=null)
            {
               _enemy= Instantiate(_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy,
                    _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner.transform.position, Quaternion.identity);
                _enemy.GetComponent<Health>().waveSpawner = gameObject.GetComponent<WaveSpawner>();
                NumberOfLiveEnemies++;
            }
            _enemiesLeftToSpawn--;
            _currentEnemyIndex++;
            StartCoroutine(SpawnEnemyInWave());
        }
        else
        {
            if (_currentWaveIndex < _waves.Length - 1)
            {
                _currentWaveIndex++;
                _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
                _currentEnemyIndex = 0;
            }
        }
    }

    public void LaunchWave()
    {
        StartCoroutine(SpawnEnemyInWave());
    }
    private void SpawnersPosition(Vector2 vector2,int col,float height)
    {
        _spawners.transform.position = new Vector3(37, vector2.y, 0);
        for(int i=0;i<6;i++)
        {
            _spawners.transform.GetChild(i).position = new Vector3(37,(vector2.y-height/2)+((height*(2*i+1)) / (col*2)),0);
        }
    }
}

[System.Serializable]
public class Waves 
{
    [SerializeField] private WaveSettings[] _waveSettings;
    public WaveSettings[] WaveSettings { get => _waveSettings; }
}

[System.Serializable]
public class WaveSettings
{
    [SerializeField] private GameObject _enemy;
    public GameObject Enemy { get => _enemy; }
    [SerializeField] private GameObject _neededSpawner;
    public GameObject NeededSpawner { get => _neededSpawner; }
    [SerializeField] private float _spawnDelay;
    public float SpawnDelay { get => _spawnDelay; }
}
