using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Combat")]
    [SerializeField] private int health = 3;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.15f;
    [SerializeField] private GameObject explosionEffect;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private bool isDead = false;
    private bool isKnockedBack = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void FixedUpdate()
    {
        if (player == null || isDead || isKnockedBack) return;

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;
        Vector2 movement = direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.SetRotation(angle);
    }

    public void TakeDamage(int damage, Vector2 hitDirection)
    {
        if (isDead) return;

        health -= damage;
        spriteRenderer.color = Color.red;
        Invoke(nameof(ResetColor), 0.1f);

        if (health <= 0)
        {
            ExplodeAndDie();
            return;
        }

        StartCoroutine(HandleKnockback(hitDirection));
    }

    private IEnumerator HandleKnockback(Vector2 hitDirection)
    {
        isKnockedBack = true;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(hitDirection * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        rb.linearVelocity = Vector2.zero;  // bu EN ÖNEMLİSİ
        isKnockedBack = false;
    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }

    private void ExplodeAndDie()
    {
        isDead = true;
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !isDead)
        {
            Vector2 hitDirection = ((Vector2)transform.position - (Vector2)collision.transform.position).normalized;
            TakeDamage(1, hitDirection);
            Destroy(collision.gameObject);
        }
    }
}