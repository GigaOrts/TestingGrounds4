using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float powerUpStrength;
    public GameObject powerupIndicator;
    public GameObject rocketPrefab;
    public PowerUpType currentPowerup = PowerUpType.None;

    private bool hasPowerup;
    private float forwardInput;

    private GameObject tmpRocket;
    private GameObject focalPoint;
    private Coroutine powerupCountdown;
    private Rigidbody playerRb;
    private Vector3 indicatorOffset = new Vector3(0f, -0.5f, 0f);

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
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
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            Debug.Log("huy");
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
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

        hasPowerup = false;
        currentPowerup = PowerUpType.None;
        powerupIndicator.SetActive(false);
    }
}
