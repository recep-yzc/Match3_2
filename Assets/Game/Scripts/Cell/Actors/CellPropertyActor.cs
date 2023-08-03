using System.Collections.Generic;
using Game.Scripts.Cell.Interfaces;
using Game.Scripts.Cell.Utilities;
using Game.Scripts.Cube.Utilities;

namespace Game.Scripts.Cell.Actors
{
    public class CellPropertyActor : Abstracts.Cell, ICellProperty
    {
        #region Public Properties
        
        public bool IsEmpty => CubeComponent == null;
        public int VerticalAxisOrder { get; private set; }
        public int HorizontalAxisOrder { get; private set; }
        public CubeComponent CubeComponent { get; private set; }
        public List<CellComponent> NeighborCellComponents { get; private set; }
        
        #endregion

        #region Functions

        public void SetNeighborCellComponents(List<CellComponent> value) => NeighborCellComponents = value;
        public void SetVerticalAxisOrder(int value) => VerticalAxisOrder = value;
        public void SetHorizontalAxisOrder(int value) => HorizontalAxisOrder = value;
        public void SetCubeComponent(CubeComponent value) => CubeComponent = value;
        public void RemoveCubeComponent() => CubeComponent = null;

        #endregion
    }
}