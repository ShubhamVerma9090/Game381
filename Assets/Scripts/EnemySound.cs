using UnityEngine;

public class EnemySoundEmitter : MonoBehaviour
{
    public float moveThreshold = 0.05f;
    public float soundInterval = 0.6f;
    public float loudness = 4f;
    public SoundType soundType = SoundType.EnemyMovement;

    private Vector3 lastPosition;
    private float timer;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        float movedDistance = Vector3.Distance(transform.position, lastPosition);

        if (movedDistance > moveThreshold)
        {
            timer += Time.deltaTime;

            if (timer >= soundInterval)
            {
                MakeSound();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }

        lastPosition = transform.position;
    }

    private void MakeSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.EmitSound(transform.position, loudness, soundType, gameObject);
        }
    }
}