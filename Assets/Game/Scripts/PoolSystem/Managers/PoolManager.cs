using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.ObserverPattern.Interfaces;
using Game.Scripts.ObserverPattern.Systems;
using Game.Scripts.ObserverPattern.Utilities;
using Game.Scripts.PoolSystem.Interfaces;
using Game.Scripts.PoolSystem.Utilities;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.PoolSystem.Managers
{
    public class PoolManager : Singleton<PoolManager>
    {
        [Header("Pool List")] [SerializeField] private List<PoolItemProperty> creatablePoolItems = new();

        #region Private Properties

        private readonly Dictionary<Type, HashSet<IPoolItem>> _poolItemList = new();
        private readonly ISubjectSystem _subjectSystem = new SubjectSystem();

        #endregion

        #region Action

        private void OnEnable()
        {
            Listen(true);
        }

        private void OnDisable()
        {
            Listen(false);
        }

        private void Listen(bool status)
        {
            _subjectSystem.SaveEvent(GameEvents.Pool.OnPoolCreate, status, OnCreate);
        }

        #endregion

        private void OnCreate(object[] value)
        {
            creatablePoolItems.ForEach(CreatePoolItems);
        }

        public T GetPoolItem<T>(Type poolItemType)
        {
            IPoolItem iPoolItemResult = null;

            if (_poolItemList.ContainsKey(poolItemType))
            {
                iPoolItemResult = _poolItemList[poolItemType].FirstOrDefault(x => x.IsSpawnable);

                if (iPoolItemResult == null)
                {
                    iPoolItemResult = CreatePoolItem(poolItemType);
                    Add(poolItemType, iPoolItemResult);
                }
            }

            return (T)iPoolItemResult;
        }

        private void Add<T>(Type poolItemType, T poolItem)
        {
            HashSet<IPoolItem> newPoolItems;
            if (_poolItemList.ContainsKey(poolItemType))
            {
                newPoolItems = _poolItemList[poolItemType];
                newPoolItems.Add((IPoolItem)poolItem);
            }
            else
            {
                newPoolItems = new HashSet<IPoolItem> { (IPoolItem)poolItem };
                _poolItemList.Add(poolItemType, newPoolItems);
            }
        }

        private IPoolItem CreatePoolItem(Type poolItemType)
        {
            var creatablePoolItemPropertyTemp =
                creatablePoolItems.FirstOrDefault(x => x.GetPoolItemType() == poolItemType);

            if (creatablePoolItemPropertyTemp == null)
            {
                Debug.LogError("Type is not correct!");
                return null;
            }

            var prefab = creatablePoolItemPropertyTemp.prefab;
            var parent = creatablePoolItemPropertyTemp.parent;

            return PoolItemInstantiate(prefab, parent);
        }

        private void CreatePoolItems(PoolItemProperty poolItemProperty)
        {
            var prefab = poolItemProperty.prefab;
            var parent = poolItemProperty.parent;
            var spawnCount = poolItemProperty.spawnCount;
            var poolItemType = poolItemProperty.GetPoolItemType();

            for (var i = 0; i < spawnCount; i++)
            {
                var iPoolItemTemp = PoolItemInstantiate(prefab, parent);
                Add(poolItemType, iPoolItemTemp);
            }
        }

        private IPoolItem PoolItemInstantiate(GameObject prefab, Transform parent)
        {
            return Instantiate(prefab, parent).GetComponent<IPoolItem>();
        }
    }
}