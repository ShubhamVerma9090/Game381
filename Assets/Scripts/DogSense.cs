using UnityEngine;

public class DogSense : MonoBehaviour
{
    [Header("Hearing")]
    public float hearingRange = 6f;
    public float alertForgetTime = 3f;

    [Header("References")]
    public DogClearSight clearSight;

    [Header("Debug")]
    public Vector3 lastHeardPosition;
    public GameObject lastHeardSource;
    public bool isAlert;

    private float alertTimer;

    private void Awake()
    {
        if (clearSight == null)
            clearSight = GetComponent<DogClearSight>();
    }

    private void OnEnable()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.OnSoundMade += OnSoundHeard;
    }

    private void OnDisable()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.OnSoundMade -= OnSoundHeard;
    }

    private void Update()
    {
        if (isAlert)
        {
            alertTimer -= Time.deltaTime;

            if (alertTimer <= 0f)
            {
                isAlert = false;
                lastHeardSource = null;
            }
        }

        if (isAlert)
        {
            LookToward(lastHeardPosition);
        }
    }

    private void OnSoundHeard(SoundEvent soundEvent)
    {
        float distance = Vector3.Distance(transform.position, soundEvent.position);
        float effectiveRange = hearingRange + soundEvent.loudness;

        if (distance > effectiveRange)
            return;

        if (soundEvent.type == SoundType.EnemyFootstep || soundEvent.type == SoundType.EnemyMovement)
        {
            isAlert = true;
            alertTimer = alertForgetTime;
            lastHeardPosition = soundEvent.position;
            lastHeardSource = soundEvent.source;

            Debug.Log("Dog heard enemy movement.");

            if (clearSight != null && !clearSight.IsActive && !clearSight.IsOnCooldown)
            {
                clearSight.TryActivateClearSight();
            }
        }
    }

    private void LookToward(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        direction.z = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}