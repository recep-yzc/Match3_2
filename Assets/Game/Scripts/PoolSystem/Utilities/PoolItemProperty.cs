using System;
using Game.Scripts.PoolSystem.Interfaces;
using UnityEngine;

namespace Game.Scripts.PoolSystem.Utilities
{
    [Serializable]
    public class PoolItemProperty
    {
        [Space(5)] public GameObject prefab;
        [Space(5)] public int spawnCount;
        [Space(5)] public Transform parent;

        public Type GetPoolItemType()
        {
            var poolItemType = prefab.GetComponent<IPoolItem>()?.GetType();

            if (poolItemType == null)
                Debug.LogError("Prefab has not IPoolItem");

            return poolItemType;
        }
    }
}