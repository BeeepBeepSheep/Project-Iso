using UnityEngine;
using UnityEngine.UI;

public class ShowMessageOnCollision : MonoBehaviour
{
    public Button messageButton; // Reference to a UI button

    private void Start()
    {
        // Deactivate the button at the start
        HideButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideButton();
        }
    }

    private void ShowButton()
    {
        messageButton.gameObject.SetActive(true);
    }

    private void HideButton()
    {
        messageButton.gameObject.SetActive(false);
    }
}
