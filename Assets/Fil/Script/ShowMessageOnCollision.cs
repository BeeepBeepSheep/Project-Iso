using UnityEngine;
using TMPro;

public class ShowMessageOnCollision : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Reference to a UI TextMeshPro text
    public KeyCode teleportKey = KeyCode.E; // Key to trigger teleportation
    public string[] levelScenes; // Array of level scene names
    public string essentialScene = "Essential"; // Name of the essential scene

    private void Start()
    {
        // Deactivate the text at the start
        HideText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideText();
        }
    }

    private void Update()
    {
        if (messageText.gameObject.activeSelf && Input.GetKeyDown(teleportKey))
        {
            TeleportPlayer();
        }
    }

    private void ShowText()
    {
        messageText.gameObject.SetActive(true);
    }

    private void HideText()
    {
        messageText.gameObject.SetActive(false);
    }

    private void TeleportPlayer()
    {
        if (levelScenes.Length > 0)
        {
            // Choose a random level scene from the array
            string randomLevelScene = levelScenes[Random.Range(0, levelScenes.Length)];

            // Unload the current level scene
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

            // Load the new random level scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(randomLevelScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogWarning("No level scenes specified for teleportation.");
        }
    }
}
