using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PoliteDog : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;

    private Animator anim;
    private Rigidbody2D rb;
    private Rigidbody2D playerRb;

    [Header("Follow Settings")]
    public float stopDistance = 2.0f;         
    public float startFollowDistance = 5.0f;  
    public float minPlayerSpeed = 0.1f;       

    [Header("Control Mode")]
    public bool manualControl = false;

    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (player != null)
            playerRb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (manualControl)
        {
            HandleManualMovement();
        }
        else
        {
            HandleFollowMovement();
        }

        
        anim.SetBool("Run", Mathf.Abs(horizontalInput) > 0.01f);
    }

    void HandleManualMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        horizontalInput = moveX;

        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed;
        rb.linearVelocity = movement;
    }

    void HandleFollowMovement()
    {
        
        if (player == null || playerRb == null)
        {
            rb.linearVelocity = Vector2.zero;
            horizontalInput = 0f;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        bool playerIsMoving = playerRb.linearVelocity.magnitude > minPlayerSpeed;

        
        if (distance <= stopDistance || !playerIsMoving)
        {
            rb.linearVelocity = Vector2.zero;
            horizontalInput = 0f;
            return;
        }

        
        if (distance >= startFollowDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            
            rb.linearVelocity = direction * moveSpeed;

            horizontalInput = rb.linearVelocity.x;
        }
        else
        {
            
            rb.linearVelocity = Vector2.zero;
            horizontalInput = 0f;
        }
    }
}