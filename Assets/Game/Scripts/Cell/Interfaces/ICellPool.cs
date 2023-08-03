namespace Game.Scripts.Cell.Interfaces
{
    public interface ICellPool
    {
        #region Properties

        public bool IsActive { get; }

        #endregion

        #region Functions
        
        public void Active(params object[] value); 
        public void Disable(params object[] value);
        public void Shuffle(params object[] value);

        #endregion
    }
}