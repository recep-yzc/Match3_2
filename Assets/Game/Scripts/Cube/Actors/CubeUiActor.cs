using Game.Scripts.Cube.Interfaces;
using Game.Scripts.Cube.Utilities;
using UnityEngine;

namespace Game.Scripts.Cube.Actors
{
    public class CubeUiActor : Abstracts.Cube, ICubeUi
    {
        [Header("References")] [SerializeField]
        private GameObject objModel;

        [SerializeField] private SpriteRenderer imgCube;

        #region Private Properties

        private Transform _transform;
        private UiProperties _uiProperties;

        #endregion

        #region Functions

        protected override void FetchComponents()
        {
            base.FetchComponents();
            _transform = transform;
        }

        public void UpdateSprite(int value)
        {
            imgCube.sprite = Helper.GetSpriteByMergeCount(_uiProperties, value);
        }

        public void FetchUiProperties()
        {
            _uiProperties = Component.Property.DataSo.cubeUiPropertyByType[Component.Property.Type];
        }

        public void SetSprite(Sprite value) => imgCube.sprite = value;
        public void SetScale(Vector3 value) => _transform.localScale = value;
        public Vector2 GetScale() => _transform.localScale;
        public void UpdateSpriteOrder(int value) => imgCube.sortingOrder = value;
        public void ShowHolder() => objModel.SetActive(true);
        public void HideHolder() => objModel.SetActive(false);

        #endregion
    }
}