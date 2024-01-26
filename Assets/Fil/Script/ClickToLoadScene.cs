using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToLoadScene : MonoBehaviour
{
    public string[] scenesToLoad;
    public string essentialSceneName = "EssentialScene";

    private void OnMouseDown()
    {
        StartCoroutine(LoadRandomScene());
    }

    private IEnumerator LoadRandomScene()
    {
        // Ensure the player doesn't move while the scene is being loaded
        // You may want to disable player controls or implement a loading screen here

        // Get a random scene from the array
        string sceneToLoad = GetRandomScene();

        // Unload the current scene (excluding the essential scene)
        UnloadCurrentScene();

        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        // Wait until the new scene is loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
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
