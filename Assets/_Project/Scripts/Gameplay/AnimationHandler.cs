using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public static class AnimationHandler
    {
        public static void SmoothSetToPosition(Transform current, Transform target, float duration)
        {
            current.SetParent(target);
            current.DOLocalMove(Vector3.zero, duration);
        }

        public static void SmoothScale(Transform current, Transform target, float duration)
        {
            var currentSize = current.GetComponent<Renderer>().bounds.size;
            var targetSize = target.GetComponent<Renderer>().bounds.size;

            var scaleFactorX = targetSize.x / currentSize.x;
            var scaleFactorY = targetSize.y / currentSize.y;
            var scaleFactorZ = targetSize.z / currentSize.z;

            var scaleFactor = Mathf.Min(scaleFactorX, scaleFactorY, scaleFactorZ);

            current.DOScale(current.localScale * scaleFactor * 2f, duration);
        }

        public static void SmoothSetToPositionAndScale(Transform current, Transform target, float duration)
        {
            SmoothSetToPosition(current, target, duration);
            SmoothScale(current, target, duration);
        }
    }
}
