//using System.Collections;
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Animator))]
//public class DogController : MonoBehaviour
//{
//    public Transform player;
//    public float moveSpeed = 3f;

//    private Animator anim;
//    private Rigidbody2D rb;
//    private Rigidbody2D playerRb;

//    [Header("Follow Settings")]
//    public float stopDistance = 2.0f;
//    public float startFollowDistance = 5.0f;
//    public float minPlayerSpeed = 0.1f;

//    [Header("Control Mode")]
//    public bool manualControl = false;

//    [Header("Bark Settings")]
//    public float barkDuration = 1f;
//    private bool isBarking = false;

//    private float horizontalInput;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        anim = GetComponent<Animator>();

//        if (player != null)
//            playerRb = player.GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        if (isBarking)
//        {
//            rb.linearVelocity = Vector2.zero;
//            anim.SetBool("Run", false);
//            return;
//        }

//        if (manualControl)
//        {
//            HandleManualMovement();
//        }
//        else
//        {
//            HandleFollowMovement();
//        }

//        anim.SetBool("Run", Mathf.Abs(horizontalInput) > 0.01f);
//    }

//    void HandleManualMovement()
//    {
//        float moveX = Input.GetAxisRaw("Horizontal");
//        float moveY = Input.GetAxisRaw("Vertical");
//        horizontalInput = moveX;

//        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed;
//        rb.linearVelocity = movement;
//    }

//    void HandleFollowMovement()
//    {
//        if (player == null || playerRb == null)
//        {
//            rb.linearVelocity = Vector2.zero;
//            horizontalInput = 0f;
//            return;
//        }

//        float distance = Vector2.Distance(transform.position, player.position);
//        bool playerIsMoving = playerRb.linearVelocity.magnitude > minPlayerSpeed;

//        if (distance <= stopDistance || !playerIsMoving)
//        {
//            rb.linearVelocity = Vector2.zero;
//            horizontalInput = 0f;
//            return;
//        }

//        if (distance >= startFollowDistance)
//        {
//            Vector2 direction = (player.position - transform.position).normalized;
//            rb.linearVelocity = direction * moveSpeed;
//            horizontalInput = rb.linearVelocity.x;
//        }
//        else
//        {
//            rb.linearVelocity = Vector2.zero;
//            horizontalInput = 0f;
//        }
//    }

//    public void BarkAtTarget(Transform target)
//    {
//        if (!isBarking)
//            StartCoroutine(BarkRoutine(target));
//    }

//    private IEnumerator BarkRoutine(Transform target)
//    {
//        isBarking = true;
//        rb.linearVelocity = Vector2.zero;

//        if (target != null)
//        {
//            if (target.position.x > transform.position.x)
//                transform.localScale = new Vector3(1, 1, 1);
//            else
//                transform.localScale = new Vector3(-1, 1, 1);
//        }

//        anim.SetTrigger("Bark");

//        yield return new WaitForSeconds(barkDuration);

//        isBarking = false;
//    }

//    void OnTriggerStay2D(Collider2D other)
//    {
//        if (other.CompareTag("Breaker"))
//        {
//            Debug.Log("Dog detected breaker!");

//            // Trigger bark
//            anim.SetTrigger("Bark");
//        }
//    }


//}
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Animator))]
//public class DogController : MonoBehaviour
//{
//    public enum DogState
//    {
//        FollowAhead,
//        MoveToTarget,
//        WaitingAtBreaker,
//        Barking
//    }

//    [Header("References")]
//    public Transform player;
//    public Rigidbody2D playerRb;

//    private Rigidbody2D rb;
//    private Animator anim;
//    private Vector3 originalScale;

//    [Header("Movement")]
//    public float moveSpeed = 3f;
//    public float aheadDistance = 3f;
//    public float stopDistanceFromPlayer = 1.5f;
//    public float targetReachDistance = 0.4f;

//    [Header("Detection")]
//    public float scanRadius = 12f;
//    public LayerMask alertLayer;

//    [Header("Bark Settings")]
//    public float barkDuration = 1.2f;
//    private float barkTimer = 0f;

//    private DogState currentState = DogState.FollowAhead;
//    private DogBreakerDetector currentTarget;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        anim = GetComponent<Animator>();
//        originalScale = transform.localScale;

//        if (player != null && playerRb == null)
//            playerRb = player.GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        if (player == null) return;

//        switch (currentState)
//        {
//            case DogState.FollowAhead:
//                LookForTarget();
//                break;

//            case DogState.MoveToTarget:
//                if (currentTarget == null)
//                {
//                    ReturnToFollow();
//                    return;
//                }
//                break;

//            case DogState.WaitingAtBreaker:
//                if (currentTarget == null)
//                {
//                    ReturnToFollow();
//                    return;
//                }

//                if (currentTarget.targetType == DogBreakerDetector.TargetType.Breaker)
//                {
//                    if (currentTarget.IsCompleted())
//                    {
//                        currentTarget = null;
//                        ReturnToFollow();
//                    }
//                }
//                break;

//            case DogState.Barking:
//                barkTimer -= Time.deltaTime;
//                if (barkTimer <= 0f)
//                {
//                    if (currentTarget == null)
//                    {
//                        ReturnToFollow();
//                        return;
//                    }

//                    if (currentTarget.targetType == DogBreakerDetector.TargetType.Breaker)
//                    {
//                        currentState = DogState.WaitingAtBreaker;
//                    }
//                    else
//                    {
//                        currentTarget = null;
//                        ReturnToFollow();
//                    }
//                }
//                break;
//        }

//        UpdateAnimation();
//    }

//    void FixedUpdate()
//    {
//        if (player == null) return;

//        switch (currentState)
//        {
//            case DogState.FollowAhead:
//                FollowAheadMovement();
//                break;

//            case DogState.MoveToTarget:
//                MoveToCurrentTarget();
//                break;

//            case DogState.WaitingAtBreaker:
//            case DogState.Barking:
//                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//                break;
//        }
//    }

//    void FollowAheadMovement()
//    {
//        float playerDir = GetPlayerFacingDirection();
//        Vector3 desiredPos = player.position + new Vector3(playerDir * aheadDistance, 0f, 0f);

//        float distFromPlayer = Vector2.Distance(transform.position, player.position);

//        if (distFromPlayer < stopDistanceFromPlayer)
//        {
//            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//            return;
//        }

//        float dir = Mathf.Sign(desiredPos.x - transform.position.x);
//        float xDiff = Mathf.Abs(desiredPos.x - transform.position.x);

//        if (xDiff > 0.2f)
//        {
//            rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
//            FaceDirection(dir);
//        }
//        else
//        {
//            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//        }
//    }

//    void MoveToCurrentTarget()
//    {
//        if (currentTarget == null)
//        {
//            ReturnToFollow();
//            return;
//        }

//        Vector3 targetPos = currentTarget.transform.position;
//        float dist = Vector2.Distance(transform.position, targetPos);

//        if (dist <= targetReachDistance)
//        {
//            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//            BarkAtTarget();
//            return;
//        }

//        float dir = Mathf.Sign(targetPos.x - transform.position.x);
//        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
//        FaceDirection(dir);
//    }

//    void LookForTarget()
//    {
//        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, scanRadius, alertLayer);

//        if (hits.Length == 0) return;

//        DogBreakerDetector bestTarget = null;
//        float bestDistance = Mathf.Infinity;
//        float playerDir = GetPlayerFacingDirection();

//        foreach (Collider2D hit in hits)
//        {
//            DogBreakerDetector target = hit.GetComponent<DogBreakerDetector>();
//            if (target == null) continue;
//            if (!target.CanAlert()) continue;

//            float dirToTarget = target.transform.position.x - player.position.x;

//            if (playerDir > 0 && dirToTarget < 0) continue;
//            if (playerDir < 0 && dirToTarget > 0) continue;

//            float dist = Vector2.Distance(transform.position, target.transform.position);
//            if (dist < bestDistance)
//            {
//                bestDistance = dist;
//                bestTarget = target;
//            }
//        }

//        if (bestTarget != null)
//        {
//            currentTarget = bestTarget;
//            currentState = DogState.MoveToTarget;
//        }
//    }

//    public void BarkAtTarget()
//    {
//        currentState = DogState.Barking;
//        barkTimer = barkDuration;

//        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

//        if (anim != null)
//        {
//            anim.SetTrigger("Bark");
//        }
//    }

//    void ReturnToFollow()
//    {
//        currentState = DogState.FollowAhead;
//    }

//    float GetPlayerFacingDirection()
//    {
//        if (playerRb != null && Mathf.Abs(playerRb.linearVelocity.x) > 0.05f)
//        {
//            return Mathf.Sign(playerRb.linearVelocity.x);
//        }

//        return transform.localScale.x >= 0 ? 1f : -1f;
//    }

//    void FaceDirection(float dir)
//    {
//        Vector3 scale = originalScale;
//        scale.x = Mathf.Abs(originalScale.x) * Mathf.Sign(dir);
//        transform.localScale = scale;
//    }

//    void UpdateAnimation()
//    {
//        if (anim == null) return;

//        float moveAmount = Mathf.Abs(rb.linearVelocity.x);
//        anim.SetBool("isRunning", moveAmount > 0.1f);
//    }

//    void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, scanRadius);
//    }
//}


using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class DogController : MonoBehaviour
{
    public enum DogState { Idle, MovingToCheckpoint, BarkingAtBreaker }

    [Header("References")]
    public Transform player;
    public List<Transform> checkpoints; // Assign your breaker locations here
    private int currentCheckpointIndex = 0;

    [Header("Settings")]
    public float moveSpeed = 4f;
    public float stopDistance = 0.2f;
    public float playerTriggerDistance = 5f; // How close player must be for dog to "lead" them

    private DogState currentState = DogState.Idle;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentCheckpointIndex >= checkpoints.Count) return;

        float distToPlayer = Vector2.Distance(transform.position, player.position);
        float distToCheckpoint = Vector2.Distance(transform.position, checkpoints[currentCheckpointIndex].position);

        // Logic: If player is near, move to the next breaker/checkpoint
        if (distToPlayer < playerTriggerDistance && distToCheckpoint > stopDistance)
        {
            currentState = DogState.MovingToCheckpoint;
        }
        else if (distToCheckpoint <= stopDistance)
        {
            currentState = DogState.BarkingAtBreaker;
        }
    }

    void FixedUpdate()
    {
        if (currentState == DogState.MovingToCheckpoint)
        {
            MoveToTarget(checkpoints[currentCheckpointIndex].position);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isRunning", false);
        }
    }

    void MoveToTarget(Vector3 target)
    {
        float dir = Mathf.Sign(target.x - transform.position.x);
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

        // Flip Sprite
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir, transform.localScale.y, 1);
        anim.SetBool("isRunning", true);
    }

    // THIS IS THE KEY FUNCTION: Call this from your Breaker Script
    public void OnBreakerFixed()
    {
        currentCheckpointIndex++;
        currentState = DogState.MovingToCheckpoint;
        anim.SetTrigger("Bark"); // Happy bark because it's fixed!
    }
}