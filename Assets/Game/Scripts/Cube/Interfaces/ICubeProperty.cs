using Game.Scripts.Cell.Utilities;
using Game.Scripts.Cube.Enums;
using Game.Scripts.Cube.Scriptable;

namespace Game.Scripts.Cube.Interfaces
{
    public interface ICubeProperty
    {
        #region Properties

        public CubeType Type { get; }
        public CubeDataSo DataSo { get; }
        public CellComponent CellComponent { get; }

        #endregion

        #region Functions

        public void SetData(CubeDataSo value);
        public void SetType(CubeType value);
        public void SetCellComponent(CellComponent value);
        public void RemoveCellComponent();

        #endregion
    }
}