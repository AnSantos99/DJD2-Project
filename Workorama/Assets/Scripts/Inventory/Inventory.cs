using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// To store picked up items in a collection
    /// </summary>
    private ICollection<PuzzleItems> puzzleItemsList;

    // Variable of type canvasManager

    public Inventory()
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
        // Make it appear in canvas
    }

    /// <summary>
    /// Remove object from inventory
    /// </summary>
    /// <param name="puzzleItem"></param>
    public void RemoveFromInventory(PuzzleItems puzzleItem) 
    {
        puzzleItemsList.Remove(puzzleItem);

        // Make it dissapear from canvas
    }

    
    public bool IsInInventory(PuzzleItems item) 
        => puzzleItemsList.Contains(item);

    /// <summary>
    /// Check if there is any item inside the inventory list
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CheckItemInInventory(PuzzleItems item) 
        => puzzleItemsList.Contains(item);
}
