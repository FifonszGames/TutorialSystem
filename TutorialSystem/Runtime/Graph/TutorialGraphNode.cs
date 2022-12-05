using TutorialSystem.Runtime.Helpers;
using UnityEngine;

namespace TutorialSystem.Runtime.Graph
{
    public class TutorialGraphNode : ABaseTutorialNode
    {
        #region Serialized Fields

        [SerializeField]
        private TutorialGraph tutorialGraph;

        #endregion

        #region Public Properties

        public override bool IsCompleted => tutorialGraph && tutorialGraph.IsCompleted;

        #endregion

        #region Public Methods

        public override void Begin()
        {
            base.Begin();
            tutorialGraph.OnCompleted += OnGraphCompleted;
            tutorialGraph.Begin();
        }

        public override void ResetState()
        {
            base.ResetState();
            tutorialGraph.ResetState();
        }

        public override bool IsNodeWithID(string id) => ID.Equals(id) || tutorialGraph.HasNodeWithID(id);

        public override bool HasNext()
        {
            if (TutorialHelpers.HasSavedData(out string lastId))
            {
                if (ID.Equals(lastId))
                {
                    return base.HasNext();
                }
            }

            return tutorialGraph.HasNext();
        }

        public override ABaseTutorialNode GetNext()
        {
            if (TutorialHelpers.HasSavedData(out string lastId))
            {
                if (ID.Equals(lastId))
                {
                    return base.GetNext();
                }
            }

            return HasNext() ? this : base.GetNext();
        }

        #endregion

        #region Protected Methods

        protected override void SkipStep()
        {
            tutorialGraph.SkipCurrentStep();
        }

        protected override void MarkAsCompleted()
        {
            tutorialGraph.MarkAsCompleted();
        }

        #endregion

        #region Private Methods

        private void OnGraphCompleted()
        {
            tutorialGraph.OnCompleted -= OnGraphCompleted;
            Finish();
        }

        #endregion
    }
}
