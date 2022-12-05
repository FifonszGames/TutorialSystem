using System;
using TutorialSystem.Runtime.Graph;
using XNodeEditor;

namespace TutorialSystem.Editor
{
    [CustomNodeGraphEditor(typeof(TutorialGraph))]
    public class TutorialGraphEditor : NodeGraphEditor
    {
        private TutorialGraph TutorialGraph => target as TutorialGraph;

        #region Public Methods

        public override string GetNodeMenuName(Type type)
        {
            if (typeof(TutorialGraphNode).IsAssignableFrom(type))
            {
                return nameof(TutorialGraphNode);
            }

            return typeof(TutorialTaskNode).IsAssignableFrom(type) ? nameof(TutorialTaskNode) : null;
        }

        public override void OnOpen()
        {
            base.OnOpen();
            TrySetFirstNode();
        }

        #endregion

        #region Private Methods

        private void TrySetFirstNode()
        {
            TutorialGraph.FindFirstNode();
        }

        #endregion
    }
}
