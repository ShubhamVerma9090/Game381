using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public float Hitpoints = 5f;
    public float MaxHitpoints = 5;
    public HealthManager healthbar;
    public Transform player;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Detection")]
    private float distanceToPlayer;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;

    [Header("Attack")]
    public float attackCooldown = 2f;
    private float lastAttackTime = -Mathf.Infinity;
    
    [Header("Player Combat")]
    public float enemyDamage = 1f;

    private bool isDead = false;

    [SerializeField] private float damage;

    void Start()
    {
        Hitpoints = MaxHitpoints;
        if (healthbar != null) healthbar.SetHealth(Hitpoints, MaxHitpoints);
    }

    void Update()
    {
        if (isDead || player == null) return;

        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackRange) TryAttack();
            else ChasePlayer();
        }
        else StopMoving();

        UpdateAnimation();
        FlipSprite();
    }

    public void TakeHit(float damageAmount)
    {
        if (isDead) return;

        Hitpoints -= damageAmount;

        if (healthbar != null)
            healthbar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        rb.linearVelocity = Vector2.zero;

        // Optional: disable collider or destroy after a delay
        Destroy(gameObject, 1f);
    }


    void ChasePlayer()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    void StopMoving()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            StopMoving();
            if (animator != null) animator.SetTrigger("Attack"); // Matches "Attack" trigger
        }
    }

    void UpdateAnimation()
    {
        bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        if (animator != null) animator.SetBool("isMoving", moving); // Matches "isMoving" bool
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the thing we bumped into is the Player
        if (collision.CompareTag("Player"))
        {
            // Check if the player has a Health script (you'll need to create one for the player too!)
            // PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            // if (playerHealth != null) playerHealth.TakeDamage(enemyDamage);

            collision.GetComponent<Health>().TakeDamage(damage);
            Debug.Log("Player took damage!");
        }
    }


    void FlipSprite()
    {
        if (player.position.x > transform.position.x) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }
}