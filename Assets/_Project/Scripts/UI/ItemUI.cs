using System.Collections.Generic;
using System.Linq;
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
            if (IsPointerOverUIElement(gameObject))
            {
                OnMouseReleasedOverUI();
            }
        }

        private bool IsPointerOverUIElement(GameObject targetObject)
        {
            var pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            return results.Any(result => result.gameObject == targetObject);
        }

        private void OnMouseReleasedOverUI()
        {
            if (itemConfig!=null)
            {
                EventManager.TriggerEvent(new ItemAddedOrRemovedToBackpackEvent 
                    { ItemConfig = itemConfig, Action = "removed" });
            }
        }
    }
}