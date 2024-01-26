using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    public List<Renderer> renderers = new List<Renderer>();
    public List<Material> default_Materials = new List<Material>();
    public List<Material> current_materials = new List<Material>();

    public float initialAlpha;

    private void Awake()
    {
        if(renderers.Count == 0)
        {
            renderers.AddRange(GetComponentsInChildren<Renderer>());
        }

        for(int i = 0; i< renderers.Count; i++)
        {
            default_Materials.AddRange(renderers[i].materials);
            current_materials.AddRange(renderers[i].materials);
        }

        initialAlpha = current_materials[0].color.a;
    }

    public IEnumerator LerpAlphaForMaterials(float targetAlpha, float lerpDuration)
    {
        foreach (Material material in current_materials)
        {
            // Store the initial alpha value
            Color initialColor = material.color;

            // Calculate the target color with the desired alpha
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, targetAlpha);

            // Initialize the timer
            float elapsedTime = 0f;

            while (elapsedTime < lerpDuration)
            {
                // Lerp the alpha value over time
                material.color = Color.Lerp(initialColor, targetColor, elapsedTime / lerpDuration);

                // Increment the timer
                elapsedTime += Time.deltaTime;

                // Wait for the next frame
                yield return null;
            }

            // Ensure the final color is set to the target color
            material.color = targetColor;
        }
    }
}
