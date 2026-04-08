using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Breaker : MonoBehaviour
{
    [Header("Visuals")]
    public Light2D streetLight;
    public SpriteRenderer lampSR;
    public GameObject interactionPrompt; // A small "Press E" UI icon

    [Header("Settings")]
    public KeyCode interactKey = KeyCode.E;
    public Color brokenColor = Color.gray;
    public Color fixedColor = Color.white;

    private bool _playerInRange;
    private bool _isFixed;
    private bool _puzzleOpen;

    private void Start()
    {
        // Set initial state: Light off, lamp dim
        if (streetLight != null) streetLight.enabled = false;
        if (lampSR != null) lampSR.color = brokenColor;
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        // Don't allow interaction if already fixed or if puzzle is already open
        if (!_playerInRange || _isFixed || _puzzleOpen) return;

        if (Input.GetKeyDown(interactKey))
        {
            OpenBreakerPuzzle();
        }
    }

    private void OpenBreakerPuzzle()
    {
        _puzzleOpen = true;
        if (interactionPrompt != null) interactionPrompt.SetActive(false);

        // Call your manager
        PuzzleManager.Instance.OpenPuzzle(this);

        // Optional: Pause player movement here if you have a PlayerController
        // PlayerController.Instance.DisableMovement(); 
    }

    public void FinishPuzzle()
    {
        _isFixed = true;
        _puzzleOpen = false;

        if (streetLight != null) streetLight.enabled = true;
        if (lampSR != null) lampSR.color = fixedColor;

        Debug.Log("Breaker fixed! Streetlight is now active.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isFixed)
        {
            _playerInRange = true;
            if (interactionPrompt != null) interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = false;
            if (interactionPrompt != null) interactionPrompt.SetActive(false);

            // If the player walks away, close the puzzle automatically
            if (_puzzleOpen)
            {
                _puzzleOpen = false;
                PuzzleManager.Instance.ClosePuzzle(); // Make sure your manager has this!
            }
        }
    }
}

