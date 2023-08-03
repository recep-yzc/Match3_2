using System.Collections.Generic;
using Game.Scripts.Cell.Utilities;
using Game.Scripts.Cube.Utilities;

namespace Game.Scripts.Cell.Interfaces
{
    public interface ICellProperty
    {
        #region Properties
        
        public bool IsEmpty { get; }
        public int VerticalAxisOrder { get; }
        public int HorizontalAxisOrder { get; }
        public CubeComponent CubeComponent { get; }
        public List<CellComponent> NeighborCellComponents { get; }

        #endregion

        #region Functions

        public void SetNeighborCellComponents(List<CellComponent> value);
        public void SetVerticalAxisOrder(int value);
        public void SetHorizontalAxisOrder(int value);
        public void SetCubeComponent(CubeComponent value);
        public void RemoveCubeComponent();
        
        #endregion
    }
}