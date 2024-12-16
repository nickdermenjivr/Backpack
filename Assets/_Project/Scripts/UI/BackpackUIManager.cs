using System;
using System.Collections.Generic;
using _Project.Scripts.Configs;
using _Project.Scripts.Core;
using _Project.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class BackpackManagerUI : MonoBehaviour
    {
        [SerializeField] private List<ItemSlotUI> itemSlotsUI;
        
        private Dictionary<ItemType, ItemSlotUI> _backpackSlotsUI;
    
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
                case "added": 
                    AddItem(eventData.ItemConfig);
                    break;
                case "removed":
                    RemoveItem(eventData.ItemConfig);
                    break;
            }
        }
        private void Initialize()
        {
            _backpackSlotsUI = new Dictionary<ItemType, ItemSlotUI>();

            foreach (var slotUI in itemSlotsUI)
            {
                _backpackSlotsUI[slotUI.itemType] = slotUI;
            }
        }
        private void AddItem(ItemConfig item)
        {
            if (_backpackSlotsUI.TryGetValue(item.type, out var itemSlotUI))
            {
                itemSlotUI.image.enabled = true;
            }
            else
                Debug.LogWarning($"No slot found for item type: {item.type}");
        }
        private void RemoveItem(ItemConfig item)
        {
            if (_backpackSlotsUI.TryGetValue(item.type, out var itemSlotUI))
            {
                itemSlotUI.image.enabled = false;
                EventManager.TriggerEvent(new ItemAddedOrRemovedToBackpackEvent
                    { ItemConfig = item, Action = "removed" });
            }
            else
                Debug.LogWarning($"No slot found for item type: {item.type}");
        }
    }

    [Serializable]
    public class ItemSlotUI
    {
        public ItemType itemType;
        public Image image;
    }
}
