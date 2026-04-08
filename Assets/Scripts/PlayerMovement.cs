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

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalGravity = body.gravityScale;
    }

    private void Update()
    {
        // Inputs & Ground Check
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Flip Sprite
        if (horizontalInput != 0) sr.flipX = horizontalInput < 0;

        // Attack Logic (Mouse Left Click)
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack();
        }

        // Jump Logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isClimbing)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        }

        HandleLadderLogic();
        UpdateAnimatorParameters();
    }

    private void FixedUpdate()
    {
        if (!isClimbing)
        {
            body.linearVelocity = new Vector2(horizontalInput * moveSpeed, body.linearVelocity.y);
        }
        else
        {
            body.linearVelocity = new Vector2(horizontalInput * moveSpeed * 0.5f, verticalInput * climbSpeed);
        }
    }

    private void PerformAttack()
    {
        lastAttackTime = Time.time;
        anim.SetTrigger("attack1"); 
        // NOTE: Call DealDamage() via Animation Event on the Attack clip!
    }

    public void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null) enemyAI.TakeHit(attackDamage);
        }
    }

    private void HandleLadderLogic()
    {
        if (isOnLadder && Mathf.Abs(verticalInput) > 0.1f) isClimbing = true;

        if (isClimbing)
        {
            body.gravityScale = 0f;
            if (!isOnLadder) isClimbing = false;
        }
        else
        {
            body.gravityScale = originalGravity;
        }
    }

    private void UpdateAnimatorParameters()
    {
        // MATCHES YOUR SCREENSHOT EXACTLY
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded);
        //anim.SetBool("isClimbing", isClimbing);
    }

    private void OnTriggerEnter2D(Collider2D col) { if (col.CompareTag("Ladder")) isOnLadder = true; }
    private void OnTriggerExit2D(Collider2D col) { if (col.CompareTag("Ladder")) { isOnLadder = false; isClimbing = false; } }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}