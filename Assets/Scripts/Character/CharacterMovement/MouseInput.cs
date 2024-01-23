using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    public Vector3 mouseInputPosition; // Stores the position of the mouse input
    [HideInInspector]
    public Vector3 rayToWorldIntersectionPoint;

    public void MousePositionUpdate(InputAction.CallbackContext callbackContext)
    {
        mouseInputPosition = callbackContext.ReadValue<Vector2>();
    }

    void Update()
    {
        // Shoots a ray from the main camera to the mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(mouseInputPosition);

        RaycastHit hit;
        // Checks if the ray hits any collider in the scene
        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            // Updates the mouseInputPosition with the point where the ray hits an object's collider
            rayToWorldIntersectionPoint = hit.point;
        }
    }


}
