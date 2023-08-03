using UnityEngine;

namespace Game.Scripts.Cell.Scriptable
{
    [CreateAssetMenu(fileName = "CellData", menuName = "Data/CellDataSo")]
    public class CellDataSo : ScriptableObject
    {
        [Header("References")] [Range(2, 10)] public int rows = 6;
        [Range(2, 10)] public int columns = 6;
        
        public int minDestroyCount = 2;

        [Space(5)] public float scale = 1;
    }
}