using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

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
            waveNumber++;

            SpawnPowerup();

            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
        }
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(),
            miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }

    private void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;

        if (bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }

        GameObject boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
    }

    private void SpawnPowerup()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);

        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition() + powerupOffset,
            powerupPrefabs[randomPowerup].transform.rotation);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);

            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), 
                enemyPrefabs[randomEnemy].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0f, spawnPosZ);
    }
}
