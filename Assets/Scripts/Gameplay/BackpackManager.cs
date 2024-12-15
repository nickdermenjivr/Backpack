using System;
using System.Collections.Generic;
using Configs;
using Core;
using Events;
using UnityEngine;

namespace Gameplay
{
    public class BackpackManager : MonoBehaviour
    {
        [SerializeField] private List<ItemSlot> itemSlots;
        private Dictionary<ItemType, Transform> _backpackSlots;
        private InventoryManager _inventoryManager;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _backpackSlots = new Dictionary<ItemType, Transform>();

            foreach (var slot in itemSlots)
            {
                _backpackSlots[slot.itemType] = slot.slotTransform;
            }

            _inventoryManager = FindObjectOfType<InventoryManager>();
        }

        public void AddToBackpack(Item item)
        {
            SetItemInSlot(item);
            _inventoryManager.AddItem(item.itemConfig);
            EventManager.TriggerEvent(new ItemAddedOrRemovedToBackpackEvent{ItemConfig = item.itemConfig, Action = "added"});
            Debug.Log($"Item {item.itemConfig.name} added to backpack in slot {item.itemConfig.type}");
        }

        public void RemoveFromBackpack(Item item)
        {
            if (!_backpackSlots.ContainsKey(item.itemConfig.type))
            {
                Debug.LogError($"Slot for item type {item.itemConfig.type} not found!");
                return;
            }

            EventManager.TriggerEvent(new ItemAddedOrRemovedToBackpackEvent{ItemConfig = item.itemConfig, Action = "removed"});
            _inventoryManager.RemoveItem(item.itemConfig);
            Debug.Log($"Item {item.name} removed from backpack.");
        }

        public void SetItemInSlot(Item item)
        {
            if (!_backpackSlots.TryGetValue(item.itemConfig.type, out var slotTransform))
            {
                Debug.LogWarning($"No slot found for item type: {item.itemConfig.type}");
                return;
            }
            
            item.transform.SetParent(slotTransform);
            item.transform.localPosition = Vector3.zero;
        }
    }
    
    [Serializable]
    public class ItemSlot
    {
        public ItemType itemType;
        public Transform slotTransform;
    }
}