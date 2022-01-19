using UnityEngine;

public class Interaction : MonoBehaviour
{
    private const float INTERACTION_DISTANCE = 3.0f;

    [SerializeField]
    private CanvasManager canvasManager;

    private Transform cameraTransform;

    private PuzzleItems currentInteractiveItems;

    private Inventory inventory;



    // Check if puzzleItem has the requirements
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

    public void ClearCurrentInteractive()
    {
        currentInteractiveItems = null;
        canvasManager.HideInteractionPanel();
    }

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
    /// Check if player has the object required to be able to use unlock the
    /// next one.
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

    private void CheckForPlayerInteraction()
    {
        if (Input.GetMouseButtonDown(0) && 
            currentInteractiveItems != null && hasRequirements)
        {
<<<<<<< HEAD
            if (currentInteractiveItems.InterActionType() == PuzzleItemType.PICKABLE)
            {
                PickCurrentInteractive();
                FindObjectOfType<SoundManager>().Play("ItemPickup");
            } 
=======
            if (currentInteractiveItems.InterActionType() == PuzzleItemType.PICKABLE) 
            {
                PickCurrentInteractive();
            } 
                    
>>>>>>> 570a7e7ce310e2ea7b069ef500c3929c0f731cda

            else
                InteractWithCurrentInteractive();
        }
    }

    private void PickCurrentInteractive()
    {
        currentInteractiveItems.Interact();
        inventory.AddToInventory(currentInteractiveItems);
    }

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
