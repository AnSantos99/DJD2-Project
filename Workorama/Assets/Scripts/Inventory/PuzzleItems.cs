using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItems : MonoBehaviour
{
    // Store name of item
    [SerializeField]
    private string itemName;

    [SerializeField]
    private bool isActive;

    // Set type of the item
    [SerializeField]
    private PuzzleItemType puzzleItemType;

    // Set item Sprite of item to be shown on inventory
    [SerializeField]
    private Sprite itemSprite;

    [SerializeField]
    private string requirementText;

    [SerializeField]
    private PuzzleItems[] requirements;

    [SerializeField]
    private PuzzleItems[] activationChain;

    [SerializeField]
    private string[] interactionTexts;

    [SerializeField]
    private PuzzleItems[] interactionChain;


    private Animator animator;
    private Transform transform;
    private CameraMovement camSwitch;
    private int curInteractionTextId;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        camSwitch = GetComponent<CameraMovement>();
        curInteractionTextId = 0;
    }



    /*-------------------------------------------------------------------------
     --------------------------------------------------------------------------
     ------------------------------------------------------------------------*/

    /// <summary>
    /// Check if item is active
    /// </summary>
    /// <returns></returns>
    public bool IsActive() => isActive;

    /// <summary>
    /// Get Puzzle item type
    /// </summary>
    /// <returns> enum of puzzle item type </returns>
    public PuzzleItemType InterActionType() => puzzleItemType;

    /// <summary>
    /// Get icon of item
    /// </summary>
    /// <returns> icon for inventory slot </returns>
    public Sprite GetIcon() => itemSprite; 
    
    /// <summary>
    /// Get text show player what is required to get that item
    /// </summary>
    /// <returns> information of item status </returns>
    public string GetRequirementText() => requirementText;
    
    /// <summary>
    /// Gets the current requirement text
    /// </summary>
    /// <returns></returns>
    public string GetCurrentRequirementText() 
        => interactionTexts[curInteractionTextId];

    /// <summary>
    /// Puzzle requirements to be able to solve the puzzle
    /// </summary>
    /// <returns> requirements that are needed to solve puzzle </returns>
    public PuzzleItems[] PuzzleRequirement() => requirements;

    /// <summary>
    /// To activate animation of items
    /// </summary>
    public void Activate()
    {
        isActive = true;

        // ANimator
    }

    /// <summary>
    /// Interaction with different types of items
    /// </summary>
    public void Interact()
    {
        if(isActive)
        {
            if(puzzleItemType == PuzzleItemType.INDIRECT) 
            {
                if (itemName == "Building")
                {
                    RorateBuilding();
                }
                if (itemName == "Camera")
                {
                    if (Check2DSwitch())
                    {
                        SwitchTo2D();
                        //Stop Player movement, cam movement
                    }
                    else
                    {
                        SwitchTo3D();
                    }
                }
            }

            else if(puzzleItemType == PuzzleItemType.PICKABLE)
            {
                GetComponent<Collider>().enabled = false;
                gameObject.SetActive(false);
            }

            else if(puzzleItemType == PuzzleItemType.INTERACT_ONCE)
                GetComponent<Collider>().enabled = false;

            else if(puzzleItemType == PuzzleItemType.INTERACT_MULTIPLE)
                curInteractionTextId = (curInteractionTextId + 1) 
                    % interactionTexts.Length;

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


    private void RorateBuilding()
    {
        transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
    }

    private bool Check2DSwitch()
    {
        return true;
        /*if (transform.position != new Vector3(270,8,0))
        {
            return true;
        }
        return false;*/
    }

    private void SwitchTo2D()
    {
        if (this.gameObject.layer == 6)
        {
            camSwitch.SwitchNorth();
        }
        else if (this.gameObject.layer == 7)
        {
            camSwitch.SwitchSouth();
        }
        else if (this.gameObject.layer == 8)
        {
            camSwitch.SwitchEast();
        }
        else if (this.gameObject.layer == 9)
        {
            camSwitch.SwitchWest();
        }
        else
        {
            Debug.Log("Window in the incorrect layer: " + this.gameObject);
        }
    }

    private void SwitchTo3D()
    {
        camSwitch.ReturnToPlayer();
    }
}
