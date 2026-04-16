using UnityEngine;

public class Enemylvl3 : MonoBehaviour
{
    [Header("Detection & Movement")]
    public float attackRange = 1.5f;
    public float chaseRange = 5f;
    public float moveSpeed = 2f;
    public LayerMask playerLayer;

    [Header("Patrol Settings")]
    public float patrolRadius = 3f;
    private Vector2 startPos;
    private Vector2 patrolTarget;

    [Header("Combat Settings")]
    public float attackCooldown = 2f;
    public int damage = 10;
    private float lastAttackTime = 0f;

    private Animator animator;
    private Transform player;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        SetNewPatrolTarget();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            
            TryAttack();
        }
        else if (distanceToPlayer <= chaseRange)
        {
            
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    // 
    void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            StopMoving();

            if (animator != null)
                animator.SetTrigger("attack"); // Changed from "Attack" to "attack"
        }
    }

    // 
    void StopMoving()
    {
        // If using Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        Flip(player.position.x);
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolTarget, (moveSpeed * 0.5f) * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolTarget) < 0.1f)
            SetNewPatrolTarget();

        Flip(patrolTarget.x);
    }

    void SetNewPatrolTarget()
    {
        patrolTarget = startPos + Random.insideUnitCircle * patrolRadius;
    }

    public void HitPlayer()
    {
        // We use a circle at the enemy's position. 
        // Increase attackRange in the inspector if the enemy is missing.
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (hitPlayer != null)
        {
            // Try to get the Health script from the player
            Health playerHealth = hitPlayer.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Enemy successfully damaged the Player!");
            }
            else
            {
                Debug.LogWarning("Enemy touched Player, but Player is missing the Health script!");
            }
        }
        else
        {
            Debug.Log("Enemy swung but hit nothing.");
        }
    }

    void Flip(float targetX)
    {
        if (targetX > transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    public void TakeDamage()
    {
        if (isDead) return;
        animator.SetTrigger("hurt");
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}