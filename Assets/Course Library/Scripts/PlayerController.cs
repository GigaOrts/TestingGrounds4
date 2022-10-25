using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float powerUpStrength;
    public GameObject powerupIndicator;
    public GameObject turret;

    private GameObject focalPoint;
    private Rigidbody playerRb;

    private float forwardInput;
    private bool hasPowerup;
    private Vector3 indicatorOffset = new Vector3(0f, -0.5f, 0f);
    private Vector3 turretOffset = new Vector3(0f, 0.7f, 0f);

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
        turret.transform.position = transform.position + turretOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);

            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            PushAway(collision);
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7f);

        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void PushAway(Collision collision)
    {
        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
        enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
    }
}
