using System;

namespace TutorialSystem.Runtime.Interfaces
{
    public interface IUIHighlighter
    {
        #region Public Methods

        public void CheckCondition();
        public void ToggleElement(bool toggle, Action<bool> onFinish = null);

        #endregion
    }
}
