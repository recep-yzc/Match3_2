using System;
using System.Linq;
using Game.Scripts.Cube.Enums;
using Game.Scripts.Cube.Interfaces;
using Game.Scripts.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Cube.Utilities
{
    public static class Helper
    {
        [Serializable]
        public class CubeUiPropertyByType : UnitySerializedDictionary<CubeType, UiProperties>
        {
        }

        public static CubeComponent GetComponent(GameObject gem)
        {
            CubeComponent cubeComponent = new()
            {
                gameObject = gem,
                Ui = gem.GetComponent<ICubeUi>(),
                Property = gem.GetComponent<ICubeProperty>(),
                Movement = gem.GetComponent<ICubeMovement>(),
                Animation = gem.GetComponent<ICubeAnimation>(),
                Pool = gem.GetComponent<ICubePool>(),
            };

            return cubeComponent;
        }

        public static int GetRandomFactor() => Random.Range(0, 2) == 0 ? -1 : 1;

        public static Sprite GetSpriteByMergeCount(UiProperties uiProperties, int mergeCount)
        {
            return uiProperties.spriteLevelProperties.First(x => x.minMergeCount <= mergeCount).sprite;
        }
    }

    [Serializable]
    public class CubeComponent
    {
        public GameObject gameObject;

        public ICubeUi Ui;
        public ICubeProperty Property;
        public ICubeMovement Movement;
        public ICubeAnimation Animation;
        public ICubePool Pool;
    }

    [Serializable]
    public class SpriteLevelProperty
    {
        public int minMergeCount;
        public Sprite sprite;
    }

    [Serializable]
    public class UiProperties
    {
        public SpriteLevelProperty[] spriteLevelProperties;
        public Material blastParticleMaterial;
    }
}