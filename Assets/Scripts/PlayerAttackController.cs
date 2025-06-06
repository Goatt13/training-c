using UnityEngine;

public enum AttackType
{
    Melee,
    Ranged,
    Mage
}

public class PlayerAttackController : MonoBehaviour
{
    public AttackType currentAttackType = AttackType.Melee;

    [Header("General Settings")]
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;

    [Header("Ranged Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    [Header("Mage Settings")]
    public GameObject magicEffectPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol mouse tıkı = saldırı
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        switch (currentAttackType)
        {
            case AttackType.Melee:
                PerformMeleeAttack();
                break;
            case AttackType.Ranged:
                PerformRangedAttack();
                break;
            case AttackType.Mage:
                PerformMageAttack();
                break;
        }
    }

    void PerformMeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            Debug.Log("Hit enemy with melee: " + enemy.name);

            DummyTarget dummy = enemy.GetComponent<DummyTarget>();
            if (dummy != null)
            {
                dummy.TakeDamage(1);
            }
        }
    }

    void PerformRangedAttack()
    {
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    void PerformMageAttack()
    {
        Instantiate(magicEffectPrefab, attackPoint.position, Quaternion.identity);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            Debug.Log("Hit enemy with magic: " + enemy.name);
            // enemy.GetComponent<EnemyHealth>().TakeDamage(2);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}