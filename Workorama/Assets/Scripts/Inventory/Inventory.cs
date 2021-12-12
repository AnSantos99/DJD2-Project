using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class that contains member to create a inventory for the player
/// </summary>
public class Inventory 
{
    /// <summary>
    /// To store picked up items in a collection
    /// </summary>
    private List<PuzzleItems> puzzleItemsList;

    /// <summary>
    /// To get access to the canvasManager component in gameobject
    /// </summary>
    private CanvasManager canvasManager;

    /// <summary>
    /// Get to Canvas gameObject
    /// </summary>
    private Canvas canvas;

    /// <summary>
    /// Create Delegate to store method inside
    /// </summary>
    private Action action;

    /// <summary>
    /// Initialize 
    /// </summary>
    public Inventory()
    {
        puzzleItemsList = new List<PuzzleItems>();

        action = GetCanvas;

        action.Invoke();
    }

    /// <summary>
    /// Get acess to the canvas to get the CanvasManager script component
    /// </summary>
    private void GetCanvas()
    {
        canvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
        canvasManager = canvas.GetComponent<CanvasManager>();
    }


    /// <summary>
    /// Add Object to inventory
    /// </summary>
    /// <param name="puzzleItem"> Pickable puzzle item </param>
    public void AddToInventory(PuzzleItems puzzleItem) 
    {
        puzzleItemsList.Add(puzzleItem);

        // Make it appear in canvas
        canvasManager.SetInventoryIcon(puzzleItemsList.Count -1, 
            puzzleItem.GetIcon());
    }

    /// <summary>
    /// Remove object from inventory
    /// </summary>
    /// <param name="puzzleItem"> Pickable puzzle item </param>
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
    /// <param name="item"> Pickable item </param>
    /// <returns> The picked item stored in inventory </returns>
    public bool CheckItemInInventory(PuzzleItems item) 
        => puzzleItemsList.Contains(item);
}
