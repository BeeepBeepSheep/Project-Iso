using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Teleporter : MonoBehaviour
{
    public string[] sceneCollection;
    public float fadeDuration = 1.0f;
    public Image fadeImage;

    private bool hasFadedIn = false; // To track whether fade-in has already occurred

    private void Start()
    {
        // Trigger fade-in effect when the scene starts
        if (!hasFadedIn)
        {
            StartCoroutine(FadeIn());
            hasFadedIn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player entered the teleporter
            StartCoroutine(TransitionToRandomScene());
        }
    }

    private IEnumerator TransitionToRandomScene()
    {
        // Trigger fade-out effect
        yield return StartCoroutine(FadeOut());

        // Load a random scene from the collection
        LoadRandomScene();

        // Reset the fade effect (optional)
        if (!hasFadedIn)
        {
            yield return StartCoroutine(FadeIn());
            hasFadedIn = true;
        }
    }

    private void LoadRandomScene()
    {
        int randomIndex = Random.Range(0, sceneCollection.Length);
        string randomScene = sceneCollection[randomIndex];

        SceneManager.LoadScene(randomScene);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
