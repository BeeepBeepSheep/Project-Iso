using UnityEngine;
using System.Collections;

public class WallFade : MonoBehaviour
{
    [SerializeField] float fadePercentage = 0.5f; // Set the desired fade percentage between 0 (completely transparent) and 1 (completely opaque)

    Renderer wallRenderer;
    bool isFading = false;

    void Start()
    {
        wallRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            isFading = true;
            StartCoroutine(FadeWall());
        }
    }

    IEnumerator FadeWall()
    {
        float currentAlpha = wallRenderer.material.color.a;

        while (currentAlpha > fadePercentage)
        {
            currentAlpha -= Time.deltaTime; // Adjust the speed of the fade
            Color newColor = wallRenderer.material.color;
            newColor.a = currentAlpha;
            wallRenderer.material.color = newColor;

            yield return null;
        }

        isFading = false;
    }
}
