using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class InventoryManager : MonoBehaviour
    {
        private List<ItemConfig> _inventoryItems = new ();
        
        public void AddItem(ItemConfig item)
        {
            if (item != null && !_inventoryItems.Contains(item))
            {
                _inventoryItems.Add(item);
                Debug.Log($"Item {item.name} added to inventory.");
            }
            else
            {
                Debug.LogWarning($"Item {item?.name} is already in the inventory or is null.");
            }
        }

        public void RemoveItem(ItemConfig item)
        {
            if (item != null && _inventoryItems.Contains(item))
            {
                _inventoryItems.Remove(item);
                Debug.Log($"Item {item.name} removed from inventory.");
            }
            else
            {
                Debug.LogWarning($"Item {item?.name} is not in the inventory or is null.");
            }
        }

        public List<ItemConfig> GetItems()
        {
            return new List<ItemConfig>(_inventoryItems);
        }
    }
}