using Game.Scripts.Cell.Interfaces;
using Game.Scripts.Cube.Utilities;
using Game.Scripts.PoolSystem.Interfaces;
using UnityEngine;

namespace Game.Scripts.Cell.Actors
{
    public class CellPoolActor : Abstracts.Cell, ICellPool, IPoolItem
    {
        #region Public Properties

        public bool IsActive { get; private set; }
        public bool IsSpawnable { get; private set; } = true;
        
        #endregion

        #region Functions

        public void Active(object[] a)
        {
            IsSpawnable = false;
            IsActive = true;
            
            var position = (Vector2)a[0];
            var verticalIndex = (int)a[1];
            var scale = (float)a[2];
            
            Component.Property.SetVerticalAxisOrder(verticalIndex);
            Component.Movement.SetPosition(position);
            Component.Ui.SetScale(scale * Vector3.one);
            Component.Ui.ShowHolder();
        }

        public void Disable(object[] a)
        {
            Component.Ui.HideHolder();
            
            IsSpawnable = true;
            IsActive = false;
        }

        public void Shuffle(object[] a)
        {
            var cubeComponent = (CubeComponent)a[0];
            Component.Property.SetCubeComponent(cubeComponent);
        }

        #endregion
    }
}