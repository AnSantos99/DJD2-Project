using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private const float INTERACTION_DISTANCE = 2.0f;

    [SerializeField]
    public CanvasManager canvasManager;

    private Transform cameraTransform;
    private PuzzleItems currentInteractiveItems;

    private Inventory inventory;

    private bool hasRequirements;

        
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        hasRequirements = false;
        currentInteractiveItems = null;

        inventory = new Inventory();
    }

    // Update is called once per frame
    void Update()
    {
        LockForInteractive();
        CheckForPlayerInteraction();
    }

    private void LockForInteractive()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,
        out RaycastHit hitInfo, INTERACTION_DISTANCE))
        {
            PuzzleItems interactiveItems = hitInfo.collider.GetComponent<PuzzleItems>();

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
            canvasManager.ShowInteractionPanel(item.GetCurrentRequirementText());
        else
            canvasManager.ShowInteractionPanel(item.GetRequirementText());

    }

    private bool PlayerHasInteractionRequirements()
    {
        hasRequirements = false;

        PuzzleItems[] requirements = currentInteractiveItems.PuzzleRequirement();

        if (requirements != null)
            for (int i = 0; i < requirements.Length; ++i)
                if (!inventory.CheckItemInInventory(requirements[i]))
                    return false;

        hasRequirements = true;
        return true;
    }

    private void CheckForPlayerInteraction()
    {
        if (Input.GetMouseButtonDown(0) && currentInteractiveItems != null && hasRequirements)
        {
            if (currentInteractiveItems.InterActionType() == PuzzleItemType.PICKABLE)
                PickCurrentInteractive();
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
