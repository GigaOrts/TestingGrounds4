using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private GameObject focalPoint;
    private Rigidbody playerRb;
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
    }
}
