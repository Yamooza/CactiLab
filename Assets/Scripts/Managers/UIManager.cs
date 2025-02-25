using Mono.Cecil.Cil;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Main Panels")]
    [SerializeField] GameObject Panel_MainMenu;
    [SerializeField] GameObject Panel_HUD;

    [Header("Inventory")]
    [SerializeField] Transform inventoryRootObj;
    [SerializeField] InventoryData playerInventory;

    private UI_Inventory_ItemSlot[] inventorySlots;

    [Header("Singleton")]
    public bool dontDestroyOnLoad = true;
    public static UIManager Instance { get; private set; }

    private void OnValidate()
    {
        if (inventoryRootObj != null)
        {
            inventorySlots = inventoryRootObj.GetComponentsInChildren<UI_Inventory_ItemSlot>();
        }
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        playerInventory.items.Clear();
    }

    public void ToggleMainMenuPanel(bool t)
    {
        Panel_MainMenu.SetActive(t);
        Panel_HUD.SetActive(!t);
    }

    public void TryAddToInventory(ItemData item, int amount)
    {
        // Lis�t��n itemi inventoryyn ja palautetaan tieto l�ytyyk� se jo inventoryst� vai ei
        bool isNewItem = playerInventory.AddItemToInventory(item, amount);

        // jos on uusi itemi ja sit� ei l�ytynyt, etsit��n ensimm�inen tyhj� slotti ja lis�t��n itemi siihen
        if (isNewItem)
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.currentSlotItemData == null)
                {
                    slot.currentSlotItemData = item;
                    slot.RefreshSlotData();
                    return;
                }
            }
        }
        else // jos l�yty inventoryst�, etsit��n oikea slotti johon itemi asetetaan ja p�ivitet��n data
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.currentSlotItemData == item)
                {
                    slot.RefreshSlotData();
                    return;
                }
            }
        }
    }

    public void TryRemoveItemFromInventory(ItemData item, UI_Inventory_ItemSlot slot)
    {
        bool removeFromSlot = playerInventory.RemoveFromInventory(item);

        if (removeFromSlot)
        {
            slot.currentSlotItemData = null;
        }
        slot.RefreshSlotData();
    }

    public int GetInventoryItemAmount(ItemData item)
    {
        return playerInventory.GetItemAmount(item);
    }

    public void ToggleInventory(bool t)
    {
        inventoryRootObj.gameObject.SetActive(t);
    }
}
