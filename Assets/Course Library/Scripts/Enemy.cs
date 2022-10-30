using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    public bool isBoss = false;
    public float spawnInterval;
    private float nextSpawn;

    public int miniEnemySpawnCount;
    private Spawner spawnManager;

    private Rigidbody enemyRb;
    private GameObject target;
    private Vector3 lookDirection;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        target = FindObjectOfType<PlayerController>().gameObject;

        if (isBoss)
        {
            spawnManager = FindObjectOfType<Spawner>();
        }
    }

    void Update()
    {
        lookDirection = (target.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

        if (isBoss)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
