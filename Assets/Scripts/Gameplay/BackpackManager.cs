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


        private void Awake()
        {
            _backpackSlots = new Dictionary<ItemType, Transform>();

            foreach (var slot in itemSlots)
            {
                _backpackSlots[slot.itemType] = slot.slotTransform;
            }
        }

        public void AddToBackpack(ItemConfig item)
        {
            if (!_backpackSlots.ContainsKey(item.type))
            {
                Debug.LogError($"Slot for item type {item.type} not found!");
                return;
            }

            EventManager.TriggerEvent(new ItemAddedOrRemovedToBackpackEvent(){Item = item, Action = "added"});
            Debug.Log($"Item {item.name} added to backpack in slot {item.type}");
        }

        public void RemoveFromBackpack(ItemConfig item)
        {
            if (!_backpackSlots.ContainsKey(item.type))
            {
                Debug.LogError($"Slot for item type {item.type} not found!");
                return;
            }

            EventManager.TriggerEvent(new ItemAddedOrRemovedToBackpackEvent(){Item = item, Action = "removed"});
            Debug.Log($"Item {item.name} removed from backpack.");
        }
    }
    
    [Serializable]
    public class ItemSlot
    {
        public ItemType itemType;
        public Transform slotTransform;
    }
}