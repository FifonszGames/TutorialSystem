using System;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace TutorialSystem.Runtime.Graph
{
    public abstract class ABaseNode<T> : Node where T : Node
    {
        [Input(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public T enter;
        [Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public T exit;

        #region Public Properties

        [field: SerializeField, ReadOnly]
        public string ID { get; private set; }

        #endregion

        #region Public Methods

        public override object GetValue(NodePort port)
        {
            return exit;
        }

        public bool HasPrevious()
        {
            return GetInputPort(nameof(enter)).IsConnected;
        }

        public virtual bool HasNext()
        {
            return GetOutputPort(nameof(exit)).IsConnected;
        }

        public virtual T GetNext()
        {
            if (!HasNext())
            {
                return null;
            }

            Node node = GetOutputPort(nameof(exit)).Connection.node;

            return node == null ? null : node as T;
        }

        public T GetPrevious()
        {
            if (!HasPrevious())
            {
                return null;
            }

            Node node = GetInputPort(nameof(enter)).Connection.node;

            return node == null ? null : node as T;
        }

        #endregion

        #region Protected Methods

        protected override void Init()
        {
            ID ??= Guid.NewGuid().ToString();
        }

        #endregion
    }
}
