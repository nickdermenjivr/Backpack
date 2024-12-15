using System.Linq;
using Core;
using UnityEngine;

namespace Gameplay
{
    public class ItemDragHandler : DragObject
    {
        private Item _item;
        private BackpackManager _backpackManager;
        private InventoryManager _inventoryManager;

        private void Awake()
        {
            if (!TryGetComponent(out _item)) Debug.LogError($"'Item' isn't assigned to {name}");
            _backpackManager = FindObjectOfType<BackpackManager>();
            if (_backpackManager == null) Debug.LogError($"'BackpackManager' isn't found in the scene.");
            _inventoryManager = FindObjectOfType<InventoryManager>();
            if (_inventoryManager == null) Debug.LogError($"'InventoryManager' isn't found in the scene.");
        }

        protected override void OnMouseUp()
        {
            var itemsInInventory = _inventoryManager.GetItems();
            
            if (!IsOverBackpack() || itemsInInventory.Contains(_item.itemConfig)) return;
            _backpackManager.AddToBackpack(_item);
            CanDrag = false;
        }

        private bool IsOverBackpack()
        {
            var colliders = Physics.OverlapBox(transform.position, GetComponent<Collider>().bounds.extents, Quaternion.identity);

            return colliders.Any(col => col.CompareTag("Backpack"));
        }
    }
}