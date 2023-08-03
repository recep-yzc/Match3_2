using Game.Scripts.Cell.Utilities;
using Game.Scripts.Cube.Enums;
using Game.Scripts.Cube.Interfaces;
using Game.Scripts.Cube.Scriptable;
using Game.Scripts.Cube.Utilities;
using Game.Scripts.PoolSystem.Interfaces;
using UnityEngine;

namespace Game.Scripts.Cube.Actors
{
    public class CubePoolActor : Abstracts.Cube, ICubePool, IPoolItem
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

            var cubeDataSo = (CubeDataSo)a[0];
            var cubeType = (CubeType)a[1];
            var cellComponent = (CellComponent)a[2];

            Component.Property.SetData(cubeDataSo);
            Component.Property.SetType(cubeType);
            Component.Property.SetCellComponent(cellComponent);
            Component.Movement.SetPosition(cellComponent.Movement.GetPosition());

            Component.Ui.FetchUiProperties();
            Component.Ui.UpdateSprite(0);
            Component.Ui.SetScale(cellComponent.Ui.GetScale());
            Component.Ui.ShowHolder();
            Component.Ui.UpdateSpriteOrder(cellComponent.Property.VerticalAxisOrder);

            cellComponent.Property.SetCubeComponent(Component);
        }

        public void ActiveWithFall(object[] a)
        {
            IsSpawnable = false;
            IsActive = true;

            var cubeDataSo = (CubeDataSo)a[0];
            var cubeType = (CubeType)a[1];
            var cellComponent = (CellComponent)a[2];
            var spawnPosition = (Vector2)a[3];

            Component.Property.SetData(cubeDataSo);
            Component.Property.SetType(cubeType);
            Component.Property.SetCellComponent(cellComponent);
            Component.Movement.SetPosition(spawnPosition);

            Component.Ui.FetchUiProperties();
            Component.Ui.UpdateSprite(0);
            Component.Ui.SetScale(cellComponent.Ui.GetScale());
            Component.Ui.ShowHolder();
            Component.Ui.UpdateSpriteOrder(cellComponent.Property.VerticalAxisOrder);

            Component.Movement.Fall(cellComponent.Movement.GetPosition());

            cellComponent.Property.SetCubeComponent(Component);
        }

        public void Disable(object[] a)
        {
            var cellComponent = (CellComponent)a[0];

            Component.Ui.HideHolder();
            Component.Property.RemoveCellComponent();

            cellComponent.Property.RemoveCubeComponent();

            IsSpawnable = true;
            IsActive = false;
        }

        public void Shuffle(object[] a)
        {
            var cellComponent = (CellComponent)a[0];

            Component.Property.SetCellComponent(cellComponent);
            Component.Ui.UpdateSpriteOrder(cellComponent.Property.VerticalAxisOrder);
            Component.Movement.Shuffle(cellComponent.Movement.GetPosition());
        }

        public void Slide(params object[] a)
        {
            var currentCellComponent = (CellComponent)a[0];
            var nextCellComponent = (CellComponent)a[1];

            currentCellComponent.Property.SetCubeComponent(Component);

            Component.Movement.Fall(currentCellComponent.Movement.GetPosition());
            Component.Ui.UpdateSpriteOrder(currentCellComponent.Property.VerticalAxisOrder);
            Component.Property.SetCellComponent(currentCellComponent);

            nextCellComponent.Property.RemoveCubeComponent();
        }

        public CubeComponent GetComponent() => Component;

        #endregion
    }
}