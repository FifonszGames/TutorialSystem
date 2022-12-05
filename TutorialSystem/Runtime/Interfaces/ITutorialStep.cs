using Common.Runtime.Interfaces;

namespace TutorialSystem.Runtime.Interfaces
{
    public interface ITutorialStep : IBeginable, IResettable
    {
        #region Public Properties

        public bool IsSkipping { get; }

        #endregion

        #region Public Methods

        public void MarkAsCompleted();
        public void Skip();

        #endregion
    }
}
