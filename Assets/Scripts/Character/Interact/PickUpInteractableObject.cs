using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteractableObject : MonoBehaviour
{
    [SerializeField] int coinCount;
    [SerializeField] ItemData itemData;

    private void Start()
    {
        GetComponent<InteractableObject>().Subscribe(PickUp);
    }

    public void PickUp(Inventory inventory)
    {
        inventory.AddCurrency(coinCount);

        if (itemData != null)
        {
            inventory.AddItem(itemData);
        }

        Debug.Log("You are trying to pick me up!");
        Destroy(gameObject);
    }

    internal void SetItem(ItemData itemToSpawn)
    {
        itemData = itemToSpawn;
    }
}
