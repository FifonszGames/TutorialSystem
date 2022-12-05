using Common.Runtime.Abstracts;
using UnityEngine;

namespace TutorialSystem.Runtime.Events
{
    [CreateAssetMenu(fileName = nameof(SkipTutorial), menuName = "SO/" + nameof(TutorialSystem) + "/" + nameof(SkipTutorial))]
    public class SkipTutorial : GameEvent
    {
        
    }
}
