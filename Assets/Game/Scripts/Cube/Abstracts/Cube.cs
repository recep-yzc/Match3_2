using Game.Scripts.Cube.Utilities;
using Game.Scripts.ObserverPattern.Interfaces;
using Game.Scripts.ObserverPattern.Systems;
using UnityEngine;

namespace Game.Scripts.Cube.Abstracts
{
    public abstract class Cube : MonoBehaviour
    {
        #region Public Properties

        protected readonly ISubjectSystem SubjectSystem = new SubjectSystem();
        protected CubeComponent Component { get; private set; }

        #endregion

        #region Functions

        private void Awake()
        {
            Component = Helper.GetComponent(gameObject);

            FetchComponents();
        }

        protected virtual void FetchComponents()
        {
        }

        protected virtual void CustomDestroy()
        {
        }

        #endregion
    }
}