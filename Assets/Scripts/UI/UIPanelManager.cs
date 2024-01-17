using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject statsPanel;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            OpenInventory();
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            OpenStats();
        }
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }

    public void OpenStats()
    {
        statsPanel.SetActive(!statsPanel.activeInHierarchy);
    }
}
