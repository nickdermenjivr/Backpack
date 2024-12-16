using System.Linq;
using _Project.Scripts.Core;
using _Project.Scripts.Events;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class ItemDragHandler : DragObject
    {
        private Transform _resetItemPosition;
        
        private Item _item;
        private BackpackManager _backpackManager;

        private void Awake()
        {
            if (!TryGetComponent(out _item)) Debug.LogError($"'Item' isn't assigned to {name}");
            _backpackManager = FindObjectOfType<BackpackManager>();
            _resetItemPosition = GameObject.Find("ResetItemPosition").transform;
            
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
                    if (eventData.ItemConfig == _item.itemConfig) DetachItem();
                    break;
            }
        }
        private void DetachItem()
        {
            CanDrag = true;
            _item.transform.position = _resetItemPosition.position;
            base.OnMouseUp();
        }
        protected override void OnMouseUp()
        {
            if (!CanDrag) return;
            if (IsOverBackpack())
            {
                _backpackManager.AddToBackpack(_item);
                CanDrag = false;
            }
            else
                base.OnMouseUp();
        }
        

        private bool IsOverBackpack()
        {
            var colliders = Physics.OverlapBox(transform.position, GetComponent<Collider>().bounds.extents, Quaternion.identity);

            return colliders.Any(col => col.CompareTag("Backpack"));
        }
    }
}