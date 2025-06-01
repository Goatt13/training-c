using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float lifeTime = 5f;
    private Vector2 moveDirection;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the bullet after its lifetime
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)moveDirection * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destroy the bullet on collision with an enemy
        }
    }
}
