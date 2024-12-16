using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class BackpackContentView : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;

        private Vector3 _initialCanvasScale;

        private void Awake()
        {
            canvas.SetActive(false);
            _initialCanvasScale = canvas.transform.localScale;
        }

        private void OnMouseDown()
        {
            UnShrinkAndActivate();
        }

        private void OnMouseUp()
        {
            ShrinkAndDeactivate();
        }
        private void UnShrinkAndActivate()
        {
            var rectTransform = canvas.transform;
    
            rectTransform.localScale = Vector3.zero;
    
            canvas.SetActive(true);
    
            var growSequence = DOTween.Sequence();
    
            growSequence.Append(
                rectTransform.DOScale(_initialCanvasScale, 0.3f)
                    .SetEase(Ease.OutBack)
            );
        }
        private void ShrinkAndDeactivate()
        {
            var rectTransform = canvas.transform;
            var shrinkSequence = DOTween.Sequence();
            
            shrinkSequence.Append(
                rectTransform.DOScale(Vector3.zero, 0.2f)
                    .SetEase(Ease.InBack)
            );

            shrinkSequence.OnComplete(() => 
            {
                canvas.SetActive(false);
                rectTransform.localScale = _initialCanvasScale;
            });
        }
    }
}
