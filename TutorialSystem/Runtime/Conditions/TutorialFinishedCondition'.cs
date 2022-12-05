using Common.Runtime.Interfaces;
using TutorialSystem.Runtime.Helpers;

namespace TutorialSystem.Runtime.Conditions
{
    public class TutorialFinishedCondition : ICondition
    {
        public bool IsMet => TutorialHelpers.TutorialFinished();
    }
}
