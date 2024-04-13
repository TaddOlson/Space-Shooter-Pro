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
    private GameObject[] _powerups;

    [SerializeField]
    private int[] _table =
    {
        20, //Triple Shot
        20, //Speed
        20, //Shields
        20, //Reload
        10, //Health
        10 //Lunar
    };
    
    private bool _stopSpawning = false;
    [SerializeField]
    private int _total;
    [SerializeField]
    private int _randomNumber;

    public void Start()
    {
        //tally the total weight
        //draw a random number between 0 and the total weight (100).
        foreach(var item in _table)
        {
            _total += item;
        }

        _randomNumber = Random.Range(0, _total);

        for (int i = 0; i < _table.Length; i++)
        {
            if(_randomNumber <= _table [i])
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

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
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
        //pick random number between 0 - 6
        //if number is 5
        //pick a random number between 0 - 6

        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
                int randomPowerUp = Random.Range(0, 6);
                Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}
