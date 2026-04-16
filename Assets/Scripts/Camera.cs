//Works
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D playerRb;

    [Header("Follow")]
    [SerializeField] private float smoothTimeX = 0.2f;
    [SerializeField] private float smoothTimeY = 0.15f;
    [SerializeField] private bool followY = true;

    [Header("Look Ahead")]
    [SerializeField] private float aheadDistance = 2f;
    [SerializeField] private float cameraSpeed = 4f;
    [SerializeField] private float moveThreshold = 0.05f;

    private Vector3 startOffset;
    private float lookAhead;
    private float facingDirection = 1f;

    private float velocityX;
    private float velocityY;

    private void Start()
    {
        if (player != null)
        {
            startOffset = transform.position - player.position;
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        float moveX = 0f;

        if (playerRb != null && Mathf.Abs(playerRb.linearVelocity.x) > moveThreshold)
        {
            facingDirection = Mathf.Sign(playerRb.linearVelocity.x);
            moveX = aheadDistance * facingDirection;
        }

        lookAhead = Mathf.Lerp(lookAhead, moveX, Time.deltaTime * cameraSpeed);

        float targetX = player.position.x + startOffset.x + lookAhead;
        float targetY = followY
            ? player.position.y + startOffset.y
            : transform.position.y;

        float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref velocityX, smoothTimeX);
        float newY = Mathf.SmoothDamp(transform.position.y, targetY, ref velocityY, smoothTimeY);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}