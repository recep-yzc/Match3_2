using System.Collections.Generic;
using Game.Scripts.Cell.Actors;
using Game.Scripts.Cell.Interfaces;
using Game.Scripts.Cell.Scriptable;
using Game.Scripts.Cell.Utilities;
using Game.Scripts.ObserverPattern.Interfaces;
using Game.Scripts.ObserverPattern.Systems;
using Game.Scripts.ObserverPattern.Utilities;
using Game.Scripts.PoolSystem.Managers;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Cell.Controllers
{
    public class CellSpawnController : Singleton<CellSpawnController>
    {
        [Header("Data")] public CellDataSo cellData;

        #region Private Properties

        private readonly ISubjectSystem _subjectSystem = new SubjectSystem();
        private readonly List<CellComponent> _createdAllCellComponents = new();
        private PoolManager _poolManager;

        #endregion

        #region Event

        private void OnEnable()
        {
            Listen(true);
        }

        private void OnDisable()
        {
            Listen(false);
        }

        private void Listen(bool status)
        {
            _subjectSystem.SaveEvent(GameEvents.Cell.OnCellCreate, status, OnCellCreate);
        }

        #endregion

        #region Functions
        
        private void Awake()
        {
            _poolManager = PoolManager.Instance;
        }

        private void OnCellCreate(object[] a)
        {
            var columns = cellData.columns;
            var rows = cellData.rows;
            var scale = cellData.scale;

            for (var column = 0; column < columns; column++)
            {
                for (var row = 0; row < rows; row++)
                {
                    var spawnPosition = Helper.CalculatePosition(rows, row, columns, column, scale);
                    
                    var cellPool = _poolManager.GetPoolItem<ICellPool>(typeof(CellPoolActor));
                    cellPool.Active(spawnPosition, row, scale);

                    _createdAllCellComponents.Add(((CellPoolActor)cellPool).GetComponent());
                }
            }
        }

        public List<CellComponent> GetCellComponents() => _createdAllCellComponents;
        
        #endregion
    }
}