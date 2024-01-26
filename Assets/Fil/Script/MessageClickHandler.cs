using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageClickHandler : MonoBehaviour
{
    public string[] scenesToLoad;
    public string essentialSceneName = "Essential";

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click on the message
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the UI element
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Handle the click on the message
                HandleMessageClick();
            }
        }
    }

    private void HandleMessageClick()
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
