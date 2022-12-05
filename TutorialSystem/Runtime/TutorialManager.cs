using TutorialSystem.Runtime.Events;
using TutorialSystem.Runtime.Graph;
using TutorialSystem.Runtime.Helpers;
using UISystem.Shared.Enums;
using UISystem.Shared.Events;
using UnityEngine;

namespace TutorialSystem.Runtime
{
    [DisallowMultipleComponent]
    public class TutorialManager : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField]
        private TutorialGraph tutorialGraph;
        [SerializeField]
        private PanelOpened panelOpened;
        [SerializeField]
        private TutorialStarted tutorialStarted;
        [SerializeField]
        private TutorialFinished tutorialFinished;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            if (TutorialHelpers.TutorialFinished())
            {
                tutorialGraph.MarkAllAsCompleted();
            }
        }

        private void OnEnable()
        {
            panelOpened.Action += PanelOpened;
        }

        private void OnDisable()
        {
            panelOpened.Action -= PanelOpened;
        }

        private void OnDestroy()
        {
            tutorialGraph.OnCompleted -= GraphCompleted;
            tutorialGraph.ResetState();
        }

        #endregion

        #region Private Methods

        private void PanelOpened(PanelId panelId)
        {
            if (panelId != PanelId.TreeOfLifePanel)
            {
                return;
            }

            if (TutorialHelpers.TutorialFinished())
            {
                return;
            }

            StartTutorialGraph();
        }

        private void StartTutorialGraph()
        {
            if (tutorialGraph.IsCompleted)
            {
                GraphCompleted();

                return;
            }

            StartGraph();
        }

        private void StartGraph()
        {
            tutorialGraph.AssignEvents();
            tutorialGraph.OnCompleted += GraphCompleted;
            tutorialGraph.Begin();
            tutorialStarted.Invoke();
        }

        private void GraphCompleted()
        {
            tutorialGraph.UnAssignEvents();
            tutorialGraph.OnCompleted -= GraphCompleted;
            PlayerPrefs.SetInt(TutorialHelpers.TUTORIAL_FINISHED_KEY, 1);
            tutorialFinished.Invoke();
        }

        #endregion
    }
}
