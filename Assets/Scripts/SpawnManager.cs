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
    private GameObject _rarePowerup;
    

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnRarePowerupRoutine());
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
                int randomPowerUp = Random.Range(0, 5);
                Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    IEnumerator SpawnRarePowerupRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawning == false)
        {
            Vector3 randomSpawnLocation = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_rarePowerup, randomSpawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(15f, 30f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}
