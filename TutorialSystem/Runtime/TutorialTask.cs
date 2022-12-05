using System;
using System.Collections.Generic;
using Common.Runtime.Helpers.Extensions;
using Common.Runtime.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TutorialSystem.Runtime.Interfaces;
using UnityEngine;

namespace TutorialSystem.Runtime
{
    [CreateAssetMenu(fileName = nameof(TutorialTask), menuName = "SO/" + nameof(TutorialSystem) + "/" + nameof(TutorialTask))]
    public class TutorialTask : SerializedScriptableObject, ITutorialTask
    {
        #region Events

        public event Action OnCompleted;

        #endregion

        #region Serialized Fields

        [SerializeField, Required("Quest needs to have steps")]
        private List<ITutorialStep> steps;
        [SerializeField, Tooltip("When all are met, quest finishes instantly")]
        private List<ICondition> endConditions;

        #endregion

        #region Private Fields

        private ITutorialStep currentStep;
        private int currentStepIndex;

        #endregion

        #region Public Properties

        [field: SerializeField, Required("Quest needs to have a name"), PropertyOrder(-1)]
        public string Name { get; private set; }

        public bool IsCompleted => !steps.IsNullOrEmpty() && steps.AreCompleted();

        #endregion

        #region Public Methods

        public void ResetState()
        {
            steps.ForEach(step => step.ResetState());
            currentStepIndex = 0;

            if (currentStep != null)
            {
                currentStep.OnCompleted -= StepCompleted;
                currentStep = null;
            }
        }

        public void Begin()
        {
            if (steps.IsNullOrEmpty())
            {
                Debug.LogWarning($"List of steps is empty in {name}, finishing early");
                Finish();

                return;
            }

            if (EndConditionsAreMet())
            {
                MarkAsCompleted();

                return;
            }

            currentStepIndex = 0;
            StartNextStep();
        }

        public void MarkAsCompleted()
        {
            if (currentStep != null)
            {
                currentStep.OnCompleted -= StepCompleted;
                currentStep.Skip();
            }

            steps.ForEach(step => step.MarkAsCompleted());
            Finish();
        }
        
        public void SkipCurrentStep()
        {
            if (currentStep == null)
            {
                return;
            }
            
            if (currentStep.IsSkipping)
            {
                Debug.LogWarning("Tried to skip during skipping phase, abandoning");

                return;
            }

            currentStep?.Skip();
        }

        #endregion

        #region Private Methods

        private bool EndConditionsAreMet() => !endConditions.IsNullOrEmpty() && endConditions.AreMet();

        private void StartNextStep()
        {
            currentStep = steps[currentStepIndex];
            currentStep.OnCompleted += StepCompleted;
            currentStep.Begin();
        }

        private void StepCompleted()
        {
            currentStep.OnCompleted -= StepCompleted;

            if (!HasNextStep())
            {
                Finish();
            }
            else
            {
                currentStepIndex++;
                StartNextStep();
            }
        }

        private bool HasNextStep() => currentStepIndex + 1 < steps.Count;

        private void Finish()
        {
            OnCompleted?.Invoke();
        }

        #endregion
    }
}
