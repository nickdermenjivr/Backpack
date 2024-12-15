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
            Initialize();
        }

        private void Initialize()
        {
            _backpackSlots = new Dictionary<ItemType, Transform>();

            foreach (var slot in itemSlots)
            {
                _backpackSlots[slot.itemType] = slot.slotTransform;
            }
        }

        public void AddToBackpack(Item item)
        {
            SetItemInSlot(item);
            EventManager.TriggerEvent(new ItemAddedOrRemovedToBackpackEvent
                { ItemConfig = item.itemConfig, Action = "added" });

            Debug.Log($"Item {item.itemConfig.name} added to backpack in slot {item.itemConfig.type}");
        }

        public void RemoveFromBackpack(Item item)
        {
            if (!_backpackSlots.ContainsKey(item.itemConfig.type))
            {
                Debug.LogError($"Slot for item type {item.itemConfig.type} not found!");
                return;
            }

            EventManager.TriggerEvent(new ItemAddedOrRemovedToBackpackEvent
                { ItemConfig = item.itemConfig, Action = "removed" });

            Debug.Log($"Item {item.name} removed from backpack.");
        }

        private void SetItemInSlot(Item item)
        {
            if (!_backpackSlots.TryGetValue(item.itemConfig.type, out var slotTransform))
            {
                Debug.LogWarning($"No slot found for item type: {item.itemConfig.type}");
                return;
            }

            AnimationHandler.SmoothSetToPositionAndScale(item.transform, slotTransform, 0.3f );
        }
    }

    [Serializable]
    public class ItemSlot
    {
        public ItemType itemType;
        public Transform slotTransform;
    }
}