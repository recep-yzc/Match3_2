using Game.Scripts.Cube.Utilities;

namespace Game.Scripts.Cube.Interfaces
{
    public interface ICubePool
    {
        #region Properties

        public bool IsActive { get; }

        #endregion

        #region Functions

        public void Active(params object[] args);
        public void ActiveWithFall(params object[] args);
        public void Disable(params object[] args);
        public void Shuffle(params object[] args);
        public void Slide(params object[] args);

        public CubeComponent GetComponent();

        #endregion
    }
}