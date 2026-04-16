using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.8f;
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackCooldown = 0.4f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float climbSpeed = 5f;

    [Header("Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer sr;

    private float horizontalInput;
    private float verticalInput;
    private float lastAttackTime;
    private bool isGrounded;
    private bool isOnLadder;
    private bool isClimbing;
    private float originalGravity;
    [SerializeField] private float trampolineForce = 20f;
    public Transform spawnPoint;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalGravity = body.gravityScale;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (groundCheck != null)
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // --- FLIP LOGIC START ---
        if (horizontalInput != 0)
        {
            bool shouldFlip = horizontalInput < 0;

            // Only update if the direction actually changed
            if (sr.flipX != shouldFlip)
            {
                sr.flipX = shouldFlip;
                FlipAttackPoint(shouldFlip);
            }
        }
        // --- FLIP LOGIC END ---

        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isClimbing)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        }

        HandleLadderLogic();
        UpdateAnimatorParameters();
    }

    private void FlipAttackPoint(bool facingLeft)
    {
        if (attackPoint == null) return;

        // Get the current local position
        Vector3 currentPos = attackPoint.localPosition;

        // Force the X position to be positive if facing right, negative if facing left
        // This is safer than just multiplying by -1 to prevent desync
        float xOffset = Mathf.Abs(currentPos.x);
        attackPoint.localPosition = new Vector3(facingLeft ? -xOffset : xOffset, currentPos.y, currentPos.z);
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            body.linearVelocity = new Vector2(horizontalInput * moveSpeed * 0.5f, verticalInput * climbSpeed);
        }
        else
        {
            body.linearVelocity = new Vector2(horizontalInput * moveSpeed, body.linearVelocity.y);
        }
    }

    private void PerformAttack()
    {
        lastAttackTime = Time.time;
        anim.SetTrigger("attack1");
    }

    public void DealDamage()
    {
        if (attackPoint == null) return;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
                enemyAI.TakeHit(attackDamage);
        }
    }

    private void HandleLadderLogic()
    {
        if (isOnLadder && Mathf.Abs(verticalInput) > 0.1f)
        {
            isClimbing = true;
        }

        if (!isOnLadder)
        {
            isClimbing = false;
        }

        if (isClimbing)
        {
            body.gravityScale = 0f;
        }
        else
        {
            body.gravityScale = originalGravity;
        }
    }

    private void UpdateAnimatorParameters()
    {
        anim.SetBool("run", horizontalInput != 0 && !isClimbing);
        anim.SetBool("grounded", isGrounded && !isClimbing);
        anim.SetBool("isClimbing", isClimbing);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
            isOnLadder = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ladder"))
        {
            isOnLadder = false;
            isClimbing = false;
            body.gravityScale = originalGravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("tramp") && !isGrounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, trampolineForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}