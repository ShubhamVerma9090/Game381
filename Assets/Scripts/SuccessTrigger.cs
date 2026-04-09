using UnityEngine;

public class SuccessTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        // Tell the manager to turn on the light
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.WirePuzzleSolved();
        }

        // Close the puzzle after 1 second
        Invoke("HidePuzzle", 1.0f);
    }

    void HidePuzzle() => PuzzleManager.Instance.ClosePuzzle();
}

