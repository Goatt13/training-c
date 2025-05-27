using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float moveSpeed = 2f;

    void Start()
    {
        // Find the object with the "Player" tag in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Direction: player position - own position
        Vector2 direction = (player.position - transform.position).normalized;

        // Rotation
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Movement: move in that direction at constant speed
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }
}