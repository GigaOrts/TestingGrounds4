using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float SpawnRange = 9f;

    public int waveNumber = 1;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private Vector3 powerupOffset = new Vector3(0f, 0.5f, 0f);
    private int enemyCount;

    private void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPoint() + powerupOffset, powerupPrefab.transform.rotation);
    }
    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPoint() + powerupOffset, powerupPrefab.transform.rotation);
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPoint(), enemyPrefab.transform.rotation);
        }

        waveNumber++;
    }

    private Vector3 GenerateSpawnPoint()
    {
        float spawnPosX = Random.Range(-SpawnRange, SpawnRange);
        float spawnPosZ = Random.Range(-SpawnRange, SpawnRange);
        return new Vector3(spawnPosX, 0f, spawnPosZ);
    }
}
