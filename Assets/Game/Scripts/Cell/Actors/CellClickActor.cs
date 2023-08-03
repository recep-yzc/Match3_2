using Game.Scripts.ObserverPattern.Utilities;
using UnityEngine.EventSystems;

namespace Game.Scripts.Cell.Actors
{
    public class CellClickActor : Abstracts.Cell, IPointerClickHandler
    {
        #region Functions

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Component.Pool.IsActive) return;

            SubjectSystem.Publish(GameEvents.Cell.OnClicked, Component);
        }

        #endregion
    }
}