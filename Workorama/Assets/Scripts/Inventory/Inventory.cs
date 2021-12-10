using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// To store picked up items in a collection
    /// </summary>
    private List<PuzzleItems> puzzleItemsList;

    private CanvasManager canvasManager;

    public void Start()
    {
        puzzleItemsList = new List<PuzzleItems>();
    }

    /// <summary>
    /// Add Object to inventory
    /// </summary>
    /// <param name="puzzleItem"></param>
    public void AddToInventory(PuzzleItems puzzleItem) 
    {
        puzzleItemsList.Add(puzzleItem);
        canvasManager.SetInventoryIcon
            (puzzleItemsList.Count - 1, puzzleItem.GetIcon());
        // Make it appear in canvas
    }

    /// <summary>
    /// Remove object from inventory
    /// </summary>
    /// <param name="puzzleItem"></param>
    public void RemoveFromInventory(PuzzleItems puzzleItem) 
    {
        puzzleItemsList.Remove(puzzleItem);

        canvasManager.ClearInventoryIcons();

        for (int i = 0; i < puzzleItemsList.Count; ++i)
            canvasManager.SetInventoryIcon(i, puzzleItemsList[i].GetIcon());
    }

    /// <summary>
    /// Check if there is any item inside the inventory list
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CheckItemInInventory(PuzzleItems item) 
        => puzzleItemsList.Contains(item);
}
