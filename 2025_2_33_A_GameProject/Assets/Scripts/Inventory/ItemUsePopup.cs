using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUsePopup : MonoBehaviour
{
    public static ItemUsePopup Instance;

    public GameObject popupPanel;
    public Text itemNameText;
    public Image itemIcon;
    public Button useButton;
    public Button closeButton;

    private ItemData currentItem;
    private InventorySlot currentSlot;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void ShowPopup(ItemData item, InventorySlot slot)
    {
        currentItem = item;
        currentSlot = slot;

        itemNameText.text = item.itemName;
        itemIcon.sprite = item.itemIcon;

        useButton.interactable = item.isUsable;

        popupPanel.SetActive(true);
    }

    public void HidePopup()
    {
        popupPanel.SetActive(true);
    }
    void Useitem()
    {
        if (currentItem.isUsable)
        {
            PlayerStat player = FindObjectOfType<PlayerStat>();

            if (currentItem.healAmount > 0)
            {
                player.Heal(currentItem.healAmount);
            }
            else if(currentItem.healAmount < 0)
            {
                player.TakeDamage(currentItem.healAmount);
            }
                currentSlot.RemoveAmount(1);
        }
    }
}
