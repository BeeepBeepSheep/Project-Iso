using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector] public ItemGrid selectedItemGrid;

    Vector2Int positionOnGrid;
    InventoryItem selectedItem;
    RectTransform selectedItemRectTransform;

    [SerializeField] List<ItemData> itemDatas;
    [SerializeField] GameObject inventoryItemPrefab;
    [SerializeField] Transform targetCanvas;

    private void Update()
    {
        ProcessMouseInput();

        if(Input.GetKeyDown(KeyCode.A))
        {
            AddRandomItemToInventory();
        }
    }

    private void AddRandomItemToInventory()
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, targetCanvas);

        InventoryItem newInventoryItem = newItemGO.GetComponent<InventoryItem>();
        selectedItem = newInventoryItem;

        RectTransform newItemrectTransform = newItemGO.GetComponent<RectTransform>();
        newItemrectTransform.SetParent(targetCanvas);
        selectedItemRectTransform = newItemrectTransform;
        
        int selectedItemID = UnityEngine.Random.Range(0, itemDatas.Count);
        newInventoryItem.Set(itemDatas[selectedItemID]);
    }

    private void ProcessMouseInput()
    {
        if (selectedItem != null)
        {
            selectedItemRectTransform.position = Input.mousePosition;
        }

        if (selectedItemGrid == null) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            positionOnGrid = selectedItemGrid.GetTileGridPosition(Input.mousePosition);
            if (selectedItem == null)
            {
                selectedItem = selectedItemGrid.PickUpItem(positionOnGrid);
                if (selectedItem != null)
                {
                    selectedItemRectTransform = selectedItem.GetComponent<RectTransform>();
                }               
            }
            else
            {
                selectedItemGrid.PlaceItem(selectedItem, positionOnGrid.x, positionOnGrid.y);
                selectedItem = null;
                selectedItemRectTransform = null;
            }
        }
    }
}
