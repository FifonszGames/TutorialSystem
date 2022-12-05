using System;
using DG.Tweening;
using TutorialSystem.Runtime.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialSystem.Runtime.Abstracts
{
    [RequireComponent(typeof(Selectable))]
    public abstract class ASelectableHighlighter<T> : AUiHighlighter<T> where T : Selectable
    {
        #region Private Fields

        private Color originalImageColor;

        #endregion

        protected override bool IsToggled => gameObject.activeSelf && element.interactable;

        #region Unity Callbacks

        protected override void Awake()
        {
            if (TutorialHelpers.TutorialFinished())
            {
                Destroy(this);

                return;
            }

            base.Awake();
        }

        protected override void OnEnable()
        {
            if (!element)
            {
                return;
            }

            base.OnEnable();
        }

        private void Start()
        {
            originalImageColor = element.image.color;
        }

        protected override void OnDisable()
        {
            if (!element)
            {
                return;
            }

            base.OnDisable();
        }

        #endregion

        #region Public Methods

        public override void ToggleElement(bool toggle, Action<bool> onFinish = null)
        {
            element.image.color = toggle ? element.colors.highlightedColor : originalImageColor;

            if (toggle)
            {
                element.gameObject.SetActive(true);
            }
            else
            {
                element.interactable = false;
            }

            element.image.DOFade(toggle ? 1 : 0, highlightDuration).OnComplete(() => AlphaFinishAction(toggle, onFinish));
        }

        #endregion

        #region Protected Methods

        protected override void ToggleOffInstant()
        {
            element.interactable = false;
            element.image.color = originalImageColor;
            element.image.DOFade(0, 0);
        }

        #endregion

        #region Private Methods

        private void AlphaFinishAction(bool toggle, Action<bool> onFinish = null)
        {
            element.interactable = toggle;
            onFinish?.Invoke(toggle);
        }

        #endregion
    }
}
