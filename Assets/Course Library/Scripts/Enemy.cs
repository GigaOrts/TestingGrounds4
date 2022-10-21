using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private Rigidbody enemyRb;
    private GameObject target;
    private Vector3 lookDirection;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        target = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update()
    {
        if (transform.position.y < -10f)
            Destroy(gameObject);

        lookDirection = (target.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }
}
