using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItems : MonoBehaviour
{
    // Store name of item
    [SerializeField]
    private string itemName;

    [SerializeField]
    private string[] interactionText;

    // Set type of the item
    [SerializeField]
    private PuzzleItemType puzzleItemType;

    [SerializeField]
    private bool isActive;

    // Set item Sprite of item to be shown on inventory
    [SerializeField]
    private Sprite itemSprite;

    [SerializeField]
    private PuzzleItems[] interactionChain;

    [SerializeField] 
    private PuzzleItems[] requirements;

    [SerializeField] 
    private PuzzleItems[] activationChain;


    public bool IsActive => isActive;

    public PuzzleItemType InterActionType { get => puzzleItemType; }

    public Sprite GetIcon { get => itemSprite; }

    //public string GetRequirementText { get => }







}
