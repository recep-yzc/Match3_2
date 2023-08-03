using UnityEngine;

namespace Game.Scripts.Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}