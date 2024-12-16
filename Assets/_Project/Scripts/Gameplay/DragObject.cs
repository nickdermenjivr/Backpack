using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class DragObject : MonoBehaviour
    {
        private Camera _mainCamera;
        private bool _isDragging;
        private Vector3 _offset;
        private Rigidbody _rb;
        private Plane _dragPlane;

        protected bool CanDrag = true;

        private void Start()
        {
            _mainCamera = Camera.main;
            _rb = GetComponent<Rigidbody>();

            if (_mainCamera != null) _dragPlane = new Plane(-_mainCamera.transform.forward, transform.position);
        }

        protected virtual void OnMouseDown()
        {
            if (!CanDrag)
                return;
            
            _isDragging = true;
            _rb.isKinematic = true;

            _offset = transform.position - GetMouseWorldPosition();
        }

        protected virtual void OnMouseDrag()
        {
            if (!_isDragging || !CanDrag) return;

            var newPosition = GetMouseWorldPosition() + _offset;
            transform.position = newPosition;
        }

        protected virtual void OnMouseUp()
        {
            _isDragging = false;
            _rb.isKinematic = false;
        }

        private Vector3 GetMouseWorldPosition()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            return _dragPlane.Raycast(ray, out var distance) ? ray.GetPoint(distance) : transform.position;
        }
    }
}