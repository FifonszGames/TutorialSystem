using Common.Runtime.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace TutorialSystem.Runtime.MonoBehaviours
{
    //THIS CLASS IS MADE FOR TESTING PURPOSES
    [RequireComponent(typeof(Button))]
    public class GameEventInvoker : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField]
        private GameEvent gameEvent;

        #endregion

        #region Private Fields

        private Button button;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(InvokeSkipping);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(InvokeSkipping);
        }

        #endregion

        #region Private Methods

        private void InvokeSkipping()
        {
            gameEvent.Invoke();
        }

        #endregion
    }
}
