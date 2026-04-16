using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Breaker : MonoBehaviour
{
    [Header("Visuals")]
    public Light2D streetLight;
    public SpriteRenderer lampSR;
    public GameObject promptText;   // "Press E to Repair"

    [Header("Settings")]
    public KeyCode interactKey = KeyCode.E;
    public Color brokenColor = Color.gray;
    public Color fixedColor = Color.white;

    private bool _playerInRange;
    private bool _isFixed;
    private bool _puzzleOpen;


    private void Start()
    {
        if (streetLight != null)
            streetLight.enabled = false;

        if (lampSR != null)
            lampSR.color = brokenColor;

        if (promptText != null)
            promptText.SetActive(false);
    }

    private void Update()
    {
        if (!_playerInRange || _isFixed || _puzzleOpen)
            return;

        if (Input.GetKeyDown(interactKey))
        {
            OpenBreakerPuzzle();
        }
    }

    private void OpenBreakerPuzzle()
    {
        _puzzleOpen = true;

        if (promptText != null)
            promptText.SetActive(false);

        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.OpenPuzzle(this);
        }
        else
        {
            Debug.LogWarning("PuzzleManager instance not found.");
        }
    }

    public void FinishPuzzle()
    {
        _isFixed = true;
        _puzzleOpen = false;
        _playerInRange = false;

        if (streetLight != null)
            streetLight.enabled = true;

        if (lampSR != null)
            lampSR.color = fixedColor;

        if (promptText != null)
            promptText.SetActive(false);

        Debug.Log("Breaker fixed! Streetlight is now active.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isFixed)
        {
            _playerInRange = true;

            if (promptText != null)
                promptText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = false;

            if (promptText != null)
                promptText.SetActive(false);

            if (_puzzleOpen)
            {
                _puzzleOpen = false;

                if (PuzzleManager.Instance != null)
                    PuzzleManager.Instance.ClosePuzzle();
            }
        }
    }
}