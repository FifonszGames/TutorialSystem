using UnityEngine;

namespace TutorialSystem.Runtime.Helpers
{
    public static class TutorialHelpers
    {
        #region Constants

        public const string LAST_COMPLETED_TASK_KEY = "LastCompletedTask";
        public const string TUTORIAL_FINISHED_KEY = "TutorialFinished";

        #endregion

        #region Public Methods

        public static bool TutorialFinished()
        {
            if (!PlayerPrefs.HasKey(TUTORIAL_FINISHED_KEY))
            {
                return false;
            }

            return PlayerPrefs.GetInt(TUTORIAL_FINISHED_KEY) == 1;
        }

        public static bool HasSavedData(out string nodeId)
        {
            if (PlayerPrefs.HasKey(LAST_COMPLETED_TASK_KEY))
            {
                nodeId = PlayerPrefs.GetString(LAST_COMPLETED_TASK_KEY);

                return true;
            }

            nodeId = string.Empty;

            return false;
        }

        #endregion
    }
}
