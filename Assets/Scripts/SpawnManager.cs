using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private List<GameObject> _powerups;
    [SerializeField]
    private int[] _table =
    {
        200, //Triple Shot
        200, //Speed
        200, //Shields
        200, //Reload
        100, //Health
        100 //Lunar
    };
    
    private bool _stopSpawning = false;
    [SerializeField]
    private int _total;
    [SerializeField]
    private int _randomNumber;

    public void StartSpawning()
    {
        StopCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
           Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform; 
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                int randomPowerUp = Random.Range(0, 6);
                Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 8));
                PowerupSpawnTable();
        }
    }

    public void PowerupSpawnTable()
    {
        foreach (var item in _table)
        {
            _total += item;
            _total = 1000;
        }

        _randomNumber = Random.Range(0, _total);

        for (int i = 0; i < _table.Length; i++)
        {
            if (_randomNumber <= _table[i])
            {
                _powerups[i].SetActive(true);
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
