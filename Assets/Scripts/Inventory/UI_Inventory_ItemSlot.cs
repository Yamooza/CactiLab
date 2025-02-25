using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory_ItemSlot : MonoBehaviour
{
    [Header("Item Data")]
    public ItemData currentSlotItemData;

    [Header("UI References")]
    [SerializeField] Image slotIconImage;
    [SerializeField] TextMeshProUGUI slotAmountText;

    Button slotBtn;

    private void OnValidate()
    {
        slotBtn = GetComponent<Button>();
    }

    private void Awake()
    {
        slotBtn = GetComponent<Button>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotBtn.onClick.AddListener(InvokeRemoveItem);
    }

    public void RefreshSlotData()
    {
        if (currentSlotItemData == null)
        {
            slotIconImage.enabled = false;
            slotIconImage.sprite = null;
            slotAmountText.text = "";
        }
        else
        {
            slotIconImage.enabled = true;
            slotIconImage.sprite = currentSlotItemData.itemIcon;
            slotAmountText.text = UIManager.Instance.GetInventoryItemAmount(currentSlotItemData) + "x";
        }
    }

    void InvokeRemoveItem()
    {
        if (currentSlotItemData == null) return;

        GameManager.Instance.Player.GetComponent<PlayerInteraction>().DropItem(currentSlotItemData);
        UIManager.Instance.TryRemoveItemFromInventory(currentSlotItemData, this);
    }
}
