using System;
using Assets.Scripts.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Tutorial
    {
        #region Fields
        /// <summary>
        /// The queue number of the tutorial for the level
        /// </summary>
        private readonly int _id;

        /// <summary>
        /// The message of the tutorial
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// The trigger to start the tutorial
        /// </summary>
        private readonly Func<bool> _messageStartTrigger;

        /// <summary>
        /// The trigger to end the tutorial
        /// </summary>
        private readonly Func<bool> _messageEndTrigger;

        /// <summary>
        /// The tutorial GameObject to display on screen
        /// </summary>
        private GameObject _tutorialMessage;

        /// <summary>
        /// If the tutorial is being displayed
        /// </summary>
        public bool IsDisplay;
        #endregion

        #region Methods
        #region Initialization
        /// <summary>
        /// Create a new tutorial that starts as soon as its queue is up
        /// </summary>
        /// <param name="message">The message of the tutorial</param>
        /// <param name="messageEndTrigger">The trigger to end the tutorial</param>
        public Tutorial(string message, Func<bool> messageEndTrigger)
        {
            _message = message;
            _messageEndTrigger = messageEndTrigger;

            _messageStartTrigger = () => true;

            IsDisplay = false;

            _id = GameManager.RegisteredTutorials.Count - 1;
        }

        /// <summary>
        /// Create a new tutorial with a dynamic start trigger
        /// </summary>
        /// <param name="message">The message of the tutorial</param>
        /// <param name="messageStartTrigger">The trigger to start the tutorial if it's queue is up</param>
        /// <param name="messageEndTrigger">The trigger to end the tutorial</param>
        public Tutorial(string message, Func<bool> messageStartTrigger, Func<bool> messageEndTrigger)
        {
            _message = message;
            _messageStartTrigger = messageStartTrigger;
            _messageEndTrigger = messageEndTrigger;

            IsDisplay = false;

            _id = GameManager.RegisteredTutorials.Count - 1;
        }
        #endregion

        #region Trigger
        /// <summary>
        /// The trigger that starts the tutorial
        /// </summary>
        public void StartTrigger()
        {
            if (_id >= 0) {
                if (GameManager.RegisteredTutorials[_id].IsDisplay) return;
            } 

            if (GameManager.CurrentTutorial != _id) return;

            if (!_messageStartTrigger.Invoke()) return;

            GameManager.CurrentTutorial++;

            DisplayTutorial();
        }

        /// <summary>
        /// The trigger that ends the tutorial
        /// </summary>
        public void EndTrigger()
        {
            if (!IsDisplay) return;

            try {
                if (!_messageEndTrigger.Invoke()) return;
            } catch {
                return;
            }

            RemoveTutorial();
        }
        #endregion

        #region Tutorial
        /// <summary>
        /// Display the tutorial and pause the game
        /// </summary>
        private void DisplayTutorial()
        {
            Time.timeScale = 0.0f;

            _tutorialMessage = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("Tutorial"));
            _tutorialMessage.transform.SetParent(GameObject.Find("Canvas").transform);
            _tutorialMessage.transform.localPosition = Vector3.zero;
            _tutorialMessage.transform.rotation = Camera.main.transform.rotation;

            _tutorialMessage.GetComponentsInChildren(typeof(Text))[0].GetComponent<Text>().text = _message;

            IsDisplay = true;

            Debug.Log("Tutorial display " + _message);
        }

        /// <summary>
        /// Remove the tutorial
        /// </summary>
        private void RemoveTutorial()
        {
            UnityEngine.Object.Destroy(_tutorialMessage);

            IsDisplay = false;

            Debug.Log("Tutorial remove display " + _message);
        }
        #endregion
        #endregion
    }
}