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
        199, //Triple Shot
        199, //Speed
        199, //Shields
        198, //Reload
        130, //Health
        75 //Lunar
    };

    private bool _stopSpawning = false;
    [SerializeField]
    private int _total;
    [SerializeField]
    private int _randomNumber;
    [SerializeField]
    private GameObject[] _enemyAngleSpawn;

    public void StartSpawning()
    {
        foreach (var item in _table)
        {
            _total += item;
        }

        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomEnemy = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(_enemyPrefab[randomEnemy], posToSpawn, Quaternion.identity);
            EnemySpawn();
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void EnemySpawn()
    {

        int randomAngleSpawn = Random.Range(0, 2);

        if(_enemyPrefab[0])
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
        else if(_enemyPrefab[1])
        {
            _enemyAngleSpawn[randomAngleSpawn].length;
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
