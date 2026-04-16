using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public float Hitpoints = 5f;
    public float MaxHitpoints = 5f;
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
    [SerializeField] private float damage = 1f;

    private bool isDead = false;

    private void Start()
    {
        Hitpoints = MaxHitpoints;

        if (healthbar != null)
            healthbar.SetHealth(Hitpoints, MaxHitpoints);

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDead || player == null) return;

        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackRange)
                TryAttack();
            else
                ChasePlayer();
        }
        else
        {
            StopMoving();
        }

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
            Die();
    }

    private void Die()
    {
        isDead = true;

        if (animator != null)
            animator.SetTrigger("Die");

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Optional: disable collider so it stops interacting
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Destroy(gameObject, 1f);
    }

    private void ChasePlayer()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    private void StopMoving()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    private void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            StopMoving();

            if (animator != null)
                animator.SetTrigger("Attack");
        }
    }

    // THIS gets called by the Animation Event
    public void DealDamageToPlayer()
    {
        if (isDead || player == null) return;

        float currentDistance = Vector2.Distance(transform.position, player.position);

        if (currentDistance <= attackRange)
        {
            Health playerHealth = player.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Enemy hit player through animation event.");
            }
        }
    }

    private void UpdateAnimation()
    {
        bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;

        if (animator != null)
            animator.SetBool("isMoving", moving);
    }

    private void FlipSprite()
    {
        if (player == null || spriteRenderer == null) return;

        spriteRenderer.flipX = player.position.x < transform.position.x;
    }
}