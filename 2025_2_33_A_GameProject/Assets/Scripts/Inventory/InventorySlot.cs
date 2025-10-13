using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemData item;
    public int amount;

    [Header("UI References")]
    public Image itemIcon;
    public Text amountTxt;
    public GameObject emptySlotImage;

    private void Update()
    {
        UpdateSlotUI();
    }

    public void SetItem(ItemData newitem, int newAmount)
    {
        item = newitem;
        amount = newAmount;
    }

    void UpdateSlotUI()
    {
        if (item != null)
        {
            itemIcon.sprite = item.itemIcon;
            itemIcon.enabled = true;

            amountTxt.text = amount > 1 ? amount.ToString() : "";
            if (emptySlotImage != null)
            {
                emptySlotImage.SetActive(false);
            }
        }
        else
        {
            itemIcon.enabled = false;
            amountTxt.text = "";
            if (emptySlotImage != null)
            {
                emptySlotImage.SetActive(true);
            }
        }
    }

    public void Addmount(int vaiue)
    {
        amount += vaiue;
        UpdateSlotUI();    
    }

    public void RemoveAmount(int value)
    {
        amount -= value;

        if (amount <= 0)
            ClearSlot();
        else
            UpdateSlotUI();
            
    }

    public void ClearSlot()
    {
        item = null;
        amount = 0;
        UpdateSlotUI();
    }


}
