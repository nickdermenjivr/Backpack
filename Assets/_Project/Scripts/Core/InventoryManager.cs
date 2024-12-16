using System.Collections.Generic;
using _Project.Scripts.Events;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class InventoryManager : MonoBehaviour
    {
        private readonly List<ItemConfig> _inventoryItems = new ();
        private void Start()
        {
            EventManager.Subscribe<ItemAddedOrRemovedToBackpackEvent>(OnItemEvent);
        }

        private void OnDestroy()
        {
            EventManager.Unsubscribe<ItemAddedOrRemovedToBackpackEvent>(OnItemEvent);
        }
        

        private void OnItemEvent(ItemAddedOrRemovedToBackpackEvent eventData)
        {
            switch (eventData.Action)
            {
                case "added": 
                    AddItem(eventData.ItemConfig);
                    break;
                case "removed":
                    RemoveItem(eventData.ItemConfig);
                    break;
            }
        }
        private void AddItem(ItemConfig item)
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

        private void RemoveItem(ItemConfig item)
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