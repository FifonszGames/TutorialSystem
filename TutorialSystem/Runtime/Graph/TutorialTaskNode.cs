using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TutorialSystem.Runtime.Graph
{
    [Serializable, CreateNodeMenu(nameof(TutorialTaskNode)), NodeWidth(300)]
    public class TutorialTaskNode : ABaseTutorialNode
    {
        #region Serialized Fields

        [SerializeField, Required, InlineEditor]
        private TutorialTask tutorialTask;

        #endregion

        #region Public Properties

        public override bool IsCompleted => tutorialTask && tutorialTask.IsCompleted;

        #endregion

        #region Public Methods

        public override void Begin()
        {
            base.Begin();
            tutorialTask.OnCompleted += TutorialTaskCompleted;
            tutorialTask.Begin();
        }

        public override void ResetState()
        {
            base.ResetState();
            tutorialTask.ResetState();
        }

        #endregion

        #region Protected Methods

        protected override void SkipStep()
        {
            tutorialTask.SkipCurrentStep();
        }

        protected override void MarkAsCompleted()
        {
            tutorialTask.MarkAsCompleted();
        }

        #endregion

        #region Private Methods

        private void TutorialTaskCompleted()
        {
            tutorialTask.OnCompleted -= TutorialTaskCompleted;
            Finish();
        }

        #endregion
    }
}
