//////using UnityEngine;

//////public class BossLevel2 : MonoBehaviour
//////{
//////    [Header("References")]
//////    [SerializeField] private Transform player;
//////    [SerializeField] private Rigidbody2D rb;
//////    [SerializeField] private Animator anim;
//////    [SerializeField] private SpriteRenderer sr;

//////    [Header("Boss Stats")]
//////    [SerializeField] private int maxHealth = 80;
//////    [SerializeField] private int currentHealth;
//////    [SerializeField] private int attack1Damage = 15;
//////    [SerializeField] private int attack2Damage = 25;

//////    [Header("Movement")]
//////    [SerializeField] private float moveSpeed = 2.5f;
//////    [SerializeField] private float chaseRange = 8f;
//////    [SerializeField] private float attackRange = 2f;
//////    [SerializeField] private float stopDistance = 1.2f;

//////    [Header("Attack Timing")]
//////    [SerializeField] private float attackCooldown = 2f;
//////    private float attackCooldownTimer;

//////    [Header("Attack Hitbox")]
//////    [SerializeField] private Transform attackPoint;
//////    [SerializeField] private float attackRadius = 1f;
//////    [SerializeField] private LayerMask playerLayer;

//////    private bool isDead = false;
//////    private bool isAttacking = false;

//////    private void Awake()
//////    {
//////        if (rb == null) rb = GetComponent<Rigidbody2D>();
//////        if (anim == null) anim = GetComponent<Animator>();
//////        if (sr == null) sr = GetComponent<SpriteRenderer>();
//////    }

//////    private void Start()
//////    {
//////        currentHealth = maxHealth;
//////    }

//////    private void Update()
//////    {
//////        if (isDead || player == null) return;

//////        attackCooldownTimer -= Time.deltaTime;

//////        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

//////        if (distanceToPlayer <= attackRange)
//////        {
//////            TryAttack();
//////        }
//////        else if (distanceToPlayer <= chaseRange && !isAttacking)
//////        {
//////            ChasePlayer();
//////        }
//////        else
//////        {
//////            StopMoving();
//////        }

//////        UpdateAnimation();
//////        FlipTowardsPlayer();
//////    }

//////    private void ChasePlayer()
//////    {
//////        float direction = Mathf.Sign(player.position.x - transform.position.x);
//////        float distanceX = Mathf.Abs(player.position.x - transform.position.x);

//////        if (distanceX > stopDistance)
//////        {
//////            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
//////        }
//////        else
//////        {
//////            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//////        }
//////    }

//////    private void StopMoving()
//////    {
//////        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//////    }

//////    private void TryAttack()
//////    {
//////        StopMoving();

//////        if (isAttacking || attackCooldownTimer > 0f) return;

//////        isAttacking = true;
//////        attackCooldownTimer = attackCooldown;

//////        int randomAttack = Random.Range(0, 2);

//////        if (randomAttack == 0)
//////        {
//////            anim.SetTrigger("Attack");
//////        }
//////        else
//////        {
//////            anim.SetTrigger("Attack2");
//////        }
//////    }

//////    private void FlipTowardsPlayer()
//////    {
//////        if (player == null) return;

//////        if (player.position.x > transform.position.x)
//////            sr.flipX = false;
//////        else
//////            sr.flipX = true;
//////    }

//////    private void UpdateAnimation()
//////    {
//////        if (anim == null || rb == null) return;

//////        bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
//////        anim.SetBool("IsMoving", moving);
//////    }

//////    public void TakeDamage(int damage)
//////    {
//////        if (isDead) return;

//////        currentHealth -= damage;

//////        anim.SetTrigger("Hit");

//////        if (currentHealth <= 0)
//////        {
//////            Die();
//////        }
//////    }

//////    private void Die()
//////    {
//////        if (isDead) return;

//////        isDead = true;
//////        rb.linearVelocity = Vector2.zero;
//////        anim.SetBool("IsMoving", false);
//////        anim.SetTrigger("Die");

//////        Collider2D col = GetComponent<Collider2D>();
//////        if (col != null)
//////            col.enabled = false;

//////        this.enabled = false;
//////    }

//////    // Call this from animation event during Attack animation
//////    public void DealAttack1Damage()
//////    {
//////        if (isDead) return;

//////        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
//////        //if (hit != null)
//////        //{
//////        //    //PlayerHealth health = hit.GetComponent<PlayerHealth>();
//////        //    if (health != null)
//////        //    {
//////        //        health.TakeDamage(attack1Damage);
//////        //    }
//////        //}
//////    }

//////    // Call this from animation event during Attack2 animation
//////    public void DealAttack2Damage()
//////    {
//////        if (isDead) return;

//////        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
//////        //if (hit != null)
//////        //{
//////        //    PlayerHealth health = hit.GetComponent<PlayerHealth>();
//////        //    if (health != null)
//////        //    {
//////        //        health.TakeDamage(attack2Damage);
//////        //    }
//////        //}
//////    }

//////    // Call this at the end of attack animations with an animation event
//////    public void EndAttack()
//////    {
//////        isAttacking = false;
//////    }

//////    private void OnDrawGizmosSelected()
//////    {
//////        if (attackPoint != null)
//////        {
//////            Gizmos.color = Color.red;
//////            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
//////        }

//////        Gizmos.color = Color.yellow;
//////        Gizmos.DrawWireSphere(transform.position, chaseRange);

//////        Gizmos.color = Color.magenta;
//////        Gizmos.DrawWireSphere(transform.position, attackRange);
//////    }
//////}

//////using UnityEngine;

//////public class BossLevel2 : MonoBehaviour
//////{
//////    [Header("References")]
//////    [SerializeField] private Transform player;
//////    [SerializeField] private Rigidbody2D rb;
//////    [SerializeField] private Animator anim;
//////    [SerializeField] private SpriteRenderer sr;

//////    [Header("Boss Stats")]
//////    [SerializeField] private int maxHealth = 80;
//////    [SerializeField] private int currentHealth;
//////    [SerializeField] private int attack1Damage = 15;
//////    [SerializeField] private int attack2Damage = 25;

//////    [Header("Movement")]
//////    [SerializeField] private float moveSpeed = 2.5f;
//////    [SerializeField] private float chaseRange = 8f;
//////    [SerializeField] private float attackRange = 2f;
//////    [SerializeField] private float stopDistance = 1.2f;

//////    [Header("Attack Timing")]
//////    [SerializeField] private float attackCooldown = 2f;
//////    private float attackCooldownTimer;

//////    [Header("Attack Hitbox")]
//////    [SerializeField] private Transform attackPoint;
//////    [SerializeField] private float attackRadius = 1f;
//////    [SerializeField] private LayerMask playerLayer;

//////    private bool isDead = false;
//////    private bool isAttacking = false;

//////    private void Awake()
//////    {
//////        if (rb == null) rb = GetComponent<Rigidbody2D>();
//////        if (anim == null) anim = GetComponent<Animator>();
//////        if (sr == null) sr = GetComponent<SpriteRenderer>();
//////    }

//////    private void Start()
//////    {
//////        currentHealth = maxHealth;
//////    }

//////    private void Update()
//////    {
//////        if (isDead || player == null) return;

//////        attackCooldownTimer -= Time.deltaTime;

//////        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

//////        if (distanceToPlayer <= attackRange)
//////        {
//////            TryAttack();
//////        }
//////        else if (distanceToPlayer <= chaseRange && !isAttacking)
//////        {
//////            ChasePlayer();
//////        }
//////        else
//////        {
//////            StopMoving();
//////        }

//////        UpdateAnimation();
//////        FlipTowardsPlayer();
//////    }

//////    private void ChasePlayer()
//////    {
//////        float direction = Mathf.Sign(player.position.x - transform.position.x);
//////        float distanceX = Mathf.Abs(player.position.x - transform.position.x);

//////        if (distanceX > stopDistance)
//////        {
//////            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
//////        }
//////        else
//////        {
//////            StopMoving();
//////        }
//////    }

//////    private void StopMoving()
//////    {
//////        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//////    }

//////    private void TryAttack()
//////    {
//////        // 1. Check distance and cooldown status
//////        Debug.Log($"Distance to Player: {Vector2.Distance(transform.position, player.position)} | Cooldown: {attackCooldownTimer}");

//////        if (isAttacking || attackCooldownTimer > 0f)
//////        {
//////            // If this prints, the Boss is either already swinging or waiting for the timer
//////            Debug.Log($"Cannot attack. isAttacking: {isAttacking}, Timer: {attackCooldownTimer}");
//////            return;
//////        }

//////        // 2. If we reach here, the logic is firing!
//////        Debug.Log("<color=green>TRIGGERING ATTACK!</color>");

//////        StopMoving();
//////        isAttacking = true;
//////        attackCooldownTimer = attackCooldown;

//////        // Randomly pick between Attack (Attack 1) and Attack2
//////        int randomAttack = Random.Range(0, 2);

//////        if (randomAttack == 0)
//////        {
//////            Debug.Log("Playing Animation: Attack");
//////            anim.SetTrigger("Attack");
//////        }
//////        else
//////        {
//////            Debug.Log("Playing Animation: Attack2");
//////            anim.SetTrigger("Attack2");
//////        }
//////    }
//////    private void FlipTowardsPlayer()
//////    {
//////        if (player == null || isAttacking) return;

//////        // Using transform.localScale instead of flipX ensures the Hitbox moves with the Boss
//////        if (player.position.x > transform.position.x)
//////            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
//////        else
//////            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
//////    }

//////    private void UpdateAnimation()
//////    {
//////        bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
//////        anim.SetBool("IsMoving", moving);
//////    }

//////    public void TakeDamage(int damage)
//////    {
//////        if (isDead) return;

//////        currentHealth -= damage;
//////        anim.SetTrigger("Hit");

//////        if (currentHealth <= 0) Die();
//////    }

//////    private void Die()
//////    {
//////        if (isDead) return;
//////        isDead = true;

//////        rb.linearVelocity = Vector2.zero;
//////        anim.SetTrigger("Die");

//////        GetComponent<Collider2D>().enabled = false;
//////        this.enabled = false;
//////    }

//////    // --- DAMAGE LOGIC (UNCOMMENTED AND FIXED) ---

//////    public void DealAttack1Damage()
//////    {
//////        ApplyDamageToPlayer(attack1Damage);
//////    }

//////    public void DealAttack2Damage()
//////    {
//////        ApplyDamageToPlayer(attack2Damage);
//////    }

//////    private void ApplyDamageToPlayer(int damageToDeal)
//////    {
//////        if (isDead) return;

//////        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
//////        if (hit != null)
//////        {
//////            // Update "Health" to "PlayerHealth" if your player script is named differently
//////            Health playerHealth = hit.GetComponent<Health>();
//////            if (playerHealth != null)
//////            {
//////                playerHealth.TakeDamage(damageToDeal);
//////                Debug.Log($"Boss hit player for {damageToDeal} damage!");
//////            }
//////        }
//////    }

//////    public void EndAttack()
//////    {
//////        isAttacking = false;
//////    }

//////    private void OnDrawGizmosSelected()
//////    {
//////        if (attackPoint != null)
//////        {
//////            Gizmos.color = Color.red;
//////            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
//////        }
//////        Gizmos.color = Color.yellow;
//////        Gizmos.DrawWireSphere(transform.position, chaseRange);
//////    }
//////}
//////used enemycotroller to make it work but didnt work

////using UnityEngine;
////using UnityEngine.Rendering;

////public class BossLevel2 : MonoBehaviour
////{
////    [Header("References")]
////    public float Hitpoints = 5f;
////    public float MaxHitpoints = 5;
////    //public HealthManager healthbar;
////    public Transform player;
////    public Rigidbody2D rb;
////    public Animator animator;
////    public SpriteRenderer spriteRenderer;

////    [Header("Movement")]
////    public float moveSpeed = 2f;

////    [Header("Detection")]
////    private float distanceToPlayer;
////    public float detectionRange = 5f;
////    public float attackRange = 1.5f;

////    [Header("Attack")]
////    public float attackCooldown = 2f;
////    private float lastAttackTime = -Mathf.Infinity;

////    [Header("Player Combat")]
////    public float enemyDamage = 1f;

////    private bool isDead = false;

////    [SerializeField] private float damage;

////    void Start()
////    {
////        Hitpoints = MaxHitpoints;
////        //if (healthbar != null) healthbar.SetHealth(Hitpoints, MaxHitpoints);
////    }

////    void Update()
////    {
////        if (isDead || player == null) return;

////        distanceToPlayer = Vector2.Distance(transform.position, player.position);

////        if (distanceToPlayer <= detectionRange)
////        {
////            if (distanceToPlayer <= attackRange) TryAttack();
////            else ChasePlayer();
////        }
////        else StopMoving();

////        UpdateAnimation();
////        FlipSprite();
////    }

////    public void TakeHit(float damageAmount)
////    {
////        if (isDead) return;

////        Hitpoints -= damageAmount;

////        //if (healthbar != null)
////        //    healthbar.SetHealth(Hitpoints, MaxHitpoints);

////        if (Hitpoints <= 0)
////        {
////            Die();
////        }
////    }

////    private void Die()
////    {
////        isDead = true;
////        animator.SetTrigger("Die");
////        rb.linearVelocity = Vector2.zero;

////        // Optional: disable collider or destroy after a delay
////        Destroy(gameObject, 1f);
////    }


////    void ChasePlayer()
////    {
////        float direction = Mathf.Sign(player.position.x - transform.position.x);
////        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
////    }

////    void StopMoving()
////    {
////        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
////    }

////    void TryAttack()
////    {
////        if (Time.time >= lastAttackTime + attackCooldown)
////        {
////            lastAttackTime = Time.time;
////            StopMoving();
////            if (animator != null) animator.SetTrigger("Attack"); // Matches "Attack" trigger
////        }
////    }

////    void UpdateAnimation()
////    {
////        bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
////        if (animator != null) animator.SetBool("isMoving", moving); // Matches "isMoving" bool
////    }

////    private void OnTriggerEnter2D(Collider2D collision)
////    {
////        // Check if the thing we bumped into is the Player
////        if (collision.CompareTag("Player"))
////        {
////            // Check if the player has a Health script (you'll need to create one for the player too!)
////            // PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
////            // if (playerHealth != null) playerHealth.TakeDamage(enemyDamage);

////            collision.GetComponent<Health>().TakeDamage(damage);
////            Debug.Log("Player took damage!");
////        }
////    }


////    void FlipSprite()
////    {
////        if (player.position.x > transform.position.x) spriteRenderer.flipX = false;
////        else spriteRenderer.flipX = true;
////    }
////}

//using UnityEngine;

//public class BossLevel2 : MonoBehaviour
//{
//    [Header("References")]

//    public float Hitpoints = 8f;
//    public float MaxHitpoints = 8f;
//    public Transform player;
//    public Rigidbody2D rb;
//    public Animator animator;
//    public SpriteRenderer spriteRenderer;

//    [Header("Movement")]
//    public float moveSpeed = 2f;

//    [Header("Detection")]
//    private float distanceToPlayer;
//    public float detectionRange = 7f;
//    public float attackRange = 2f;

//    [Header("Attack")]
//    public float attackCooldown = 2f;
//    private float lastAttackTime = -Mathf.Infinity;

//    [Header("Player Combat")]
//    [SerializeField] private float damage = 1f;

//    private bool isDead = false;

//    private void Start()
//    {
//        Hitpoints = MaxHitpoints;
//    }

//    private void Update()
//    {
//        if (isDead || player == null) return;

//        distanceToPlayer = Vector2.Distance(transform.position, player.position);

//        if (distanceToPlayer <= detectionRange)
//        {
//            if (distanceToPlayer <= attackRange)
//            {
//                TryAttack();
//            }
//            else
//            {
//                ChasePlayer();
//            }
//        }
//        else
//        {
//            StopMoving();
//        }

//        UpdateAnimation();
//        FlipSprite();
//    }

//    public void TakeHit(float damageAmount)
//    {
//        if (isDead) return;

//        Hitpoints -= damageAmount;

//        if (Hitpoints > 0)
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
//        StopMoving();

//        if (rb != null)
//            rb.linearVelocity = Vector2.zero;

//        if (animator != null)
//            animator.SetTrigger("Die");

//        Collider2D col = GetComponent<Collider2D>();
//        if (col != null)
//            col.enabled = false;

//        this.enabled = false;

//        Destroy(gameObject, 1.2f);
//    }

//    private void ChasePlayer()
//    {
//        float direction = Mathf.Sign(player.position.x - transform.position.x);
//        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
//    }

//    private void StopMoving()
//    {
//        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//    }

//    private void TryAttack()
//    {
//        if (Time.time < lastAttackTime + attackCooldown) return;

//        lastAttackTime = Time.time;
//        StopMoving();

//        if (animator != null)
//        {
//            animator.SetTrigger("Attack"); // ONLY ONE ATTACK
//        }
//    }

//    private void UpdateAnimation()
//    {
//        bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;

//        if (animator != null)
//            animator.SetBool("IsMoving", moving);
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (isDead) return;

//        if (collision.CompareTag("Player"))
//        {
//            Health playerHealth = collision.GetComponent<Health>();
//            if (playerHealth != null)
//            {
//                playerHealth.TakeDamage(damage);
//                Debug.Log("Player took damage!");
//            }
//        }
//    }

//    private void FlipSprite()
//    {
//        if (player == null || spriteRenderer == null) return;

//        if (player.position.x > transform.position.x)
//            spriteRenderer.flipX = false;
//        else
//            spriteRenderer.flipX = true;
//    }
//}
using UnityEngine;

public class BossLevel2 : MonoBehaviour
{
    [Header("References")]
    public float Hitpoints = 8f;
    public float MaxHitpoints = 8f;
    public Transform player;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Detection")]
    private float distanceToPlayer;
    public float detectionRange = 7f;
    public float attackRange = 2f;

    [Header("Attack Settings")]
    public float attackCooldown = 2f;
    private float lastAttackTime = -Mathf.Infinity;
    [Range(0, 1)] public float attack2Chance = 0.5f; // 50% chance for the second attack

    [Header("Player Combat")]
    [SerializeField] private float damage = 1f;

    private bool isDead = false;

    private void Start()
    {
        Hitpoints = MaxHitpoints;
    }

    private void Update()
    {
        if (isDead || player == null) return;

        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                TryAttack();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            StopMoving();
        }

        UpdateAnimation();
        FlipSprite();
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        Hitpoints -= damageAmount;
        Debug.Log("Boss hit! Remaining HP: " + Hitpoints);

        if (Hitpoints > 0)
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
        StopMoving();

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        if (animator != null)
            animator.SetTrigger("Die");

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        this.enabled = false;
        Destroy(gameObject, 1.2f);
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
        if (Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;
        StopMoving();

        if (animator != null)
        {
            // Randomly choose between Attack and Attack2
            if (Random.value < attack2Chance)
            {
                animator.SetTrigger("Attack2");
            }
            else
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    // This method can be called by your Animation Events for both Attack 1 and 2
    // If you need different damage for different attacks, you can add a parameter
    public void ExecuteAttackDamage()
    {
        if (isDead || player == null) return;

        // Check distance again to ensure player hasn't moved away during the wind-up
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit by animation event!");
            }
        }
    }


    public void DealAttack2Damage()
    {
        if (isDead || player == null) return;

        // Check if player is still in range when the hit actually lands
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    // Fixes the "EndAttack" error
    public void EndAttack()
    {
        // This is called by the animation event at the end of the clip
        // It ensures the boss doesn't get "stuck" in an attack state
        Debug.Log("Attack animation finished.");

        // If you had a 'isAttacking' boolean, you would set it to false here.
        // For your current script, just having the function here will stop the error.
    }
    private void UpdateAnimation()
    {
        bool moving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        if (animator != null)
            animator.SetBool("IsMoving", moving);
    }

    private void FlipSprite()
    {
        if (player == null || spriteRenderer == null) return;

        if (player.position.x > transform.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }
}