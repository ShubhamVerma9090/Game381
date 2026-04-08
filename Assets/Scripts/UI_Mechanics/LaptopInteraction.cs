using TMPro;
using UnityEngine;

public class LaptopInteraction : MonoBehaviour

{
    [Header("UI References")]
    public GameObject laptopUI;
    public TMP_InputField inputField;
    public string correctPassword = "1234";

    [Header("World Objects")]
    public GameObject[] streetLights;

    private bool isPlayerNearby = false;



    void Update()

    {

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))

        {

            // Tell the Manager to show the UI

            if (PuzzleManager.Instance != null)

            {

                PuzzleManager.Instance.OpenLaptop(this);

                Debug.Log("Sent Open Request to Manager");

            }

            else

            {

                Debug.LogError("PuzzleManager Instance is NULL!");

            }

        }

    }



    void OpenLaptop()

    {

        laptopUI.SetActive(true);

        inputField.text = "";

        inputField.ActivateInputField();

        Time.timeScale = 0;

        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.None;

    }



    public void CloseLaptop()

    {

        laptopUI.SetActive(false);

        Time.timeScale = 1;

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

    }



    public void CheckPassword()
    {
        if (inputField.text == correctPassword)
        {
            Debug.Log("Password Correct!");

            // 1. Turn on the lights directly
            foreach (GameObject light in streetLights)
            {
                light.SetActive(true);
            }

            // 2. Notify manager (if you still need it for overall game progress)
            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.PuzzleSolved();
            }

            CloseLaptop();
        }
        else
        {
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }



    private void OnTriggerEnter2D(Collider2D other) => isPlayerNearby = other.CompareTag("Player");

    private void OnTriggerExit2D(Collider2D other) => isPlayerNearby = false;

}