using Game.Scripts.Cell.Utilities;
using Game.Scripts.Cube.Enums;
using Game.Scripts.Cube.Interfaces;
using Game.Scripts.Cube.Scriptable;

namespace Game.Scripts.Cube.Actors
{
    public class CubePropertyActor : Abstracts.Cube, ICubeProperty
    {
        #region Public Properties

        public CubeType Type { get; private set; }
        public CubeDataSo DataSo { get; private set; }
        public CellComponent CellComponent { get; private set; }

        #endregion

        #region Functions

        public void SetData(CubeDataSo value) => DataSo = value;
        public void SetType(CubeType value) => Type = value;
        public void SetCellComponent(CellComponent value) => CellComponent = value;
        public void RemoveCellComponent() => CellComponent = null;

        #endregion
    }
}