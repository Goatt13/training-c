using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    public int health = 10;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Dummy took damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Dummy destroyed.");
        Destroy(gameObject);
    }
}