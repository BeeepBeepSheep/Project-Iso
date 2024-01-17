using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [HideInInspector]
    public Vector3 mouseInputPosition; // Stores the position of the mouse input

    void Update()
    {
        // Shoots a ray from the main camera to the mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        // Checks if the ray hits any collider in the scene
        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            // Updates the mouseInputPosition with the point where the ray hits an object's collider
            mouseInputPosition = hit.point;
        }
    }
}
