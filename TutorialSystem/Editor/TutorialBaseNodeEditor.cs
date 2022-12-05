using TutorialSystem.Runtime.Graph;
using UnityEngine;
using XNodeEditor;

namespace TutorialSystem.Editor
{
    [CustomNodeEditor(typeof(ABaseTutorialNode))]
    public class TutorialBaseNodeEditor : NodeEditor
    {
        #region Public Methods

        public override void OnHeaderGUI()
        {
            ABaseTutorialNode node = (ABaseTutorialNode) target;
            GUI.color = GetNodeColor(node);
            base.OnHeaderGUI();
        }

        #endregion

        #region Private Methods

        private static Color GetNodeColor(ABaseTutorialNode node)
        {
            if (node.IsCompleted)
            {
                return Color.green;
            }

            return node.InProgress ? Color.yellow : Color.white;
        }

        #endregion
    }
}
