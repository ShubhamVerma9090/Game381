
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class DogController : MonoBehaviour
{
    public enum DogState
    {
        Idle,
        MovingToCheckpoint,
        Barking,
        WaitingForPlayerAfterTeleport
    }

    [Header("References")]
    public Transform player;
    public List<Transform> checkpoints;
    private int currentCheckpointIndex = 0;

    [Header("Settings")]
    public float moveSpeed = 4f;
    public float stopDistance = 0.3f;
    public float playerTriggerDistance = 5f;

    private DogState currentState = DogState.Idle;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 originalScale;
    private bool checkpointReached = false;

    private DogTeleportStation currentTeleportStation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;

        if (checkpoints != null && checkpoints.Count > 0)
            currentState = DogState.Idle;
    }

    void Update()
    {
        if (player == null) return;
        if (checkpoints == null || checkpoints.Count == 0) return;
        if (currentCheckpointIndex >= checkpoints.Count) return;

        Transform currentTarget = checkpoints[currentCheckpointIndex];
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        float distToCheckpoint = Vector2.Distance(transform.position, currentTarget.position);

        switch (currentState)
        {
            case DogState.Idle:
                if (distToPlayer <= playerTriggerDistance)
                {
                    checkpointReached = false;
                    currentState = DogState.MovingToCheckpoint;
                }
                break;

            case DogState.MovingToCheckpoint:
                if (!checkpointReached && distToCheckpoint <= stopDistance)
                {
                    HandleReachedTarget(currentTarget);
                }
                break;

            case DogState.Barking:
                break;

            case DogState.WaitingForPlayerAfterTeleport:
                if (currentTeleportStation != null && currentTeleportStation.exitPoint != null)
                {
                    float distPlayerToExit = Vector2.Distance(player.position, currentTeleportStation.exitPoint.position);

                    if (distPlayerToExit <= currentTeleportStation.playerResumeDistance)
                    {
                        currentTeleportStation = null;
                        checkpointReached = false;
                        currentState = DogState.MovingToCheckpoint;
                    }
                }
                break;
        }

        UpdateAnimations();
    }

    void FixedUpdate()
    {
        if (checkpoints == null || checkpoints.Count == 0) return;
        if (currentCheckpointIndex >= checkpoints.Count) return;

        if (currentState == DogState.MovingToCheckpoint && !checkpointReached)
        {
            MoveToTarget(checkpoints[currentCheckpointIndex].position);
        }
        else
        {
            StopMoving();
        }
    }

    void MoveToTarget(Vector3 target)
    {
        float differenceX = target.x - transform.position.x;

        if (Mathf.Abs(differenceX) <= stopDistance)
        {
            HandleReachedTarget(checkpoints[currentCheckpointIndex]);
            return;
        }

        float dir = Mathf.Sign(differenceX);
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        FaceDirection(dir);
    }

    void HandleReachedTarget(Transform target)
    {
        checkpointReached = true;
        StopMoving();

        DogTeleportStation teleportStation = target.GetComponent<DogTeleportStation>();

        if (teleportStation != null && teleportStation.exitPoint != null)
        {
            TeleportToStation(teleportStation);
        }
        else
        {
            StartBarking();
        }
    }

    void TeleportToStation(DogTeleportStation station)
    {
        currentTeleportStation = station;

        transform.position = station.exitPoint.position;
        StopMoving();

        float dirToPlayer = player.position.x - transform.position.x;
        if (Mathf.Abs(dirToPlayer) > 0.01f)
            FaceDirection(Mathf.Sign(dirToPlayer));

        currentCheckpointIndex++;

        if (station.waitForPlayerAfterTeleport)
        {
            currentState = DogState.WaitingForPlayerAfterTeleport;
        }
        else
        {
            checkpointReached = false;
            currentState = DogState.MovingToCheckpoint;
        }
    }

    void StartBarking()
    {
        currentState = DogState.Barking;
        StopMoving();

        Transform checkpoint = checkpoints[currentCheckpointIndex];
        transform.position = new Vector3(
            checkpoint.position.x,
            transform.position.y,
            transform.position.z
        );

        if (anim != null)
        {
            anim.ResetTrigger("Bark");
            anim.SetTrigger("Bark");
        }
    }

    void StopMoving()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    void FaceDirection(float dir)
    {
        Vector3 scale = originalScale;
        scale.x = Mathf.Abs(originalScale.x) * Mathf.Sign(dir);
        transform.localScale = scale;
    }

    void UpdateAnimations()
    {
        if (anim == null) return;

        bool isMoving = currentState == DogState.MovingToCheckpoint && Mathf.Abs(rb.linearVelocity.x) > 0.05f;
        anim.SetBool("Run", isMoving);
    }

    public void OnBreakerFixed()
    {
        currentCheckpointIndex++;

        if (currentCheckpointIndex < checkpoints.Count)
        {
            checkpointReached = false;
            currentState = DogState.MovingToCheckpoint;
        }
        else
        {
            checkpointReached = false;
            currentState = DogState.Idle;
            StopMoving();
        }
    }

    



}