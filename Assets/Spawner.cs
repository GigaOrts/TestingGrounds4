using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float SpawnRange = 9f;

    public GameObject objectPrefab;

    void Start()
    {
        Vector3 spawnPosition = GenerateSpawnPoint();
        Instantiate(objectPrefab, spawnPosition, objectPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPoint()
    {
        float spawnPosX = Random.Range(-SpawnRange, SpawnRange);
        float spawnPosZ = Random.Range(-SpawnRange, SpawnRange);
        return new Vector3(spawnPosX, 0f, spawnPosZ);
    }
}
