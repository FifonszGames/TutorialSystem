using System;
using System.Collections.Generic;
using System.Linq;
using Common.Runtime.Helpers.Extensions;
using Common.Runtime.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TutorialSystem.Runtime.Events;
using TutorialSystem.Runtime.Helpers;
using UnityEngine;
using XNode;

namespace TutorialSystem.Runtime.Graph
{
    [CreateAssetMenu(fileName = nameof(TutorialGraph), menuName = "SO/" + nameof(TutorialSystem) + "/" + nameof(TutorialGraph))]
    public class TutorialGraph : NodeGraph, IBeginable, IResettable
    {
        #region Events

        public event Action OnCompleted;

        #endregion

        #region Serialized Fields

        [SerializeField, ReadOnly, Header("Quest Nodes")]
        private List<ABaseTutorialNode> tutorialNodes;
        [SerializeField]
        private ABaseTutorialNode firstNode;
        [Header("Events"), SerializeField]
        private SkipTutorial skipTutorial;
        [SerializeField]
        private SkipCurrentStep skipCurrentStep;

        #endregion

        #region Private Fields

        private ABaseTutorialNode currentNode;

        #endregion

        #region Public Properties

        public bool IsCompleted => tutorialNodes.AreCompleted();

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            if (tutorialNodes.IsNullOrEmpty())
            {
                tutorialNodes = new List<ABaseTutorialNode>();
            }
        }

        #endregion

        #region Public Methods

        public void AssignEvents()
        {
            skipCurrentStep.Action += SkipCurrentStep;
            skipTutorial.Action += SkipTutorial;
        }

        public void UnAssignEvents()
        {
            skipCurrentStep.Action -= SkipCurrentStep;
            skipTutorial.Action -= SkipTutorial;
        }

        public void ResetState()
        {
            if (currentNode)
            {
                currentNode.OnCompleted -= OnNodeCompleted;
            }

            tutorialNodes.ForEach(node => node.ResetState());
        }

        public void Begin()
        {
            if (TutorialHelpers.HasSavedData(out string lastCompletedNode))
            {
                StartFromNodeId(lastCompletedNode);

                return;
            }

            StartFromFirstNode();
        }

        public override Node AddNode(Type type)
        {
            Node node = base.AddNode(type);

            if (node is ABaseTutorialNode questNode)
            {
                tutorialNodes.Add(questNode);
            }

            return node;
        }

        public override void RemoveNode(Node node)
        {
            base.RemoveNode(node);

            if (node is ABaseTutorialNode questNode)
            {
                tutorialNodes.Remove(questNode);
            }
        }

        public void FindFirstNode()
        {
            if (firstNode)
            {
                return;
            }

            firstNode = tutorialNodes.FirstOrDefault(node => !node.HasPrevious());
        }

        public void SkipCurrentStep()
        {
            if (!currentNode)
            {
                return;
            }

            currentNode.SkipStep(true);
        }

        public void MarkAllAsCompleted()
        {
            ABaseTutorialNode lastNode = tutorialNodes.Find(node => !node.HasNext());

            if (lastNode)
            {
                lastNode.MarkPreviousAsCompleted(true);
            }
        }

        public void MarkAsCompleted()
        {
            ABaseTutorialNode node = tutorialNodes.FirstOrDefault(node => !node.HasNext());

            if (node)
            {
                node.MarkPreviousAsCompleted(true);
            }
        }

        public bool HasNext()
        {
            if (TutorialHelpers.HasSavedData(out string lastNodeId))
            {
                ABaseTutorialNode node = FindFirstWithId(lastNodeId);

                return node ? node.HasNext() : firstNode.HasNext();
            }

            return firstNode.HasNext();
        }

        public bool HasNodeWithID(string id) => tutorialNodes.Any(node => node.IsNodeWithID(id));

        #endregion

        #region Private Methods

        private void SkipTutorial()
        {
            if (currentNode)
            {
                currentNode.OnCompleted -= OnNodeCompleted;
            }

            MarkAllAsCompleted();
            Finish();
        }

        private void StartFromFirstNode()
        {
            currentNode = firstNode;
            BeginCurrentNode();
        }

        private void StartFromNodeId(string lastCompletedNode)
        {
            ABaseTutorialNode lastCompletedTaskNode = FindFirstWithId(lastCompletedNode);

            if (!lastCompletedTaskNode)
            {
                StartFromFirstNode();

                return;
            }

            currentNode = lastCompletedTaskNode.GetNext();
            currentNode.MarkPreviousAsCompleted(false);
            BeginCurrentNode();
        }

        private void BeginCurrentNode()
        {
            currentNode.OnCompleted += OnNodeCompleted;
            currentNode.Begin();
        }

        private void OnNodeCompleted()
        {
            SaveData(currentNode);
            currentNode.OnCompleted -= OnNodeCompleted;
            currentNode = currentNode.GetNext();

            if (currentNode == null)
            {
                Finish();
            }
            else
            {
                BeginCurrentNode();
            }
        }

        private ABaseTutorialNode FindFirstWithId(string lastCompletedNodeId) => tutorialNodes.FirstOrDefault(node => node.IsNodeWithID(lastCompletedNodeId));
        private void SaveData(ABaseTutorialNode tutorialTaskNode) => PlayerPrefs.SetString(TutorialHelpers.LAST_COMPLETED_TASK_KEY, tutorialTaskNode.ID);

        private void Finish()
        {
            OnCompleted?.Invoke();
        }

        [Button]
        private void FindNodes()
        {
            tutorialNodes = new List<ABaseTutorialNode>();

            foreach (Node node in nodes)
            {
                if (node is ABaseTutorialNode taskNode)
                {
                    tutorialNodes.Add(taskNode);
                }
            }
        }

        #endregion
    }
}
