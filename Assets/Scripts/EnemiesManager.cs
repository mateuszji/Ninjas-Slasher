using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    #region Singleton

    private static EnemiesManager _instance;
    public static EnemiesManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion
    [SerializeField]
    private Enemy[] enemiesPrefabs;
    [SerializeField, Range(1, 5)]
    private float timeBetweenSpawn;
    [SerializeField, Range(0, 10)]
    private float delayBeforeFirstSpawn;
    public ParticleSystem destroyEffect;
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
            Vector3 spawnPos;
            do
                spawnPos = new Vector3(Random.Range(-13, 13), Random.Range(-7, 4), 10);
            while (!IsSafeToSpawn(spawnPos));

            int randomEnemy = Random.Range(0, enemiesPrefabs.Length);
            GameObject newEnemy = Instantiate(enemiesPrefabs[randomEnemy].gameObject, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    private bool IsSafeToSpawn(Vector3 spawnPos)
    {
        bool safe = true;
        for(int i = 0; i < PlayersManager.Instance.players.Length; i++)
        {
            Vector3 playerPos = PlayersManager.Instance.players[i].gameObject.transform.position;
            if(Vector3.Distance(spawnPos, playerPos) < 10f)
            {
                safe = false;
                break;
            }
        }
        return safe;
    }
}
