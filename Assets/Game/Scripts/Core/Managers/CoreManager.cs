using Game.Scripts.Utilities;
using Game.Scripts.Utilities.ScriptRenderSystem;
using UnityEngine;

namespace Game.Scripts.Core.Managers
{
    
#if UNITY_EDITOR
    [ScriptOrder(-9999)]
#endif
    
    public class CoreManager : Singleton<CoreManager>
    {
        private void OnEnable()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
        }

        private void OnDisable()
        {
            
        }
    }
}
