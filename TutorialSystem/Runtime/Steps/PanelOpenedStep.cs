using System;
using TutorialSystem.Runtime.Abstracts;
using UISystem.Shared.Enums;
using UISystem.Shared.Events;
using UnityEngine;

namespace TutorialSystem.Runtime.Steps
{
    [Serializable]
    public class PanelOpenedStep : ATutorialStep
    {
        #region Serialized Fields

        [SerializeField]
        private PanelOpened panelOpened;
        [SerializeField]
        private PanelId panelId;

        #endregion

        #region Protected Methods

        protected override void AssignEvents()
        {
            panelOpened.Action += IsPanelOpened;
        }

        protected override void UnAssignEvents()
        {
            panelOpened.Action -= IsPanelOpened;
        }

        #endregion

        #region Private Methods

        private void IsPanelOpened(PanelId openedPanelId)
        {
            if (openedPanelId == panelId)
            {
                Finish();
            }
        }

        #endregion
    }
}
