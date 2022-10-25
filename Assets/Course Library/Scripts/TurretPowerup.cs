using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPowerup : MonoBehaviour
{
    public Transform bulletSpawnPosition;
    public Bullet bulletPrefab;

    private List<Enemy> enemies;
    private int maxShootsCount = 3;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Shoot());
            //Destroy(gameObject);
        }
    }

    private IEnumerator Shoot()
    {
        Enemy[] tempEnemiesArray = FindObjectsOfType<Enemy>();
        enemies = new List<Enemy>();
        enemies.AddRange(tempEnemiesArray);

        for (int i = 0; i < maxShootsCount; i++)
        {
            for (int j = 0; j < enemies.Capacity; j++)
            {
                Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletPrefab.transform.rotation);
                bullet.SetTarget(enemies[j]);
            }

            yield return new WaitForSeconds(0.3f);
        }

        enemies = null;
    }
}
