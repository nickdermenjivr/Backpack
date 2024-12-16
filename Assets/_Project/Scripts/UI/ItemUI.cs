using _Project.Scripts.Core;
using _Project.Scripts.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI
{
    public class ItemUI : MonoBehaviour
    {
        public ItemConfig itemConfig;

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0)) return;
            if (IsPointerOverUIElement())
            {
                OnMouseReleasedOverUI();
            }
        }

        private bool IsPointerOverUIElement()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        private void OnMouseReleasedOverUI()
        {
            Debug.LogError($"Released mouse from - {itemConfig.name}");
            if (itemConfig!=null)
            {
                EventManager.TriggerEvent(new ItemAddedOrRemovedToBackpackEvent 
                    { ItemConfig = itemConfig, Action = "removed" });
            }
        }
    }
}