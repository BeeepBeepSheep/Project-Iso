using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textOnScreen; // Reference to the UI text to display object names
    [SerializeField] UIPoolBar hpBar; // Reference to the health bar UI

    GameObject currentHoverOverObject; // Keeps track of the currently hovered GameObject

    [HideInInspector]
    public InteractableObject hoveringOverObject; // Reference to the InteractableObject the mouse is hovering over
    [HideInInspector]
    public Character hoveringOverCharacter; // Reference to the Character component of the hovered object

    InteractHandler interactHandler;

    Vector2 mousePosition;

    private void Awake()
    {
        interactHandler = GetComponent<InteractHandler>();
    }

    void Update()
    {
       CheckInteractObject(); // Checks for interactable objects under the mouse pointer      
    }

    public void MousePositionInput(InputAction.CallbackContext callbackContext)
    {
        mousePosition = callbackContext.ReadValue<Vector2>();
    }

    private void CheckInteractObject()
    {
        // Casts a ray from the main camera to the mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (currentHoverOverObject != hit.transform.gameObject)
            {
                currentHoverOverObject = hit.transform.gameObject;
                UpdateInteractableObject(hit);
            }
        }
    }

    internal void Interact()
    {
        interactHandler.interactedObject = hoveringOverObject;
    }

    internal bool InteractCheck()
    {
        // If an interactable object is being hovered over and the left mouse button is clicked
        return hoveringOverObject != null;
    }

    

    private void UpdateInteractableObject(RaycastHit hit)
    {
        // Checks if the object hit by the ray is an InteractableObject
        InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
        if (interactableObject != null)
        {
            // Updates the reference to the currently hovered InteractableObject
            hoveringOverObject = interactableObject;
            // Gets the Character component from the InteractableObject (if it exists)
            hoveringOverCharacter = interactableObject.GetComponent<Character>();
            // Updates the UI text to display the name of the hovered object
            textOnScreen.text = hoveringOverObject.objectName;
        }
        else
        {
            // If not hovering over an InteractableObject, reset the references and clear the UI text
            hoveringOverCharacter = null;
            hoveringOverObject = null;
            textOnScreen.text = " ";
        }
        UpdateHPBar(); // Updates the health bar UI based on the hovered character (if applicable)
    }

    private void UpdateHPBar()
    {
        // Checks if a character is being hovered over
        if (hoveringOverCharacter != null)
        {
            // Shows the health bar UI and links it to the life pool of the hovered character
            hpBar.Show(hoveringOverCharacter.lifePool);
        }
        else
        {
            // Clears the health bar UI if not hovering over a character
            hpBar.Clear();
        }
    }

    internal void ResetState()
    {
        interactHandler.ResetState();
    }
}
