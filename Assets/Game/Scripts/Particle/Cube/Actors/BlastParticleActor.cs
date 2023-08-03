using Game.Scripts.Cube.Utilities;
using Game.Scripts.Particle.Cube.Interfaces;
using Game.Scripts.PoolSystem.Interfaces;
using UnityEngine;

namespace Game.Scripts.Particle.Cube.Actors
{
    public class BlastParticleActor : MonoBehaviour, IBlastPool, IPoolItem
    {
        [Header("References")] [SerializeField]
        private ParticleSystem vfxBlast;

        #region Public Properties

        public bool IsActive { get; private set; }
        public bool IsSpawnable { get; private set; } = true;

        #endregion

        #region Private Properties

        private Transform _transform;
        private ParticleSystemRenderer _particleSystemRenderer;

        #endregion

        #region Functions

        private void Awake()
        {
            _transform = transform;
            _particleSystemRenderer = vfxBlast.GetComponent<ParticleSystemRenderer>();
        }

        public void Active(object[] a)
        {
            IsActive = true;
            IsSpawnable = false;

            var cubeComponent = (CubeComponent)a[0];

            _particleSystemRenderer.material = cubeComponent.Property.DataSo
                .cubeUiPropertyByType[cubeComponent.Property.Type].blastParticleMaterial;
            _transform.position = cubeComponent.gameObject.transform.position;

            vfxBlast.Play();
        }

        public void Disable(object[] a)
        {
            IsActive = false;
            IsSpawnable = true;
        }

        public void OnParticleEnd()
        {
            Disable(null);
        }

        #endregion
    }
}