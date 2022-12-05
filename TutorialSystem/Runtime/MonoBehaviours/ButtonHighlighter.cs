using Common.Runtime.Interfaces;
using TutorialSystem.Runtime.Abstracts;
using TutorialSystem.Runtime.Conditions;
using UISystem.Shared.Events;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialSystem.Runtime.MonoBehaviours
{
    [RequireComponent(typeof(Button))]
    public class ButtonHighlighter : ASelectableHighlighter<Button>
    {
        #region Serialized Fields

        [SerializeField]
        protected SelectableClicked selectableClicked;

        #endregion

        #region Private Fields

        private static readonly ICondition AbortCondition = new TutorialFinishedCondition();

        #endregion

        #region Protected Methods

        protected override void AssignEvents()
        {
            base.AssignEvents();
            element.onClick.AddListener(ButtonClicked);
        }

        protected override void UnAssignEvents()
        {
            base.UnAssignEvents();
            element.onClick.AddListener(ButtonClicked);
        }

        #endregion

        #region Private Methods

        private void ButtonClicked()
        {
            if (AbortCondition.IsMet)
            {
                return;
            }
            
            element.image.color = element.colors.normalColor;
            element.interactable = false;
            selectableClicked.Invoke(selectableID, element);
        }

        #endregion
    }
}
