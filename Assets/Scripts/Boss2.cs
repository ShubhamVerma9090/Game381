using UnityEngine;

public class BossLevel2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;

    [Header("Boss Stats")]
    [SerializeField] private int maxHealth = 80;
    [SerializeField] private int currentHealth;
    [SerializeField] private int attack1Damage = 15;
    [SerializeField] private int attack2Damage = 25;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float chaseRange = 8f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float stopDistance = 1.2f;

    [Header("Attack Timing")]
    [SerializeField] private float attackCooldown = 2f;
    private float attackCooldownTimer;

    [Header("Attack Hitbox")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private LayerMask playerLayer;

    private bool isDead = false;
    private bool isAttacking = false;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
        if (sr == null) sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead || player == null) return;

        attackCooldownTimer -= Time.deltaTime;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            TryAttack();
        }
        else if (distanceToPlayer <= chaseRange && !isAttacking)
        {
            ChasePlayer();
        }
        else
        {
            StopMoving();
        }

        UpdateAnimation();
        FlipTowardsPlayer();
    }

    private void ChasePlayer()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);

        if (distanceX > stopDistance)
        {
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }

    private void StopMoving()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    private void TryAttack()
    {
        StopMoving();

        if (isAttacking || attackCooldownTimer > 0f) return;

        isAttacking = true;
        attackCooldownTimer = attackCooldown;

        int randomAttack = Random.Range(0, 2);

        if (randomAttack == 0)
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            anim.SetTrigger("Attack2");
        }
    }

    private void FlipTowardsPlayer()
    {
        if (player == null) return;

        if (player.position.x > transform.position.x)
            sr.flipX = false;
        else
            sr.flipX = true;
    }

    private void UpdateAnimation()
    {
        if (anim == null || rb == null) return;

        bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        anim.SetBool("IsMoving", moving);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("IsMoving", false);
        anim.SetTrigger("Die");

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        this.enabled = false;
    }

    // Call this from animation event during Attack animation
    public void DealAttack1Damage()
    {
        if (isDead) return;

        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        //if (hit != null)
        //{
        //    //PlayerHealth health = hit.GetComponent<PlayerHealth>();
        //    if (health != null)
        //    {
        //        health.TakeDamage(attack1Damage);
        //    }
        //}
    }

    // Call this from animation event during Attack2 animation
    public void DealAttack2Damage()
    {
        if (isDead) return;

        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
        //if (hit != null)
        //{
        //    PlayerHealth health = hit.GetComponent<PlayerHealth>();
        //    if (health != null)
        //    {
        //        health.TakeDamage(attack2Damage);
        //    }
        //}
    }

    // Call this at the end of attack animations with an animation event
    public void EndAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}