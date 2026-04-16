using UnityEngine;

public class DogTeleportStation : MonoBehaviour
{
    public Transform exitPoint;
    public bool waitForPlayerAfterTeleport = true;
    public float playerResumeDistance = 3f;
}