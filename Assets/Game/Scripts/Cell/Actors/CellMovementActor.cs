using Game.Scripts.Cell.Interfaces;
using UnityEngine;

namespace Game.Scripts.Cell.Actors
{
    public class CellMovementActor : Abstracts.Cell, ICellMovement
    {
        #region Private Properties

        private Transform _transform;

        #endregion
        
        #region Functions

        protected override void FetchComponents()
        {
            base.FetchComponents();
            _transform = transform;
        }

        public void SetPosition(Vector2 value) => _transform.position = value;
        public Vector2 GetPosition() => _transform.position;

        #endregion
    }
}