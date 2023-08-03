using Game.Scripts.ObserverPattern.Managers;

namespace Game.Scripts.ObserverPattern.Interfaces
{
    public interface ISubjectSystem
    {
        public void Publish(string managerEvent, params object[] args);
        public void Subscribe(string managerEvent, Observer observer);
        public void Unsubscribe(string managerEvent, Observer observer);
        public void SaveEvent(string managerEvent, bool status, Observer observer);
    }
}