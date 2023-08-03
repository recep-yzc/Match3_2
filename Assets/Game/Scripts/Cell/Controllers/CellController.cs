using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Cell.Scriptable;
using Game.Scripts.Cell.Utilities;
using Game.Scripts.ObserverPattern.Interfaces;
using Game.Scripts.ObserverPattern.Systems;
using Game.Scripts.ObserverPattern.Utilities;
using Game.Scripts.Particle.Cube.Actors;
using Game.Scripts.Particle.Cube.Interfaces;
using Game.Scripts.PoolSystem.Managers;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Cell.Controllers
{
    public class CellController : Singleton<CellController>
    {
        [Header("Data")] public CellDataSo cellDataSo;

        #region private Properties

        private readonly ISubjectSystem _subjectSystem = new SubjectSystem();

        private readonly List<CellComponent> _cellComponents = new();
        private readonly Dictionary<Vector2, CellComponent> _cellComponentWithPositions = new();

        private readonly FloatCells _cellComponentByVertical = new();
        private readonly FloatCells _cellComponentByHorizontal = new();

        private readonly List<CellComponent> _lastBlastCellComponents = new();
        private readonly List<CellComponent> _lastSlideCellComponents = new();

        private CellSpawnController _cellSpawnController;
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
            _subjectSystem.SaveEvent(GameEvents.Cell.OnClicked, status, OnClicked);
            _subjectSystem.SaveEvent(GameEvents.Cell.OnCellControllerInit, status, OnCellControllerInit);
            _subjectSystem.SaveEvent(GameEvents.Cube.OnFallCompleted, status, OnFallCompleted);
        }

        #endregion

        #region Functions

        private void Awake()
        {
            _cellSpawnController = CellSpawnController.Instance;
            _poolManager = PoolManager.Instance;
        }

        private void OnCellControllerInit(object[] a)
        {
            _cellComponents.AddRange(_cellSpawnController.GetCellComponents());

            SaveCellComponentWithPositions();
            FetchCellComponentByAxis(SnapAxis.X);
            FetchCellComponentByAxis(SnapAxis.Y);
            FetchNeighborCellComponents();

            _cellComponents.ForEach(UpdateCubeSprite);

            CheckAnyClickable();
        }

        private void OnFallCompleted(object[] a)
        {
            var cellComponent = (CellComponent)a[0];
            UpdateCubeSprite(cellComponent);

            SpecificUpdateCubeSprite();

            CheckAnyClickable();
        }

        private void OnClicked(object[] a)
        {
            var cellComponent = (CellComponent)a[0];

            if (cellComponent.Property.IsEmpty) return;
            if (cellComponent.Property.CubeComponent.Movement.IsMoving) return;

            var foundCellComponents = GetSameTypeCellComponents(cellComponent);

            var canBlast = true;
            foreach (var foundCellComponent in foundCellComponents)
            {
                if (foundCellComponents.Count < cellDataSo.minDestroyCount)
                {
                    foundCellComponent.Property.CubeComponent.Animation.ClickAnim();
                    canBlast = false;
                }
            }

            if (!canBlast) return;

            BlastCellComponent(foundCellComponents);
            SlideCellComponent();

            CreateCubeComponent();
        }

        private void CheckAnyClickable()
        {
            foreach (var cellComponent in _cellComponents)
            {
                var foundCellComponents = GetSameTypeCellComponents(cellComponent);
                if (foundCellComponents.Count >= cellDataSo.minDestroyCount)
                {
                    return;
                }
            }

            ShuffleCellComponent();
            CheckAnyClickable();
        }

        private void ShuffleCellComponent()
        {
            var cubeComponents = _cellComponents
                .OrderBy(x => Guid.NewGuid() /*x => x.Property.CubeComponent.Property.Type*/)
                .Select(x => x.Property.CubeComponent).ToList();
            
            var cellComponents = _cellComponents;

            for (var i = 0; i < cellComponents.Count; i++)
            {
                var cubeComponent = cubeComponents[i];
                var cellComponent = cellComponents[i];

                cellComponent.Pool.Shuffle(cubeComponent);
                cubeComponent.Pool.Shuffle(cellComponent);
            }

            foreach (var cellComponent in cellComponents)
            {
                UpdateCubeSprite(cellComponent);
            }
        }

        private List<CellComponent> GetSameTypeCellComponents(CellComponent cellComponent)
        {
            var foundCellComponents = new List<CellComponent>();
            FindOtherSameTypeCellComponent(ref foundCellComponents, cellComponent);
            return foundCellComponents;
        }

        private void SaveCellComponentWithPositions()
        {
            foreach (var cellComponent in _cellComponents)
            {
                var position = cellComponent.Movement.GetPosition();

                _cellComponentWithPositions.Add(position, cellComponent);
            }
        }

        private void FetchCellComponentByAxis(SnapAxis snapAxis)
        {
            foreach (var cellComponent in _cellComponents)
            {
                var position = cellComponent.Movement.GetPosition();
                var key = snapAxis == SnapAxis.X ? position.x : position.y;

                if (snapAxis == SnapAxis.X)
                {
                    if (_cellComponentByVertical.ContainsKey(key))
                    {
                        _cellComponentByVertical[key].Add(cellComponent);
                    }
                    else
                    {
                        _cellComponentByVertical[key] = new List<CellComponent> { cellComponent };
                    }
                }
                else if (snapAxis == SnapAxis.Y)
                {
                    if (_cellComponentByHorizontal.ContainsKey(key))
                    {
                        _cellComponentByHorizontal[key].Add(cellComponent);
                    }
                    else
                    {
                        _cellComponentByHorizontal[key] = new List<CellComponent> { cellComponent };
                    }
                }
            }
        }

        private void FetchNeighborCellComponents()
        {
            foreach (var cellComponent in _cellComponents)
            {
                var position = cellComponent.Movement.GetPosition();
                var otherCellComponents = new List<CellComponent>();

                foreach (var direction in Helper.Directions)
                {
                    var key = position + (direction * cellDataSo.scale);

                    if (_cellComponentWithPositions.TryGetValue(key, out var neighborCellComponent) &&
                        neighborCellComponent != cellComponent)
                    {
                        otherCellComponents.Add(neighborCellComponent);
                    }
                }

                cellComponent.Property.SetNeighborCellComponents(otherCellComponents);
            }
        }

        private void FindOtherSameTypeCellComponent(ref List<CellComponent> foundCellComponents,
            CellComponent cellComponent)
        {
            if (foundCellComponents.Contains(cellComponent))
                return;

            var cubeType = cellComponent.Property.CubeComponent.Property.Type;
            foundCellComponents.Add(cellComponent);

            foreach (var otherCellComponent in cellComponent.Property.NeighborCellComponents)
            {
                if (otherCellComponent.Property.IsEmpty) continue;

                var otherCubeType = otherCellComponent.Property.CubeComponent.Property.Type;

                if (otherCubeType.Equals(cubeType))
                {
                    FindOtherSameTypeCellComponent(ref foundCellComponents, otherCellComponent);
                }
            }
        }

        private void BlastCellComponent(List<CellComponent> cellComponents)
        {
            if (cellComponents.Count < cellDataSo.minDestroyCount) return;

            List<CellComponent> lastBlastCellComponents = new();

            foreach (var cellComponent in cellComponents)
            {
                if (cellComponent.Property.IsEmpty) continue;

                var cubeComponent = cellComponent.Property.CubeComponent;
                cubeComponent.Pool.Disable(cellComponent);

                lastBlastCellComponents.Add(cellComponent);

                #region Particle

                var blastParticle = _poolManager.GetPoolItem<IBlastPool>(typeof(BlastParticleActor));
                blastParticle.Active(cubeComponent);

                #endregion
            }

            SaveLastBlastCellComponents(lastBlastCellComponents);
        }

        private void SlideCellComponent()
        {
            List<CellComponent> slideCellComponents = new();

            foreach (var cellComponentByVertical in _cellComponentByVertical)
            {
                var cellComponents = cellComponentByVertical.Value;

                if (!IsExistsEmpty(cellComponents)) continue;

                for (var i = 1; i < cellComponents.Count; i++)
                {
                    for (var j = 1; j < cellComponents.Count; j++)
                    {
                        var currentCellComponent = cellComponents[j - 1];
                        var nextCellComponent = cellComponents[j];

                        if (!currentCellComponent.Property.IsEmpty || nextCellComponent.Property.IsEmpty) continue;

                        var cubeComponent = nextCellComponent.Property.CubeComponent;
                        cubeComponent.Pool.Slide(currentCellComponent, nextCellComponent);

                        slideCellComponents.Add(nextCellComponent);
                    }
                }
            }

            SaveLastSlideCellComponents(slideCellComponents);
        }

        private void CreateCubeComponent()
        {
            foreach (var cellComponentByVertical in _cellComponentByVertical)
            {
                var cellComponents = cellComponentByVertical.Value.Where(x => x.Property.IsEmpty).ToList();

                if (cellComponents.Count <= 0) continue;
                _subjectSystem.Publish(GameEvents.Cube.OnCubeCreateAfter, cellComponents);
            }
        }

        private void SpecificUpdateCubeSprite()
        {
            List<CellComponent> cellComponents = new();
            cellComponents.AddRange(_lastBlastCellComponents);
            cellComponents.AddRange(_lastSlideCellComponents);

            foreach (var cellComponent in cellComponents)
            {
                foreach (var neighborCellComponent in cellComponent.Property.NeighborCellComponents)
                {
                    UpdateCubeSprite(neighborCellComponent);
                }
            }

            _lastBlastCellComponents.Clear();
            _lastSlideCellComponents.Clear();
        }

        private void UpdateCubeSprite(CellComponent cellComponent)
        {
            var foundCellComponents = GetSameTypeCellComponents(cellComponent);
            var count = foundCellComponents.Count;

            foreach (var foundCellComponent in foundCellComponents)
            {
                if (foundCellComponent.Property.CubeComponent.Movement.IsMoving)
                {
                    --count;
                }
            }

            foreach (var foundCellComponent in foundCellComponents)
            {
                foundCellComponent.Property.CubeComponent.Ui.UpdateSprite(count);
            }
        }

        private void SaveLastBlastCellComponents(List<CellComponent> cellComponents)
        {
            _lastBlastCellComponents.AddRange(cellComponents);
        }

        private void SaveLastSlideCellComponents(List<CellComponent> cellComponents)
        {
            _lastSlideCellComponents.AddRange(cellComponents);
        }

        private bool IsExistsEmpty(List<CellComponent> cellComponents)
        {
            return cellComponents.Any(cellComps => cellComps.Property.IsEmpty);
        }

        #endregion
    }
}