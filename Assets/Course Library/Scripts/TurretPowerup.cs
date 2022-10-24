using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPowerup : MonoBehaviour
{
    private const float bulletSpeed = 3f;

    public Transform bulletSpawnPosition;
    public GameObject bulletPrefab;

    private List<Enemy> enemies;

    private void Start()
    {
        enemies = new List<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup Turret"))
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.3f);

        enemies.AddRange(FindObjectsOfType<Enemy>());

        for (int i = 0; i < enemies.Capacity; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletPrefab.transform.rotation);
            Vector3 bulletDirection = (enemies[i].transform.position - bullet.transform.position).normalized;
            //создать корутину для перемещения пули до цели, MoveBullet(GameObject bullet, Enemy enemy);
            // Каждой пуле нужен апдейт
        }

        
    }
}
