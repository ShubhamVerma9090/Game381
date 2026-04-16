using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject endGamePanel;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;

        if (collision.CompareTag("Player"))
        {
            hasTriggered = true;
            ShowEndScreen();
        }
    }

    private void ShowEndScreen()
    {
        // Show UI
        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        // Stop time (freezes game)
        Time.timeScale = 0f;
    }
}