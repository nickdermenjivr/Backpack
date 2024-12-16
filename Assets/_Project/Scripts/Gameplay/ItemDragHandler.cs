using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class ItemDragHandler : DragObject
    {
        private Item _item;
        private BackpackManager _backpackManager;

        private void Awake()
        {
            if (!TryGetComponent(out _item)) Debug.LogError($"'Item' isn't assigned to {name}");
            _backpackManager = FindObjectOfType<BackpackManager>();
            if (_backpackManager == null) Debug.LogError($"'BackpackManager' isn't found in the scene.");
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