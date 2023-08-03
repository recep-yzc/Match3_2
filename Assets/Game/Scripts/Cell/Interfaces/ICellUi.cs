using UnityEngine;

namespace Game.Scripts.Cell.Interfaces
{
    public interface ICellUi 
    {
        #region Functions

        public void SetSprite(Sprite value);
        public void SetScale(Vector3 value);
        public Vector3 GetScale();
        public void ShowHolder();
        public void HideHolder();

        #endregion
      
    }
}
