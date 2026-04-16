//using UnityEngine;

//public class Boss2Health : MonoBehaviour
//{
//    [SerializeField] private int maxHealth = 10;

//    private int currentHealth;
//    private Animator animator;
//    private bool isDead = false;

//    private void Awake()
//    {
//        currentHealth = maxHealth;
//        animator = GetComponent<Animator>();
//    }

//    public void TakeDamage(int damage)
//    {
//        if (isDead) return;

//        currentHealth -= damage;
//        currentHealth = Mathf.Max(currentHealth, 0);

//        // --- HEALTH STATUS VISUALIZATION ---
//        string healthBar = "";
//        for (int i = 0; i < maxHealth; i++)
//        {
//            healthBar += (i < currentHealth) ? "■" : "□";
//        }

//        Debug.Log($"{gameObject.name} Health: {currentHealth}/{maxHealth} {healthBar}");
//        // -----------------------------------

//        if (currentHealth > 0)
//        {
//            animator.SetTrigger("Hit");
//        }
//        else
//        {
//            Die();
//        }
//    }

//    private void Die()
//    {
//        if (isDead) return;
//        isDead = true;

//        Debug.Log($"<color=red>{gameObject.name} has died!</color>");
//        animator.SetTrigger("Die");

//        Collider2D col = GetComponent<Collider2D>();
//        if (col != null)
//            col.enabled = false;

//        Rigidbody2D rb = GetComponent<Rigidbody2D>();
//        if (rb != null)
//        {
//            rb.linearVelocity = Vector2.zero;
//            rb.bodyType = RigidbodyType2D.Kinematic;
//        }

//        EnemyAI ai = GetComponent<EnemyAI>();
//        if (ai != null)
//            ai.enabled = false;

//        Destroy(gameObject, 2f);
//    }

//    public int GetCurrentHealth()
//    {
//        return currentHealth;
//    }

//    public int GetMaxHealth()
//    {
//        return maxHealth;
//    }

//    public bool IsDead()
//    {
//        return isDead;
//    }
//}

using UnityEngine;

public class Boss2Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // --- HEALTH STATUS VISUALIZATION ---
        string healthBar = "";
        for (int i = 0; i < maxHealth; i++)
        {
            // Adds a filled square for current health and an empty one for lost health
            healthBar += (i < currentHealth) ? "■" : "□";
        }

        Debug.Log($"{gameObject.name} Health: {currentHealth}/{maxHealth} {healthBar}");
        // ------------------------------------

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"<color=red>{gameObject.name} has died!</color>");
        animator.SetTrigger("Die");

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Note: In newer Unity versions, use .linearVelocity. 
            // In older versions, use .velocity.
            rb.linearVelocity = Vector2.zero;
        }

        EnemyAI ai = GetComponent<EnemyAI>();
        if (ai != null)
            ai.enabled = false;

        Destroy(gameObject, 1.2f);
    }
}