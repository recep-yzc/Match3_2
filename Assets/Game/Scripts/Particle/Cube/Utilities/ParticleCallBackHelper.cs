using Game.Scripts.Particle.Cube.Interfaces;
using UnityEngine;

namespace Game.Scripts.Particle.Cube.Utilities
{
    public class ParticleCallBackHelper : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GameObject particleParent;

        #region Private Properties

        private IBlastPool _blastPool;

        #endregion

        #region Functions
        
        private void Awake()
        {
            _blastPool = particleParent.GetComponent<IBlastPool>();
        }

        private void OnParticleSystemStopped()
        {
            _blastPool?.OnParticleEnd();
        }
        
        #endregion
    }
}
