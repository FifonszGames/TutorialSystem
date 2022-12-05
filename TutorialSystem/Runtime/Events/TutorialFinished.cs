using Common.Runtime.Abstracts;
using UnityEngine;

namespace TutorialSystem.Runtime.Events
{
    [CreateAssetMenu(fileName = nameof(TutorialFinished), menuName = "SO/" + nameof(TutorialSystem) + "/" + nameof(TutorialFinished))]
    public class TutorialFinished : GameEvent
    {
    }
}
