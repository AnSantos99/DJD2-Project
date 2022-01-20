using UnityEngine;

/// <summary>
/// Class that defines what interaction is and what is and what is not
/// interactable.
/// </summary>
public class Interaction : MonoBehaviour
{
    // Define an unchangeable value used for raycast
    private const float INTERACTION_DISTANCE = 3.0f;

    /// <summary>
    /// Get the canvas manager to be able to access interaction panels
    /// </summary>
    [SerializeField]
    private CanvasManager canvasManager;

    /// <summary>
    /// Get access to the cameraTranform component
    /// </summary>
    private Transform cameraTransform;

    /// <summary>
    /// Get access to this class in order to know the specific items that are
    /// interactable and which not.
    /// </summary>
    private PuzzleItems currentInteractiveItems;

    /// <summary>
    /// Get access to the inventory to be able to store, remove and manipulate
    /// the inventory of the player
    /// </summary>
    private Inventory inventory;

    // Check if puzzleItem has the requirements needed
    private bool hasRequirements;
  
    // Start is called before the first frame update
    private void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;

        hasRequirements = false;

        currentInteractiveItems = null;

        inventory = new Inventory();
    }

    // Update is called once per frame
    private void Update()
    {
        LockForInteractive();

        CheckForPlayerInteraction();
    }

    /// <summary>
    /// Check with raycast if there are interactive objects near player
    /// </summary>
    private void LockForInteractive()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,
        out RaycastHit hitInfo, INTERACTION_DISTANCE))
        {
            PuzzleItems interactiveItems = 
                hitInfo.collider.GetComponent<PuzzleItems>();

            if (interactiveItems == null || !interactiveItems.IsActive())
                ClearCurrentInteractive();

            else if (interactiveItems != currentInteractiveItems) 
                SetCurrentInteractive(interactiveItems);
        }
        else 
            ClearCurrentInteractive();      
    }

    /// <summary>
    /// hide canvas panel if there arent any interactable objects in the ray of
    /// the player
    /// </summary>
    public void ClearCurrentInteractive()
    {
        currentInteractiveItems = null;
        canvasManager.HideInteractionPanel();
    }

    /// <summary>
    /// Activate the panel if the requirements are meet meaning if player is in
    /// vision of a interactable object
    /// </summary>
    /// <param name="item"> Interactable object of type puzzle item </param>
    private void SetCurrentInteractive(PuzzleItems item)
    {
        currentInteractiveItems = item;

        if (PlayerHasInteractionRequirements())
            canvasManager.ShowInteractionPanel(
                item.GetCurrentRequirementText());
        else
            canvasManager.ShowInteractionPanel(item.GetRequirementText());
    }

    /// <summary>
    /// Check if player has the object required to be able to use and unlock 
    /// the next one. Else if condition is not met, dont unlock it.
    /// </summary>
    /// <returns> true if condition above is meet </returns>
    private bool PlayerHasInteractionRequirements()
    {
        hasRequirements = false;

        PuzzleItems[] requirements = 
            currentInteractiveItems.PuzzleRequirement();
        
        if (requirements != null)
            for (int i = 0; i < requirements.Length; ++i)
                if (!inventory.CheckItemInInventory(requirements[i]))
                    return false;

        hasRequirements = true;
        return true;
    }

    /// <summary>
    /// Method to check if all teh conditions are made in order for the player
    /// to be able to pick up Pickable objects. While that, play a sound for
    /// the pickup of the object
    /// </summary>
    private void CheckForPlayerInteraction()
    {
        if (Input.GetMouseButtonDown(0) && currentInteractiveItems != 
            null && hasRequirements)
        {
            if (currentInteractiveItems.InterActionType() == PuzzleItemType.PICKABLE)
            {
                PickCurrentInteractive();
                FindObjectOfType<SoundManager>().Play("ItemPickup");
            } 

            else
                InteractWithCurrentInteractive();
        }
    }

    /// <summary>
    /// Method that picks a current item that is interactable and adds it into
    /// the inventory
    /// </summary>
    private void PickCurrentInteractive()
    {
        currentInteractiveItems.Interact();
        inventory.AddToInventory(currentInteractiveItems);
    }

    /// <summary>
    /// Check if player has the current item needed for the next puzzle in 
    /// inventory in order to unlock it and remove the object from the inventory
    /// if true.
    /// </summary>
    private void InteractWithCurrentInteractive()
    {
        PuzzleItems[] requirements = currentInteractiveItems.PuzzleRequirement();

        if (requirements != null)
        {
            for (int i = 0; i < requirements.Length; ++i)
            {
                requirements[i].gameObject.SetActive(true);
                inventory.RemoveFromInventory(requirements[i]);
            }
        }
        currentInteractiveItems.Interact();
    }
}
