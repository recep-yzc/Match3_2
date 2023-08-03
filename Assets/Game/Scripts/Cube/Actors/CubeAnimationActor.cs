using DG.Tweening;
using Game.Scripts.Cube.Interfaces;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.Cube.Actors
{
    public class CubeAnimationActor : Abstracts.Cube, ICubeAnimation
    {
        [Header("References")] [SerializeField]
        private Transform holder;

        #region Private Properties

        #region DropAnimProperties

        private Tween _dropMovementTween;
        private readonly float _dropMovementDuration = 0.1f;
        private readonly float _dropMovementScaleFactor = 0.075f;

        private Tween _clickShakeTween;
        private Tween _clickShakeTweenComplete;
        private readonly float _dropRotationDuration = 0.2f;
        private readonly float _dropRotationScaleFactor = 5f;

        #endregion

        #endregion

        #region Functions

        public void ClickAnim()
        {
            ShakeIt();
        }

        public void DropAnim()
        {
            DropIt();
        }

        public void ResetAllAnim()
        {
            ResetLocalPosition();
            ResetLocalRotation();
        }

        private void DropIt()
        {
            var targetY = Component.Ui.GetScale().y * _dropMovementScaleFactor;

            _dropMovementTween.Kill();
            _dropMovementTween = holder.DOLocalMoveY(targetY, _dropMovementDuration)
                .SetLink(gameObject)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.Unset);
        }

        private void ShakeIt()
        {
            var targetEulerAngle = new Vector3(0, 0, _dropRotationScaleFactor) * Utilities.Helper.GetRandomFactor();

            _clickShakeTween.Kill();
            _clickShakeTweenComplete.Kill();
            
            holder.localEulerAngles = -targetEulerAngle;

            _clickShakeTween = holder.DOLocalRotate(targetEulerAngle, _dropRotationDuration)
                .OnComplete(() => _clickShakeTweenComplete = holder.DOLocalRotate(Vector3.zero, _dropRotationDuration))
                .SetLink(gameObject)
                .SetEase(Ease.Unset);
        }

        private void ResetLocalPosition()
        {
            _dropMovementTween.Kill();
            holder.localPosition = Vector2.zero;
        }

        private void ResetLocalRotation()
        {
            _clickShakeTween.Kill();
            _clickShakeTweenComplete.Kill();
            
            holder.localRotation = quaternion.identity;
        }

        #endregion
    }
}