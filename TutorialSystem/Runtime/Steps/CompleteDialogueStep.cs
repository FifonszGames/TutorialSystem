using System;
using DialogueSystem.Runtime.ScriptableObjects;
using Sirenix.OdinInspector;
using TutorialSystem.Runtime.Abstracts;
using UISystem.Shared.Data;
using UISystem.Shared.Enums;
using UISystem.Shared.Events;
using UnityEngine;

namespace TutorialSystem.Runtime.Steps
{
    [Serializable]
    public class CompleteDialogueStep : ATutorialStep
    {
        #region Serialized Fields

        [SerializeField, Required]
        private PanelClosed panelClosed;
        [SerializeField, Required]
        private OpenPanel openPanel;
        [SerializeField, Required]
        private DisableCurrentPanel disableCurrentPanel;
        [SerializeField, Required]
        private DialogueGraph dialogueGraph;

        #endregion

        private PanelId PanelId => dialogueGraph.IsMonologue() ? PanelId.MonologuePanel : PanelId.DialoguePanel;

        #region Public Methods

        public override void Begin()
        {
            base.Begin();
            openPanel.Invoke(new OpenPanelInfo {PanelId = PanelId, PanelData = dialogueGraph});
        }

        public override void Skip()
        {
            IsSkipping = true;
            disableCurrentPanel.Invoke();
        }

        #endregion

        #region Protected Methods

        protected override void AssignEvents() => panelClosed.Action += PanelClosed;

        protected override void UnAssignEvents() => panelClosed.Action -= PanelClosed;

        #endregion

        #region Private Methods

        private void PanelClosed(PanelId panelId)
        {
            if (panelId != PanelId)
            {
                return;
            }

            Finish();
        }

        #endregion
    }
}
