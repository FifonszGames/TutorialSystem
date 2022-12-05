using Common.Runtime.Abstracts;
using UnityEngine;

namespace TutorialSystem.Runtime.Events
{
    [CreateAssetMenu(fileName = nameof(SkipCurrentStep), menuName = "SO/" + nameof(TutorialSystem) + "/" + nameof(SkipCurrentStep))]
    public class SkipCurrentStep : GameEvent
    {
    }
}
