using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField]private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;
    private void Update()
    {
       // Maintain the current Y and Z of the camera, only follow Player on X
    transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // This line calculates lookAhead but currently doesn't apply it to the position
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

    }


    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }

}
        




