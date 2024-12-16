using UnityEngine;

namespace _Project.Scripts.UI
{
    public class BackpackContentView : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        private void Start()
        {
            canvas.SetActive(false);
        }

        private void OnMouseDown()
        {
            canvas.SetActive(true);
        }

        private void OnMouseUp()
        {
            canvas.SetActive(false);
        }
    }
}
