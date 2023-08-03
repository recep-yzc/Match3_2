using Game.Scripts.Cell.Interfaces;
using UnityEngine;

namespace Game.Scripts.Cell.Actors
{
    public class CellUiActor : Abstracts.Cell, ICellUi
    {
        [Header("References")] [SerializeField]
        private GameObject objModel;

        [SerializeField] private SpriteRenderer imgCell;

        #region Private Properties

        private Transform _transform;

        #endregion

        #region Functions
        
        protected override void FetchComponents()
        {
            base.FetchComponents();
            _transform = transform;
        }
        public void SetSprite(Sprite value) => imgCell.sprite = value;
        public void SetScale(Vector3 value) => _transform.localScale = value;
        public Vector3 GetScale() => _transform.localScale;
        public void ShowHolder() => objModel.SetActive(true);
        public void HideHolder() => objModel.SetActive(false);

        #endregion
    }
}