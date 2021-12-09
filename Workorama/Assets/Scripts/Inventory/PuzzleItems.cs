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

    [SerializeField]
    private string requirementTexts; 

    private int curInteractionTextId;


    public bool IsActive() => isActive;

    public PuzzleItemType InterActionType() => puzzleItemType;

    public Sprite GetIcon() => itemSprite; 
    
    public string GetRequirementText() => requirementTexts;
    
    public string GetCurrentRequirementText() => interactionText[curInteractionTextId];

    public PuzzleItems[] PuzzleRequirement() => requirements;

    public void Activate()
    {
        isActive = true;

        // ANimator
    }

    public void Interact()
    {
        if(isActive)
        {
            //if() animator != null interact

            if(puzzleItemType == PuzzleItemType.PICKABLE)
            {
                GetComponent<Collider>().enabled = false;
                gameObject.SetActive(false);
            }

            else if(puzzleItemType == PuzzleItemType.INTERACT_ONCE)
                GetComponent<Collider>().enabled = false;

            else if(puzzleItemType == PuzzleItemType.INTERACT_MULTIPLE)
                curInteractionTextId = (curInteractionTextId + 1) 
                    % interactionText.Length;

            ProcessActivationChain();
            ProcessInteractionChain();
        }
    }

    private void ProcessActivationChain()
    {
        if(activationChain != null)
        {
            for(int i = 0; i < activationChain.Length; i++)
                activationChain[i].Activate();
        }
    }

    private void ProcessInteractionChain()
    {
        if(interactionChain != null)
        {
            for(int i = 0; i < interactionChain.Length; i++)
                interactionChain[i].Interact();
        }
    }
}
