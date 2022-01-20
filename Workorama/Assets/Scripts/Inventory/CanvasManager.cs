using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for the showing of information into the screen
/// </summary>
public class CanvasManager : MonoBehaviour
{
    // Interaction panel for objects
    [SerializeField]
    private GameObject interactionPanel;

    // Interaction text for objects
    [SerializeField]
    private Text interactionText;

    // Inventory icons for inventory panel on canvas
    [SerializeField]
    private Image[] inventoryIcons;

    private void Start()
    {
        HideInteractionPanel();
    }

    /// <summary>
    /// Hide Panel during game
    /// </summary>
    public void HideInteractionPanel() => interactionPanel.SetActive(false);

    /// <summary>
    /// Make it visible with the corresponding message attached to a object
    /// </summary>
    /// <param name="message"> Receive the text attached to the gameobject
    /// </param>
    public void ShowInteractionPanel(string message)
    {
        interactionText.text = message;
        interactionPanel.SetActive(true);
    }

    /// <summary>
    /// Set the sprites of the objects on the inventory panel when adding
    /// </summary>
    /// <param name="i"></param>
    /// <param name="icon"></param>
    public void SetInventoryIcon(int i, Sprite icon)
    {
        inventoryIcons[i].sprite = icon;
        inventoryIcons[i].color = Color.white;
    }

    /// <summary>
    /// Clear the icons after removing object from inventory
    /// </summary>
    public void ClearInventoryIcons()
    {
        for(int i = 0; i < inventoryIcons.Length; i++)
        {
            inventoryIcons[i].sprite = null;
            inventoryIcons[i].color = Color.clear;
        }
    }
}
