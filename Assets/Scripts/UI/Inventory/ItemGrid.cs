using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemGrid : MonoBehaviour
{
    InventoryItem[,] inventoryItemGrid;

    public const float TileSizeWidth = 32f;
    public const float TileSizeHeight = 32f;

    [SerializeField] int gridSizeWidth;
    [SerializeField] int gridSizeHeight;

    RectTransform rectTransform;

    Vector2 mousePositionOnTheGrid;
    Vector2Int tileGridPosition = new Vector2Int();

    [SerializeField] GameObject inventoryItemPrefab;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Init(gridSizeWidth, gridSizeHeight);
    }

    private void Init(int width, int height)
    {
        inventoryItemGrid = new InventoryItem[width, height];
        Vector2 size = new Vector2();
        size.x = TileSizeWidth * width;
        size.y = TileSizeHeight * height;
        rectTransform.sizeDelta = size;
    }

    public void PlaceItem(InventoryItem itemToPlace, int x, int y)
    {
        RectTransform itemRectTransform = itemToPlace.GetComponent<RectTransform>();
        itemRectTransform.SetParent(transform);

        inventoryItemGrid[x, y] = itemToPlace;
        
        itemRectTransform.localPosition = CalculatePositionOfObjectOnGrid(itemToPlace, x, y);
    }

    private static Vector2 CalculatePositionOfObjectOnGrid(InventoryItem item, int x, int y)
    {
        Vector2 positionOnGrid = new Vector2();
        positionOnGrid.x = TileSizeWidth * x + TileSizeWidth * item.itemData.sizeWidth / 2;
        positionOnGrid.y = -(TileSizeHeight * y + TileSizeHeight * item.itemData.sizeHeight / 2);
        return positionOnGrid;
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        mousePositionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        mousePositionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(mousePositionOnTheGrid.x / TileSizeWidth);
        tileGridPosition.y = (int)(mousePositionOnTheGrid.y / TileSizeHeight);

        return tileGridPosition;
    }

    public InventoryItem PickUpItem(Vector2Int tilePositionOnGrid)
    {
        InventoryItem pickedItem = inventoryItemGrid[tilePositionOnGrid.x, tilePositionOnGrid.y];
        inventoryItemGrid[tilePositionOnGrid.x, tilePositionOnGrid.y] = null;
        return pickedItem;
    }
}
