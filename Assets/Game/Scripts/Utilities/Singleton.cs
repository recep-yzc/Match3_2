using UnityEngine;

namespace Game.Scripts.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly object _lock = new();
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = FindObjectOfType<T>();

                            if (_instance == null)
                            {
                                var singletonObject = new GameObject(typeof(T).Name);
                                _instance = singletonObject.AddComponent<T>();
                            }
                        }
                    }
                }

                return _instance;
            }
        }
    }
}