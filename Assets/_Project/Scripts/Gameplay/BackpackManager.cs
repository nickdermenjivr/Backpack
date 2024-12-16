using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Configs;
using _Project.Scripts.Core;
using _Project.Scripts.Events;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class BackpackManager : MonoBehaviour
    {
        [SerializeField] private List<ItemSlot> itemSlots;
        private Dictionary<ItemType, Transform> _backpackSlots;
        private List<ItemDataForBackpack> _backpackItemsData;

        private void Awake()
        {
            Initialize();
        }
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
                case "removed": 
                    RemoveFromBackpack(eventData.ItemConfig);
                    break;
            }
        }
        private void Initialize()
        {
            _backpackItemsData = new List<ItemDataForBackpack>();
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
        
        private void RemoveFromBackpack(ItemConfig item)
        {
            DetachItemFromSlot(item);
            Debug.Log($"Item {item.name} removed from backpack from slot {item.type}");
        }

        private void SetItemInSlot(Item item)
        {
            if (_backpackSlots.TryGetValue(item.itemConfig.type, out var slotTransform))
            {
                _backpackItemsData.Add(new ItemDataForBackpack(item));
                AnimationHandler.SmoothSetToPositionAndScale(item.transform, slotTransform, 0.3f );
            }
            else
                Debug.LogWarning($"No slot found for item type: {item.itemConfig.type}");
        }
        
        private void DetachItemFromSlot(ItemConfig item)
        {
            var itemData = _backpackItemsData.FirstOrDefault(i => i.item.itemConfig == item);

            if (itemData == null) return;
            itemData.item.transform.SetParent(itemData.initialParent);
            AnimationHandler.SmoothScale(itemData.item.transform, itemData.initialScale, 0.3f );
            _backpackItemsData.Remove(itemData);
        }
    }

    [Serializable]
    public class ItemSlot
    {
        public ItemType itemType;
        public Transform slotTransform;
    }

    [Serializable]
    public class ItemDataForBackpack
    {
        public Item item;
        public Vector3 initialScale;
        public Transform initialParent;

        public ItemDataForBackpack(Item newItem)
        {
            item = newItem;
            initialScale = newItem.transform.localScale;
            initialParent = newItem.transform.parent;
        }
    }
}