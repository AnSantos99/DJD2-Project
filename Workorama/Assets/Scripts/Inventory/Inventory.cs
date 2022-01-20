using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that defines what the inventory is made of.
/// </summary>
public class Inventory
{
    /// <summary>
    /// To store picked up items in a collection
    /// </summary>
    private List<PuzzleItems> puzzleItemsList;

    /// <summary>
    /// Get access to canvasmanager for display of icons
    /// </summary>
    private CanvasManager canvasManager;

    /// <summary>
    /// Get access to canvas
    /// </summary>
    private Canvas canvas;

    public Inventory()
    {
        puzzleItemsList = new List<PuzzleItems>();
        GetCanvas();
    }

    /// <summary>
    /// Method that gets the canvas of the game
    /// </summary>
    private void GetCanvas()
    {
        canvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
        canvasManager = canvas.GetComponent<CanvasManager>();
    }

    /// <summary>
    /// Add Object to inventory
    /// </summary>
    /// <param name="puzzleItem"></param>
    public void AddToInventory(PuzzleItems puzzleItem) 
    {
        // Add item to list
        puzzleItemsList.Add(puzzleItem);

        // Make it appear in canvas
        canvasManager.SetInventoryIcon(puzzleItemsList.Count -1, 
            puzzleItem.GetIcon());
    }

    /// <summary>
    /// Method to be able to select an item from inventory (for the future)
    /// </summary>
    /// <param name="puzzleItem"></param>
    public void SelectItemFromInventory(PuzzleItems puzzleItem) => 
        puzzleItemsList.Find((item) => puzzleItem);


    /// <summary>
    /// Remove object from inventory by removing it from the list and clearing
    /// the canvas
    /// </summary>
    /// <param name="puzzleItem"></param>
    public void RemoveFromInventory(PuzzleItems puzzleItem) 
    {
        puzzleItemsList.Remove(puzzleItem);

        // Clear it from canvas
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
