using System;
using Common.Runtime.Interfaces;
using UnityEngine;

namespace TutorialSystem.Runtime.Graph
{
    public abstract class ABaseTutorialNode : ABaseNode<ABaseTutorialNode>, IResettable, IBeginable
    {
        #region Events

        public event Action OnCompleted;

        #endregion

        #region Public Properties

        public abstract bool IsCompleted { get; }
        public bool InProgress { get; private set; }

        #endregion

        #region Public Methods

        public virtual void Begin()
        {
            InProgress = true;
        }

        public virtual void ResetState()
        {
            InProgress = false;
        }

        public void SkipStep(bool requirePreviousCompletion)
        {
            if (requirePreviousCompletion && !CanSkip())
            {
                Debug.LogWarning($"tried to skip {name}, but previous is not completed, abandoning!");

                return;
            }

            SkipStep();
        }

        public void MarkPreviousAsCompleted(bool markSelf)
        {
            if (markSelf)
            {
                MarkAsCompleted();
            }

            if (!HasPrevious())
            {
                return;
            }

            GetPrevious().MarkPreviousAsCompleted(true);
        }

        public virtual bool IsNodeWithID(string id) => ID.Equals(id);

        #endregion

        #region Protected Methods

        protected abstract void SkipStep();
        protected abstract void MarkAsCompleted();

        protected void Finish()
        {
            InProgress = false;
            OnCompleted?.Invoke();
        }

        #endregion

        #region Private Methods

        private bool CanSkip() => !HasPrevious() || GetPrevious().IsCompleted;

        #endregion
    }
}
