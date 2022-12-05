using System.Collections.Generic;
using System.Linq;
using Common.Runtime.Abstracts;
using Common.Runtime.Helpers.Utils;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using TutorialSystem.Runtime.Helpers;
using TutorialSystem.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialSystem.Runtime
{
    public class SelectablesTracker : SerializedMonoBehaviour
    {
        #region Serialized Fields

        [SerializeField]
        private GameEvent togglerOfEvent;
        [SerializeField]
        private GameEvent togglerOnEvent;

        [SerializeField, ReadOnly]
        private List<Selectable> selectables;
        [OdinSerialize, ReadOnly]
        private List<IUIHighlighter> uiHighlighters;
        
        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            if (TutorialHelpers.TutorialFinished())
            {
                Destroy(gameObject);
            }
            else if (selectables.IsNullOrEmpty() || uiHighlighters.IsNullOrEmpty())
            {
                PopulateLists();
            }
        }

        private void OnEnable()
        {
            AssignEvents();
        }

        private void OnDisable()
        {
            UnAssignEvents();
        }

        #endregion

        #region Private Methods

        private void AssignEvents()
        {
            togglerOfEvent.Action += ManageToggleOffEvent;
            togglerOnEvent.Action += ManageToggleOnEvent;
        }

        private void UnAssignEvents()
        {
            togglerOfEvent.Action -= ManageToggleOffEvent;
            togglerOnEvent.Action -= ManageToggleOnEvent;
        }

        private void ManageToggleOnEvent()
        {
            ManageSelectables(true);
            uiHighlighters.ForEach(uiHighlighter => uiHighlighter.ToggleElement(true));
        }

        private void ManageToggleOffEvent()
        {
            ManageSelectables(false);
            uiHighlighters.ForEach(uiHighlighter => uiHighlighter.CheckCondition());
        }

        private void ManageSelectables(bool toggle)
        {
            ClearNulls();
            ToggleSelectables(toggle);
        }

        private void ClearNulls()
        {
            if (selectables.Any(selectable => selectable == null))
            {
                selectables.RemoveAll(selectable => selectable == null);
            }
        }

        private void ToggleSelectables(bool toggle)
        {
            selectables.ForEach(selectable => selectable.interactable = toggle);
        }

        [Button]
        private void PopulateLists()
        {
            selectables = GameObjectUtils.FindAllOfType<Selectable>().ToList();
            uiHighlighters = GameObjectUtils.FindAllOfType<IUIHighlighter>().ToList();
        }

        #endregion
    }
}
