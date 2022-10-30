using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float powerUpStrength;
    public GameObject powerupIndicator;
    public GameObject rocketPrefab;
    public PowerUpType currentPowerup = PowerUpType.None;

    public float hangTime = 1f;
    public float smashSpeed = 1f;
    public float explosionForce = 10f;
    public float explosionRadius = 10f;

    private bool smashing;
    private float floorY;

    private GameObject tmpRocket;
    private GameObject focalPoint;
    private Coroutine powerupCountdown;
    private Rigidbody playerRb;
    private Vector3 indicatorOffset = new Vector3(0f, -0.5f, 0f);
    private float forwardInput;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = FindObjectOfType<CameraRotation>().gameObject;
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerupIndicator.transform.position = transform.position + indicatorOffset;

        if (Input.GetKeyDown(KeyCode.F) && currentPowerup == PowerUpType.Rockets)
        {
            LaunchRockets();
        }

        if (currentPowerup == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            currentPowerup = other.gameObject.GetComponent<Powerup>().powerUpType;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerup == PowerUpType.Pushback)
        {
            PushAway(collision);
        }
    }

    private void LaunchRockets()
    {
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            Debug.Log("huy");
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            Debug.Log(tmpRocket);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    private void PushAway(Collision collision)
    {
        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
        enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7f);

        currentPowerup = PowerUpType.None;
        powerupIndicator.SetActive(false);
    }

    private IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }

        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2f);
            yield return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius,
                    0.0f, ForceMode.Impulse);
            }
        }

        smashing = false;
    }
}
