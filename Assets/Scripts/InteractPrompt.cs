using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    public GameObject promptUI;

    void Start()
    {
        HidePrompt();
    }

    public void ShowPrompt()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(true);
        }
    }

    public void HidePrompt()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }
}