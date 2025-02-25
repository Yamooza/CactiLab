using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryData", menuName = "AUD/New Inventory Data", order = 1)]
public class InventoryData : ScriptableObject
{
    public List<InventoryItem> items;

    public bool AddItemToInventory(ItemData newItem, int amount)
    {
        // k‰yd‰‰n l‰pi nykyinen inventory lista 
        foreach (InventoryItem invItem in items)
        {
            // Jos uusi itemi ON jo listalla ja max stack ei ole saavutettu lis‰t‰‰n se stackkiin
            if (invItem.itemData == newItem && newItem.canStack)
            {
                invItem.amount += amount;
                return false;
            }
        }

        // Jos itemi‰ ei lˆydy listalta lis‰t‰‰n uusi itemi inventoryyn
        items.Add(new InventoryItem(newItem, amount));
        return true;
    }

    public bool RemoveFromInventory(ItemData item)
    {
        // otetaan talteen lˆydetty itemi inventorysta
        InventoryItem foundItem = null;

        // K‰yd‰‰n l‰pi inventory lista ja etsit‰‰n haluttu itemi
        foreach (InventoryItem invItem in items)
        {
            // Jos itemi lˆydet‰‰n ja sen amount on suurempi kuin 0
            if (invItem.itemData == item && invItem.amount > 0)
            {
                // v‰hennet‰‰n itemin amounttia yhdell‰
                invItem.amount--;
                foundItem = invItem;
                break;
            }
        }

        // Jos itemin amount on nolla tai pienempi, sitten poistetaan itemi inventoryst‰.
        if (foundItem != null && foundItem.amount <= 0)
        {
            items.Remove(foundItem);
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetItemAmount(ItemData item)
    {
        foreach (InventoryItem invItem in items)
        {
            if (invItem.itemData == item)
            {
                return invItem.amount;
            }
        }

        return 0;
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int amount;

    public InventoryItem(ItemData itemData, int amount)
    {
        this.itemData = itemData;
        this.amount = amount;
    }
}
