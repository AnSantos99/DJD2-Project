using System.Collections;
using UnityEngine;

/// <summary>
/// Class that defines what a puzzle item is
/// </summary>
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

    // Get the requirement text for a puzzle object
    [SerializeField]
    private string requirementText;

    // Setup the requirements that are needed to meet to be able to continue
    [SerializeField]
    private PuzzleItems[] requirements;

    // Activate specific puzzle items
    [SerializeField]
    private PuzzleItems[] activationChain;

    // Set the interaction text of the puzzle item
    [SerializeField]
    private string[] interactionTexts;

    // Activates another puzzle item after the previous one has been done
    [SerializeField]
    private float timeRotating = 3.0f;

    [SerializeField]
    private PuzzleItems[] interactionChain;

    private PlayerLook playerLook;
    private PlayerMov playerMov;

    // Get the animators of the components
    private Animator animator;

    // Get the transform of the components
    private Transform transform;

    // Get the cameraMovemnt script to acess the camera
    private CameraMovement camSwitch;

    // set up the current text ID
    private int curInteractionTextId;

    private void Start() 
    {   
        playerLook = (PlayerLook)FindObjectOfType(typeof(PlayerLook));
        playerMov = (PlayerMov)FindObjectOfType(typeof(PlayerMov));
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        camSwitch = (CameraMovement)FindObjectOfType(typeof(CameraMovement));

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
    public void Activate() => isActive = true;

    /// <summary>
    /// Interaction with different types of items
    /// </summary>
    public void Interact()
    {
        if(isActive)
        {
            if (animator != null)
                animator.SetTrigger("Interact");

            if (puzzleItemType == PuzzleItemType.INDIRECT) 
            {
                // If the object is Building rotate when interact
                if (itemName == "Building")
                    RorateBuilding();

                // if the object is camera switch view when interact
                if (itemName == "Camera")
                    camSwitch.SwitchCam();

                // if the object is a number for puzzle, rotate to make it visible
                if (itemName == "Number")
                    RotateObject();
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

    /// <summary>
    /// Check if there are elements in the chain and Activate them
    /// </summary>
    private void ProcessActivationChain()
    {
        if(activationChain != null)
        {
            for(int i = 0; i < activationChain.Length; i++)
                activationChain[i].Activate();
        }
    }

    /// <summary>
    /// Check if there are elements in the chain and make them interactable
    /// </summary>
    private void ProcessInteractionChain()
    {
        if(interactionChain != null)
        {
            for(int i = 0; i < interactionChain.Length; i++)
                interactionChain[i].Interact();
        }
    }

    /// <summary>
    /// Rotate the building
    /// </summary>
    private void RorateBuilding()
    {
        transform.Rotate(0, 90, 0);
        playerLook.enabled = false;
        playerMov.enabled = false;

        camSwitch.CamShakeDown(timeRotating);
    }

    /// <summary>
    /// Rotate the Object 90 degrees in the x axis
    /// </summary>
    private void RotateObject() => transform.Rotate(0, 0, -90);
}
