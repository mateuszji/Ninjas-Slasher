using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private Enemy[] enemiesPrefabs;
    [SerializeField, Range(1, 5)]
    private float timeBetweenSpawn;
    [SerializeField, Range(0, 10)]
    private float delayBeforeFirstSpawn;

    private void Start()
    {
        StartCoroutine(FirstEnemySpawn());
    }

    IEnumerator FirstEnemySpawn()
    {
        yield return new WaitForSeconds(delayBeforeFirstSpawn);
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-13, 13), Random.Range(-7, 4), 10);
            int randomEnemy = Random.Range(0, enemiesPrefabs.Length);
            GameObject newEnemy = Instantiate(enemiesPrefabs[randomEnemy].gameObject, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
}
