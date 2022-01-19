using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject interactionPanel;

    [SerializeField]
    private Text interactionText;

    [SerializeField]
    private Image[] inventoryIcons;

    private void Start()
    {
        HideInteractionPanel();
    }

    public void HideInteractionPanel() => interactionPanel.SetActive(false);

    public void ShowInteractionPanel(string message)
    {
        interactionText.text = message;
        interactionPanel.SetActive(true);
    }

    public void SetInventoryIcon(int i, Sprite icon)
    {
        inventoryIcons[i].sprite = icon;
        inventoryIcons[i].color = Color.white;
    }

    /// <summary>
    /// 
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
