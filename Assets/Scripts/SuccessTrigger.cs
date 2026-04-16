using UnityEngine;

public class SuccessTrigger : MonoBehaviour
{
    public DogController dog;

    private void OnEnable()
    {
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.WirePuzzleSolved();
        }

        if (dog != null)
        {
            dog.OnBreakerFixed();
        }

        Invoke(nameof(HidePuzzle), 1.0f);
    }

    void HidePuzzle()
    {
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.ClosePuzzle();
        }
    }
}