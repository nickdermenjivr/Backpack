using System;
using System.Collections.Generic;
using Configs;
using Core;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BackpackManagerUI : MonoBehaviour
    {
        [SerializeField] private List<ItemSlotUI> itemSlotsUI;
        [SerializeField] private GameObject canvas;
        
        private Dictionary<ItemType, ItemSlotUI> _backpackSlotsUI;

        private void Awake()
        {
            CloseCanvas();
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
            }
            else
                Debug.LogWarning($"No slot found for item type: {item.type}");
        }
        private void OpenCanvas()
        {
            canvas.SetActive(true);
        }
        private void CloseCanvas()
        {
            canvas.SetActive(false);
        }
    }

    [Serializable]
    public class ItemSlotUI
    {
        public ItemType itemType;
        public Image image;
    }
}
