using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    void Start()
    {
     
        if (GlobalSaveManager.ShouldUseSavedPosition)
        {
            transform.position = GlobalSaveManager.SavedPosition;

            GlobalSaveManager.ShouldUseSavedPosition = false;

            Debug.Log("Player returned to: " + transform.position);
        }
    }
}