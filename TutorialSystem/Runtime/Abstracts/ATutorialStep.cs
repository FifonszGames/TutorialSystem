using System;
using Sirenix.OdinInspector;
using TutorialSystem.Runtime.Interfaces;

namespace TutorialSystem.Runtime.Abstracts
{
    [Serializable]
    public abstract class ATutorialStep : ITutorialStep
    {
        #region Events

        public event Action OnCompleted;

        #endregion

        #region Public Properties

        [ShowInInspector, ReadOnly]
        public bool IsCompleted { get; protected set; }
        [ShowInInspector, ReadOnly]
        public bool IsSkipping { get; protected set; }

        #endregion

        #region Public Methods

        public virtual void Begin()
        {
            IsSkipping = false;
            IsCompleted = false;
            AssignEvents();
        }

        public virtual void ResetState()
        {
            IsSkipping = false;
            UnAssignEvents();
            IsCompleted = false;
        }

        public void MarkAsCompleted()
        {
            IsSkipping = false;
            IsCompleted = true;
        }

        public virtual void Skip()
        {
            Finish();
        }

        #endregion

        #region Protected Methods

        protected virtual void AssignEvents()
        {
        }

        protected virtual void UnAssignEvents()
        {
        }

        protected virtual void Finish()
        {
            IsSkipping = false;
            UnAssignEvents();
            IsCompleted = true;
            OnCompleted?.Invoke();
        }

        #endregion
    }
}
