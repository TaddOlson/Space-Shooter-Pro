using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private List<GameObject> _powerups;
    [SerializeField]
    private int[] _table =
    {
        142, //Triple Shot
        142, //Speed
        142, //Shields
        142, //Reload
        142, //Health
        142, //Lunar
        142, //Player Chill
    };

    private bool _stopSpawning = false;
    [SerializeField]
    private int _total;
    [SerializeField]
    private int _randomNumber;
    [SerializeField]
    private GameObject[] _enemyAngleSpawn;

    [SerializeField]
    private int _enemySpawnedCount = 0; // current number of enemies spawned in wave
    [SerializeField]
    private int _enemyPerSpawn = 5; // number of enemies to spawn per wave
    [SerializeField]
    private int _currentWave = 1; // current wave number
    [SerializeField]
    private int _initialEnemySpawnCount = 5;
    [SerializeField]
    private int _enemiesPerWaveIncrease = 5;
    [SerializeField]
    private int _totalEnemyWaveCount = 7;

    private UIManager _uiManager;

    public void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    public void StartSpawning()
    {
        foreach (var item in _table)
        {
            _total += item;
        }

        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnEnemyWaveRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            EnemySpawn();
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void EnemySpawn()
    {

        Vector3 normalToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
        int randomAngleSpawn = Random.Range(0, 2);
        int randomEnemy = Random.Range(0, 2);
        
        if (randomEnemy == 0)
        {
            GameObject firstEnemy = Instantiate(_enemyPrefab[0], normalToSpawn, Quaternion.identity);
            firstEnemy.transform.parent = _enemyContainer.transform;
        }
        else if (randomEnemy == 1)
        {
            GameObject enemy = Instantiate(_enemyPrefab[1], _enemyAngleSpawn[randomAngleSpawn].transform.position, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            EnemyDiagonal enemyDiagonal = enemy.GetComponent<EnemyDiagonal>();
            enemyDiagonal.EnemyDirection(randomAngleSpawn);
            enemyDiagonal.transform.rotation = _enemyAngleSpawn[randomAngleSpawn].transform.rotation;
        }

        _enemySpawnedCount++;

        if (_enemySpawnedCount >= 5)
        {
            _stopSpawning = true;
        }

    }

    IEnumerator SpawnEnemyWaveRoutine()
    {
        while (_currentWave <= _totalEnemyWaveCount)
        {
            _uiManager.UpdateWave(_currentWave);

            yield return new WaitForSeconds(20.5f);

            _stopSpawning = false;
            _enemySpawnedCount = 0;

            _initialEnemySpawnCount += _enemiesPerWaveIncrease;

            StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpawnPowerupRoutine());

            _currentWave++;
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            PowerupSpawnTable();
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void PowerupSpawnTable()
    {
        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
        _randomNumber = Random.Range(0, _total);

        for (int i = 0; i < _table.Length; i++)
        {
            if (_randomNumber <= _table[i])
            {
                Instantiate(_powerups[i], posToSpawn, Quaternion.identity);
                return;
            }
            else
            {
                _randomNumber -= _table[i];
            }
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    
    
}
