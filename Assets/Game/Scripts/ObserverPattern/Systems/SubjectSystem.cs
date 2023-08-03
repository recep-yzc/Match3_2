using Game.Scripts.ObserverPattern.Interfaces;
using Game.Scripts.ObserverPattern.Managers;

namespace Game.Scripts.ObserverPattern.Systems
{
    public class SubjectSystem : ISubjectSystem
    {
        public void Publish(string managerEvent, params object[] args) => SubjectManager.Instance.NotifyObservers(managerEvent, args);
        public void Subscribe(string managerEvent, Observer observer) => SubjectManager.Instance.AddObserver(managerEvent, observer);
        public void Unsubscribe(string managerEvent, Observer observer) => SubjectManager.Instance.RemoveObserver(managerEvent, observer);
        public void SaveEvent(string managerEvent, bool status, Observer observer)
        {
            if (status)
            {
                Subscribe(managerEvent, observer);
            }
            else
            {
                Unsubscribe(managerEvent, observer);
            }
        }
    }
}