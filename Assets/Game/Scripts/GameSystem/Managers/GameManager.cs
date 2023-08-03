using System.Collections;
using Game.Scripts.ObserverPattern.Interfaces;
using Game.Scripts.ObserverPattern.Systems;
using Game.Scripts.ObserverPattern.Utilities;
using UnityEngine;

namespace Game.Scripts.GameSystem.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Private Property

        private readonly ISubjectSystem _subjectSystem = new SubjectSystem();

        #endregion
        
        private IEnumerator Start()
        {
            _subjectSystem.Publish(GameEvents.Pool.OnPoolCreate);

            yield return null;
            
            _subjectSystem.Publish(GameEvents.Cell.OnCellCreate);
            _subjectSystem.Publish(GameEvents.Cube.OnCubeCreate);
            
            _subjectSystem.Publish(GameEvents.Cell.OnCellControllerInit);
            _subjectSystem.Publish(GameEvents.Cube.OnCubeControllerInit);

        }
    }
}
