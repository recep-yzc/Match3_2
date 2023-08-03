using System.Collections.Generic;
using Game.Scripts.Cube.Utilities;
using Game.Scripts.ObserverPattern.Interfaces;
using Game.Scripts.ObserverPattern.Systems;
using Game.Scripts.ObserverPattern.Utilities;
using Game.Scripts.PoolSystem.Managers;
using Game.Scripts.Utilities;

namespace Game.Scripts.Cube.Controllers
{
    public class CubeController : Singleton<CubeController>
    {
        #region private Properties

        private readonly ISubjectSystem _subjectSystem = new SubjectSystem();

        private readonly List<CubeComponent> _cubeComponents = new();

        private CubeSpawnController _cubeSpawnController;
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
            _subjectSystem.SaveEvent(GameEvents.Cube.OnCubeControllerInit, status, OnCubeControllerInit);
        }

        #endregion

        private void Awake()
        {
            _cubeSpawnController = CubeSpawnController.Instance;
            _poolManager = PoolManager.Instance;
        }
        
        private void OnCubeControllerInit(object[] a)
        {
            _cubeComponents.AddRange(_cubeSpawnController.GetCubeComponents());
        }
    }
}
