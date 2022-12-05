using System;
using Common.Runtime.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace TutorialSystem.Runtime.Conditions
{
    [Serializable]
    public class BeginableNotCompletedCondition : ICondition
    {
        #region Serialized Fields

        [OdinSerialize, Required]
        private IBeginable beginable;

        #endregion

        #region Public Properties

        public bool IsMet => !beginable.IsCompleted;

        #endregion
    }
}
