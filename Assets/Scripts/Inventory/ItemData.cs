using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "AUD/Create New Item", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public bool canStack = true;
    public int itemValue;
}

public enum ItemType
{
    Junk,
    Consumable,
    Equipment,
    Weapon
}
