using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private PuzzleItems lever;

    private ICollection<PuzzleItems> puzzleItemsList;

    // Start is called before the first frame update
    void Start()
    {
        lever = new PuzzleItems("Lever", PuzzleItemType.PICKABLE, null);

        puzzleItemsList = new List<PuzzleItems>();

        puzzleItemsList.Add(lever);

        foreach (PuzzleItems item in puzzleItemsList)
        {
            Debug.Log(item.ItemName);
        }
        
    }

    private void AddToIventory(PuzzleItems puzzleItem) 
    {
        puzzleItemsList.Add(puzzleItem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
