using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed;
    private float horizontalInput;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
