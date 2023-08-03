using UnityEngine;

namespace Game.Scripts.Cube.Interfaces
{
    public interface ICubeMovement
    {
        #region Properties

        public bool IsMoving { get; }

        #endregion
        
        #region Functions

        public void Fall(Vector2 value);
        public void SetPosition(Vector2 value);
        public void Shuffle(Vector2 value);

        #endregion
    }
}