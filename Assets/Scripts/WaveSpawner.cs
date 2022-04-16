using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] _waves;
    [SerializeField] private GameObject _spawners;
    [SerializeField] private float _spawnDistance;

    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;
    private int _indexNextWave;

    public int NumberOfLiveEnemies;

    public Timer Timer;

    private void Start()
    {
        _indexNextWave = 1;
        _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
        //SpawnersPosition(GridController.Instance.CentreGrid, GridController.Instance.HorizontalCount, GridController.Instance.HeightGrid);
        LaunchWave();
    }
    private void FixedUpdate()
    {
        if (NumberOfLiveEnemies==0 && _currentWaveIndex==_indexNextWave)
        {
            _indexNextWave++;
            Timer.CountingEnemies();
            LaunchWave();
        }
    }

    //private IEnumerator SpawnEnemyInWave()
    //{
    //    if(_enemiesLeftToSpawn > 0)
    //    {
    //        yield return null;
    //        if (_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy != null &&
    //           _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner!=null)
    //        {
    //            StartCoroutine(CreationEnemy(_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy,
    //                _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner.transform,
    //                _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].SpawnDelay));
    //        }
    //        _enemiesLeftToSpawn--;
    //        _currentEnemyIndex++;
    //        StartCoroutine(SpawnEnemyInWave());
    //        //for(int i=0;i<_enemiesLeftToSpawn;i++)
    //        //{
    //        //    if (_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy != null &&
    //        //   _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner != null)
    //        //    {
    //        //        StartCoroutine(CreationEnemy(_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy,
    //        //            _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner.transform,
    //        //            _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].SpawnDelay));
    //        //    }
    //        //    _currentEnemyIndex++;
    //        //}
    //    }
    //    else
    //    {
    //        if (_currentWaveIndex < _waves.Length - 1)
    //        {
    //            _currentWaveIndex++;
    //            _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
    //            _currentEnemyIndex = 0;
    //        }
    //    }
    //}
    private IEnumerator CreationEnemy(GameObject enemy,Transform transform,float spawnDelay)
    {
        NumberOfLiveEnemies++;
        GameObject instantiateEnemy;
        yield return new WaitForSeconds(spawnDelay);
        instantiateEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        instantiateEnemy.GetComponent<Entity>().waveSpawner= gameObject.GetComponent<WaveSpawner>();
    }

    public void LaunchWave()
    {
        //StartCoroutine(SpawnEnemyInWave());
        for (int i = 0; i < _enemiesLeftToSpawn; i++)
        {
            if (_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy != null &&
           _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner != null)
            {
                StartCoroutine(CreationEnemy(_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy,
                    _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner.transform,
                    _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].SpawnDelay));
            }
            _currentEnemyIndex++;
        }
        if (_currentWaveIndex < _waves.Length - 1)
        {
            _currentWaveIndex++;
            _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
            _currentEnemyIndex = 0;
        }
    }
    public void SpawnersPosition(Vector2 vector2, int col, float height, float scale = 0)
    {
        _spawners.transform.position = new Vector3(_spawnDistance + scale / 2, vector2.y, 0);
        for (int i = 0; i < 6; i++)
        {
            _spawners.transform.GetChild(i).position = new Vector3(_spawnDistance + scale / 2, (vector2.y - height / 2) + ((height * (2 * i + 1)) / (col * 2)), 0);
        }
    }
    public Waves[] GetWaves()
    {
        return _waves;
    }
    public int GetCurrentWaveIndex()
    {
        return _currentWaveIndex;
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
