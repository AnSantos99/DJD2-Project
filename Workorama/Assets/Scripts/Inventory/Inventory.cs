using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// To store picked up items in a collection
    /// </summary>
    private List<PuzzleItems> puzzleItemsList;

    private CanvasManager canvasManager;
    private Canvas canvas;

    public Inventory()
    {

        //puzzleItemsList = GetComponent<List<PuzzleItems>>();
        puzzleItemsList = new List<PuzzleItems>();

        canvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
        canvasManager = canvas.GetComponent<CanvasManager>();
    }

    private void Start()
    {
        
    }


    /// <summary>
    /// Add Object to inventory
    /// </summary>
    /// <param name="puzzleItem"></param>
    public void AddToInventory(PuzzleItems puzzleItem) 
    {
        puzzleItemsList.Add(puzzleItem);

        // Make it appear in canvas
        canvasManager.SetInventoryIcon(puzzleItemsList.Count -1, puzzleItem.GetIcon());
    }

    /// <summary>
    /// Remove object from inventory
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
