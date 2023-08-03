namespace Game.Scripts.Particle.Cube.Interfaces
{
    public interface IBlastPool
    {
        #region Properties

        public bool IsActive { get; }

        #endregion

        #region Functions

        public void Active(params object[] value);
        public void Disable(params object[] value);

        public void OnParticleEnd();

        #endregion
    }
}
