using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] string postMessage; // Message to display upon interaction
    public string objectName; // Name of the object

    private void Start()
    {
        // Sets the objectName to the name of the GameObject this script is attached to
        objectName = transform.name;
    }

    // Public method called to interact with this object
    public void Interact()
    {
        if (postMessage != "")
        {
            GameSceneManager.instance.StartTransition(postMessage);
        }
    }
}
