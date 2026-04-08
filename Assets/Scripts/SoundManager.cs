using System;
using UnityEngine;

public enum SoundType
{
    EnemyFootstep,
    EnemyMovement,
    Danger
}

public struct SoundEvent
{
    public Vector3 position;
    public float loudness;
    public SoundType type;
    public GameObject source;

    public SoundEvent(Vector3 position, float loudness, SoundType type, GameObject source)
    {
        this.position = position;
        this.loudness = loudness;
        this.type = type;
        this.source = source;
    }
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Action<SoundEvent> OnSoundMade;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EmitSound(Vector3 position, float loudness, SoundType type, GameObject source)
    {
        SoundEvent newSound = new SoundEvent(position, loudness, type, source);
        OnSoundMade?.Invoke(newSound);
    }
}