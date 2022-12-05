using System;
using Common.Runtime.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TutorialSystem.Runtime.Interfaces;
using UISystem.Shared.Enums;
using UISystem.Shared.Events;
using UnityEngine;

namespace TutorialSystem.Runtime.Abstracts
{
    [DisallowMultipleComponent]
    public abstract class AUiHighlighter<T> : SerializedMonoBehaviour, IUIHighlighter where T : Behaviour
    {
        #region Serialized Fields

        [SerializeField, Header("Higlighting")]
        private SelectableHighlighted selectableHighlighted;
        [SerializeField]
        private HighlightSelectable highlightSelectable;
        [SerializeField, Range(0.1f, 2f)]
        protected float highlightDuration = 1f;
        [SerializeField]
        protected SelectableID selectableID;
        [OdinSerialize, Tooltip("Under what condition should this element toggle off")]
        private ICondition toggleOffCondition;


        #endregion

        #region Private Fields

        protected T element;
        protected abstract bool IsToggled { get;}

        #endregion

        #region Unity Callbacks

        protected virtual void Awake()
        {
            element = GetComponent<T>();
        }

        private void Start()
        {
            CheckCondition();
        }

        protected virtual void OnEnable()
        {
            AssignEvents();
        }

        protected virtual void OnDisable()
        {
            UnAssignEvents();
        }

        #endregion

        #region Public Methods

        public void CheckCondition()
        {
            if (toggleOffCondition == null || !toggleOffCondition.IsMet)
            {
                return;
            }

            ToggleOffInstant();
        }

        public abstract void ToggleElement(bool toggle, Action<bool> onFinish = null);

        #endregion

        #region Protected Methods

        protected abstract void ToggleOffInstant();

        protected virtual void AssignEvents()
        {
            highlightSelectable.Action += HighlightSelectableInvoked;
        }

        protected virtual void UnAssignEvents()
        {
            highlightSelectable.Action -= HighlightSelectableInvoked;
        }

        #endregion

        #region Private Methods

        private void HighlightingFinished(bool toggle)
        {
            selectableHighlighted.Invoke(selectableID, toggle);
        }

        private void HighlightSelectableInvoked(SelectableID selectableID, bool toggle)
        {
            if (selectableID != this.selectableID)
            {
                return;
            }

            if (toggle && IsToggled || !toggle && !IsToggled)
            {
                return;
            }
            
            ToggleElement(toggle, HighlightingFinished);
        }

        #endregion
    }
}
