using UnityEngine;

namespace Game.Scripts.Cube.Interfaces
{
    public interface ICubeUi
    {
        #region Functions

        public void UpdateSpriteOrder(int value);
        public void UpdateSprite(int value);
        public void FetchUiProperties();
        
        public void SetSprite(Sprite value);
        public void SetScale(Vector3 value);
        public Vector2 GetScale();
        
        public void ShowHolder();
        public void HideHolder();

        #endregion
    }
}