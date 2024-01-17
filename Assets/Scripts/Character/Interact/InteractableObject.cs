using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Action<Inventory> interact;
    public string objectName;

    private void Start()
    {
        objectName = gameObject.name;
    }

    // Public method called to interact with this object
    public void Interact(Inventory inventory)
    {
        interact?.Invoke(inventory);
    }
}