using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy _targetEnemy;
    private float speed = 5f;

    private void Update()
    {
        if (_targetEnemy != null)
        {
            Vector3 directionToTarget = _targetEnemy.transform.position - transform.position;
            transform.Translate(directionToTarget * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Destroy(enemy.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetTarget(Enemy target)
    {
        _targetEnemy = target;
    }
}
