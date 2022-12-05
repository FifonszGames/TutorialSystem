using System;
using Sirenix.OdinInspector;
using TutorialSystem.Runtime.Abstracts;
using UISystem.Shared.Enums;
using UISystem.Shared.Events;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialSystem.Runtime.Steps
{
    [Serializable]
    public class ClickOnButtonStep : ATutorialStep
    {
        #region Serialized Fields

        [SerializeField, Required]
        private HighlightSelectable highlightSelectable;
        [SerializeField, Required]
        private SelectableClicked selectableClicked;
        [SerializeField, Required]
        private SelectableID selectableToClick;

        #endregion

        #region Public Methods

        public override void Begin()
        {
            base.Begin();
            InvokeHighlighting(true);
        }

        public override void Skip()
        {
            InvokeHighlighting(false);
            base.Skip();
        }

        public override void ResetState()
        {
            InvokeHighlighting(false);
            base.ResetState();
        }

        #endregion

        #region Protected Methods

        protected override void AssignEvents() => selectableClicked.Action += SelectableClicked;
        protected override void UnAssignEvents() => selectableClicked.Action -= SelectableClicked;

        #endregion

        #region Private Methods

        private void SelectableClicked(SelectableID selectableID, Selectable selectable)
        {
            if (selectableID != selectableToClick)
            {
                return;
            }

            Finish();
        }

        private void InvokeHighlighting(bool toggle) => highlightSelectable.Invoke(selectableToClick, toggle);

        #endregion
    }
}
