//using UnityEngine;

//public class ControlSwitcher : MonoBehaviour
//{
//    public MonoBehaviour playerMovement;
//    public MonoBehaviour dogMovement;
//    public PoliteDog dog;
//    public Transform playerTransform;
//    public Transform dogTransform;
//   // public CameraFollow cameraFollow;

//    private bool controllingPlayer = true;

//    void Start()
//    {
//        EnablePlayerControl();
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Z))
//        {
//            controllingPlayer = !controllingPlayer;

//            if (controllingPlayer)
//                EnablePlayerControl();
//            else
//                EnableDogControl();
//        }
//    }

//    void EnablePlayerControl()
//    {
//        playerMovement.enabled = true;
//        dog.manualControl = false;  // Dog follows player automatically
//        if (cameraFollow != null) cameraFollow.target = playerTransform;
//    }

//    void EnableDogControl()
//    {
//        playerMovement.enabled = false; // Player freezes
//        dog.manualControl = true;       // Dog moves manually
//        if (cameraFollow != null) cameraFollow.target = dogTransform;
//    }
//}
