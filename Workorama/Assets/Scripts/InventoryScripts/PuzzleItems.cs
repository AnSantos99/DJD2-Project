using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItems : MonoBehaviour
{
    // Store name of item
    private string itemName;

    // Set type of the item
    [SerializeField]
    private PuzzleItemType puzzleItemType;

    // Set item Sprite of item to be shown on inventory
    [SerializeField]
    private Sprite itemSprite;

    public PuzzleItems(string itemName, PuzzleItemType puzzleItemType, 
        Sprite itemSprite) 
    {
        this.itemName = itemName;
        this.puzzleItemType = puzzleItemType;
        this.itemSprite = itemSprite;
    }

    public string ItemName { get => itemName; }


}
