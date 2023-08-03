using System;
using System.Collections.Generic;
using Game.Scripts.Cell.Interfaces;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Cell.Utilities
{
    public static class Helper
    {
        public static readonly Vector2[] Directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        public static Vector2 CalculatePosition(int totalRow, int row, int totalColumn, int column, float scale)
        {
            var resultRow = CalculateGridPosition(totalRow, row, scale);
            var resultColumn = CalculateGridPosition(totalColumn, column, scale);

            return new(resultColumn, resultRow);
        }

        private static float CalculateGridPosition(int totalColumn, int column, float scale)
        {
            var centerOffsetColumn = (totalColumn * 0.5f) * scale;
            var halfScaleColumn = scale * 0.5f;
            var positionColumn = (column + 1) * scale;
            var resultColumn = positionColumn - centerOffsetColumn - halfScaleColumn;
            return resultColumn;
        }
        
        public static CellComponent GetComponent(GameObject cellGameObject)
        {
            CellComponent cellComponent = new()
            {
                gameObject = cellGameObject,
                Ui = cellGameObject.GetComponent<ICellUi>(),
                Property = cellGameObject.GetComponent<ICellProperty>(),
                Movement = cellGameObject.GetComponent<ICellMovement>(),
                Pool = cellGameObject.GetComponent<ICellPool>(),
            };

            return cellComponent;
        }
    }

    [Serializable]
    public class CellComponent
    {
        public GameObject gameObject;

        public ICellUi Ui;
        public ICellProperty Property;
        public ICellMovement Movement;
        public ICellPool Pool;
    }

    [Serializable]
    public class FloatCells : UnitySerializedDictionary<float, List<CellComponent>>
    {
    }
}