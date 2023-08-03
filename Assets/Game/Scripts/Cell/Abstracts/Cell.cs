using Game.Scripts.Cell.Utilities;
using Game.Scripts.ObserverPattern.Interfaces;
using Game.Scripts.ObserverPattern.Systems;
using UnityEngine;

namespace Game.Scripts.Cell.Abstracts
{
    public abstract class Cell : MonoBehaviour
    {
        #region Public Properties

        protected CellComponent Component { get; private set; }
        protected readonly ISubjectSystem SubjectSystem = new SubjectSystem();

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

        public CellComponent GetComponent() => Component;

        #endregion
    }
}