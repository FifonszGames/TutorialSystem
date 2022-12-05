using Common.Runtime.Interfaces;

namespace TutorialSystem.Runtime.Interfaces
{
    public interface ITutorialTask : IBeginable, IResettable
    {
        #region Public Properties

        public string Name { get; }

        #endregion

        #region Public Methods

        public void MarkAsCompleted();
        public void SkipCurrentStep();

        #endregion
    }
}
