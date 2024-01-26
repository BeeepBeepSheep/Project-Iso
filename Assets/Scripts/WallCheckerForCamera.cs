using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckerForCamera : MonoBehaviour
{
    public float targetWallMaterial_AlphaPErcentage_AfterFade;

    public float alphaTransitionDuration = 1.5f;
    public float wallAlphaMax = 1f;
    public float wallAlphaMin = 0.1f;
    private void OnTriggerEnter(Collider other)//fade out
    {
        if(other.CompareTag("HideableWall"))
        {
            WallFade wall = other.GetComponent<WallFade>();

            wall.StopAllCoroutines();
            StartCoroutine(wall.LerpAlphaForMaterials(wallAlphaMin, alphaTransitionDuration));
        }
    }

    private void OnTriggerExit(Collider other)//fade in
    {
        if (other.CompareTag("HideableWall"))
        {
            WallFade wall = other.GetComponent<WallFade>();

            wall.StopAllCoroutines();
            StartCoroutine(wall.LerpAlphaForMaterials(wallAlphaMax, alphaTransitionDuration));
        }
    }
}
