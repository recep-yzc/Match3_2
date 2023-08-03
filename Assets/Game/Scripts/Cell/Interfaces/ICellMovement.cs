using UnityEngine;

namespace Game.Scripts.Cell.Interfaces
{
    public interface ICellMovement
    {
        #region Functions

        public void SetPosition(Vector2 value);
        public Vector2 GetPosition();

        #endregion
    }
}