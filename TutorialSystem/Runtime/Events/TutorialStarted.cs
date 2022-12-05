using Common.Runtime.Abstracts;
using UnityEngine;

namespace TutorialSystem.Runtime.Events
{
    [CreateAssetMenu(fileName = nameof(TutorialStarted), menuName = "SO/" + nameof(TutorialSystem) + "/" + nameof(TutorialStarted))]
    public class TutorialStarted : GameEvent
    {
    }
}
