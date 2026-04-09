using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float CurrentSpawnedEnemies = 0;
    private float MaxSpawnedEnemies;

    public List<GameObject> enemyTypes;

    public float currentCooldown = 0;
    private float spawnCooldown;

    public List<Transform> SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        MaxSpawnedEnemies = 30;
        spawnCooldown = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown < spawnCooldown)
        {
            currentCooldown += Time.deltaTime;
        }

        if (currentCooldown >= spawnCooldown && CurrentSpawnedEnemies < MaxSpawnedEnemies)
        {
            SpawnEnemy();
            currentCooldown = 0;
            CurrentSpawnedEnemies++;
        }
    }

    void SpawnEnemy()
    {
        int enemyPoint = Random.Range(0, SpawnPoints.Count - 1);
        int enemyTypeToSpawn = Random.Range(0, enemyTypes.Count - 1);
        Instantiate(enemyTypes[enemyTypeToSpawn], SpawnPoints[enemyPoint].position, SpawnPoints[enemyPoint].rotation);
    }
}
