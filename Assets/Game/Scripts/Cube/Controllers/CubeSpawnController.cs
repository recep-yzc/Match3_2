using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Cell.Controllers;
using Game.Scripts.Cell.Utilities;
using Game.Scripts.Cube.Actors;
using Game.Scripts.Cube.Enums;
using Game.Scripts.Cube.Interfaces;
using Game.Scripts.Cube.Scriptable;
using Game.Scripts.Cube.Utilities;
using Game.Scripts.ObserverPattern.Interfaces;
using Game.Scripts.ObserverPattern.Systems;
using Game.Scripts.ObserverPattern.Utilities;
using Game.Scripts.PoolSystem.Managers;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Cube.Controllers
{
    public class CubeSpawnController : Singleton<CubeSpawnController>
    {
        [Header("Data")] public CubeDataSo cubeDataSo;

        #region Private Properties

        private readonly ISubjectSystem _subjectSystem = new SubjectSystem();
        private readonly List<CubeComponent> _cubeComponents = new();

        private CellSpawnController _cellSpawnController;

        #endregion

        #region Action

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
            _subjectSystem.SaveEvent(GameEvents.Cube.OnCubeCreate, status, OnCreate);
            _subjectSystem.SaveEvent(GameEvents.Cube.OnCubeCreateAfter, status, OnCreateAfter);
        }

        #endregion

        #region Functions

        private void Awake()
        {
            _cellSpawnController = CellSpawnController.Instance;
        }

        private void OnCreate(object[] a)
        {
            var cellComponents = _cellSpawnController.GetCellComponents();

            foreach (var cellComponent in cellComponents)
            {
                var itemType = GetRandomCubeType();

                var cubePool = PoolManager.Instance.GetPoolItem<ICubePool>(typeof(CubePoolActor));
                cubePool.Active(cubeDataSo, itemType, cellComponent);

                _cubeComponents.Add(cubePool.GetComponent());
            }
        }

        private void OnCreateAfter(object[] a)
        {
            var cellComponents = (List<CellComponent>)a[0];

            foreach (var cellComponent in cellComponents)
            {
                var itemType = GetRandomCubeType();

                var cellPosition = cellComponent.Movement.GetPosition();
                var scale = cellComponent.Ui.GetScale();
                var spawnPosition = new Vector2(cellPosition.x, cellPosition.y + (cellComponents.Count * scale.y));

                var cubePool = PoolManager.Instance.GetPoolItem<ICubePool>(typeof(CubePoolActor));
                cubePool.ActiveWithFall(cubeDataSo, itemType, cellComponent, spawnPosition);

                _cubeComponents.Add(cubePool.GetComponent());
            }
        }

        private CubeType GetRandomCubeType()
        {
            var cubeType = cubeDataSo.cubeUiPropertyByType.OrderBy(x => Guid.NewGuid()).First();
            return cubeType.Key;
        }

        public List<CubeComponent> GetCubeComponents() => _cubeComponents;

        #endregion
    }
}