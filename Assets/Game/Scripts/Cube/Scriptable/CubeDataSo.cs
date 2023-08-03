using Game.Scripts.Cube.Utilities;
using UnityEngine;

namespace Game.Scripts.Cube.Scriptable
{
    [CreateAssetMenu(fileName = "CubeItemDataSo", menuName = "Data/CubeDataSo")]
    public class CubeDataSo : ScriptableObject
    {
        [Header("Properties")]
        public float fallSpeed = 5f;
        public AnimationCurve fallEase;
        public float shuffleSpeed = 5f;
        public AnimationCurve shuffleEase;
        

        [Space(5)]
        public Helper.CubeUiPropertyByType cubeUiPropertyByType;
    }
}
