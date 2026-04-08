using UnityEngine;

public class GobinHealth : MonoBehaviour
{
    [Header("Damage to Player")]
    public int damage;
    public Health playerHealth;

    [Header("Enemy Health")]
    public int maxHealth = 5;
    private int currentHealth;
    private bool isDead = false;

    private Animator anim;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
    }

    // 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
    }

    // 
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Enemy HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (anim != null)
            anim.SetTrigger("die");

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 1f);
    }
}