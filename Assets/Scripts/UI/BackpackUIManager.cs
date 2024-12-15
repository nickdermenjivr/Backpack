using System;
using System.Collections.Generic;
using Configs;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BackpackManagerUI : MonoBehaviour
    {
        [SerializeField] private List<ItemSlotUI> itemSlotsUI;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        private Dictionary<ItemType, ItemSlotUI> _backpackSlotsUI;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _backpackSlotsUI = new Dictionary<ItemType, ItemSlotUI>();

            foreach (var slotUI in itemSlotsUI)
            {
                _backpackSlotsUI[slotUI.itemType] = slotUI;
            }
        }

        private void AddToBackpackUI(ItemUI item)
        {
            SetItemInUISlot(item);

            Debug.LogError($"No slot found for item type {item.itemConfig}");
        }

        private void RemoveFromBackpackUI(ItemUI item)
        {
            if (_backpackSlotsUI.TryGetValue(item.itemConfig.type, out var slotUI))
            {
                Debug.Log($"Item {item.name} removed to UI in slot {item.itemConfig.type}");
            }

            Debug.LogError($"No slot found for item type {item.itemConfig.type}");
        }
        
        private void SetItemInUISlot(ItemUI item)
        {
            if (!_backpackSlotsUI.TryGetValue(item.itemConfig.type, out var slotUI))
            {
                Debug.LogWarning($"No slot found for item type: {item.itemConfig.type}");
                return;
            }
            
            item.transform.SetParent(slotUI.slotTransform);
            AnimationHandler.SmoothSetToPositionAndScale(item.transform, slotUI.slotTransform, 0.3f);
        }
    }

    [Serializable]
    public class ItemSlotUI
    {
        public ItemType itemType;
        public Transform slotTransform;
    }
}
