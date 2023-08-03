using System.Collections.Generic;
using Game.Scripts.Utilities;
using Game.Scripts.Utilities.ScriptRenderSystem;

namespace Game.Scripts.ObserverPattern.Managers
{
    public delegate void Observer(object[] args);

#if UNITY_EDITOR
    [ScriptOrder(-100)]
#endif
    public class SubjectManager : Singleton<SubjectManager>
    {
        private readonly Dictionary<string, HashSet<Observer>> _eventList = new();

        public void NotifyObservers(string eventName, params object[] parameters)
        {
            if (_eventList.TryGetValue(eventName, out var callbacks))
            {
                foreach (var callback in callbacks)
                {
                    callback?.Invoke(parameters);
                }
            }
        }

        public void AddObserver(string eventName, Observer observer)
        {
            if (!_eventList.TryGetValue(eventName, out var callbacks))
            {
                callbacks = new HashSet<Observer>();
                _eventList.Add(eventName, callbacks);
            }

            callbacks.Add(observer);
        }

        public void RemoveObserver(string eventName, Observer observer)
        {
            if (_eventList.TryGetValue(eventName, out var callbacks))
            {
                callbacks.Remove(observer);
                if (callbacks.Count == 0)
                {
                    _eventList.Remove(eventName);
                }
            }
        }
    }
}