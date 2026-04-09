using UnityEngine;

public class Enemylvl3 : MonoBehaviour
{
    [Header("Detection Settings")]
    public float attackRange = 1.5f;
    public LayerMask playerLayer;
    public Transform attackPoint;

    [Header("Combat Settings")]
    public float attackCooldown = 2f;
    private float nextAttackTime = 0f;

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        // Check if player is in range
        bool playerInRange = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (playerInRange && Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void AttackPlayer()
    {
        // Randomly choose between attack and a combo
        if (Random.value > 0.7f)
        {
            anim.SetTrigger("combo");
        }
        else
        {
            anim.SetTrigger("attack");
        }
    }

    // Call this from your health script
    public void TakeDamage()
    {
        anim.SetTrigger("hurt");
    }

    // Call this when health reaches 0
    public void Die()
    {
        isDead = true;
        anim.SetTrigger("die");
    }

    // Visualizes the attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}