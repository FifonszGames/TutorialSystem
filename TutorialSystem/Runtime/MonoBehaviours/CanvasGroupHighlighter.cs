using System;
using DG.Tweening;
using TutorialSystem.Runtime.Abstracts;
using UISystem.Core.Helpers;
using UnityEngine;

namespace TutorialSystem.Runtime.MonoBehaviours
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupHighlighter : AUiHighlighter<CanvasGroup>
    {
        protected override bool IsToggled => element.IsToggled();

        #region Public Methods

        public override void ToggleElement(bool toggle, Action<bool> onFinish = null)
        {
            element.SimpleFade(toggle).OnComplete(() => onFinish?.Invoke(toggle));
        }

        #endregion

        #region Protected Methods

        protected override void ToggleOffInstant()
        {
            element.alpha = 0;
            element.ChangeCanvasGroupInteractable(false);
        }

        #endregion
    }
}
