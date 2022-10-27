using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int waveNumber = 1;
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;

    private Vector3 powerupOffset = new Vector3(0f, 0.5f, 0f);
    private float spawnRange = 9f;
    private int enemyCount;

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }

    private void SpawnPowerup()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);

        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPoint() + powerupOffset,
            powerupPrefabs[randomPowerup].transform.rotation);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);

            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPoint(), 
                enemyPrefabs[randomEnemy].transform.rotation);
        }

        waveNumber++;
    }

    private Vector3 GenerateSpawnPoint()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0f, spawnPosZ);
    }
}
