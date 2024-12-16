using System;
using System.Collections.Generic;
using _Project.Scripts.Configs;
using _Project.Scripts.Core;
using _Project.Scripts.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class BackpackManagerUI : MonoBehaviour
    {
        [SerializeField] private List<ItemUISlot> itemSlotsUI;
        private Dictionary<ItemType, Image> _backpackSlotsUI;
    
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
            _backpackSlotsUI = new Dictionary<ItemType, Image>();

            foreach (var slot in itemSlotsUI)
            {
                _backpackSlotsUI[slot.itemType] = slot.image;
            }
        }
        private void AddItem(ItemConfig item)
        {
            if (_backpackSlotsUI.TryGetValue(item.type, out var slotImage))
            {
                slotImage.enabled = true;
                
                var itemUI = slotImage.AddComponent<ItemUI>();
                itemUI.itemConfig = item;
            }
            else
                Debug.LogWarning($"No slot found for item type: {item.type}");
        }
        private void RemoveItem(ItemConfig item)
        {
            if (_backpackSlotsUI.TryGetValue(item.type, out var slotImage))
            {
                slotImage.enabled = false;
                Destroy(slotImage.GetComponent<ItemUI>());
                Debug.LogError($"Delete ItemUI from: {item.name}");
            }
            else
                Debug.LogWarning($"No slot found for item type: {item.type}");
        }
    }
    
    [Serializable]
    public class ItemUISlot
    {
        public ItemType itemType;
        public Image image;
    }
}
