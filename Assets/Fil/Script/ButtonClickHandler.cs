using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour
{
    public string[] scenesToLoad;
    public string essentialSceneName = "Essential";

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(HandleButtonClick);
        }
    }

    public void HandleButtonClick()
    {
        // Get a random scene from the array
        string sceneToLoad = GetRandomScene();

        // Unload the current scene (excluding the essential scene)
        UnloadCurrentScene();

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }

    private void UnloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != essentialSceneName)
        {
            SceneManager.UnloadSceneAsync(currentScene.buildIndex);
        }
    }

    private string GetRandomScene()
    {
        string sceneToLoad = essentialSceneName;

        while (sceneToLoad == essentialSceneName)
        {
            sceneToLoad = scenesToLoad[Random.Range(0, scenesToLoad.Length)];
        }

        return sceneToLoad;
    }
}
