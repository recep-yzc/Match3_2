using DG.Tweening;
using Game.Scripts.Cube.Interfaces;
using Game.Scripts.ObserverPattern.Utilities;
using UnityEngine;

namespace Game.Scripts.Cube.Actors
{
    public class CubeMovementActor : Abstracts.Cube, ICubeMovement
    {
        #region Public Properties

        public bool IsMoving { get; private set; }

        #endregion

        #region Private Properties

        private Transform _transform;
        private Tween _moveTween;

        #endregion


        #region Functions

        protected override void FetchComponents()
        {
            base.FetchComponents();
            _transform = transform;
        }

        public void SetPosition(Vector2 value) => _transform.position = value;

        #region Shuffle

        public void Shuffle(Vector2 value)
        {
            _moveTween.Kill();
            _moveTween = _transform.DOMove(value, Component.Property.DataSo.shuffleSpeed)
                .SetSpeedBased()
                .SetEase(Component.Property.DataSo.shuffleEase)
                .SetLink(gameObject)
                .OnStart(OnShuffleStart)
                .OnComplete(OnShuffleComplete);
        }

        private void OnShuffleStart()
        {
            Component.Animation.ResetAllAnim();
            IsMoving = true;
        }

        private void OnShuffleComplete()
        {
            IsMoving = false;
            SubjectSystem.Publish(GameEvents.Cube.OnFallCompleted, Component.Property.CellComponent);
        }

        #endregion

        #region Fall

        public void Fall(Vector2 value)
        {
            _moveTween.Kill();
            _moveTween = _transform.DOMove(value, Component.Property.DataSo.fallSpeed)
                .SetSpeedBased()
                .SetEase(Component.Property.DataSo.fallEase)
                .SetLink(gameObject)
                .OnStart(OnFallStart)
                .OnComplete(OnFallComplete);
        }

        private void OnFallStart()
        {
            Component.Animation.ResetAllAnim();
            IsMoving = true;
        }

        private void OnFallComplete()
        {
            Component.Animation.DropAnim();
            IsMoving = false;

            SubjectSystem.Publish(GameEvents.Cube.OnFallCompleted, Component.Property.CellComponent);
        }

        #endregion

        #endregion
    }
}