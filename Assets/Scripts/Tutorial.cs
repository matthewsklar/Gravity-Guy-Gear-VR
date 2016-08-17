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
        /// The message of the tutorial
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// The trigger to check if to add the tutorial message
        /// </summary>
        private readonly Func<bool> _messageStartTrigger;

        private readonly Func<bool> _messageEndTrigger;

        public bool IsDisplay;

        private GameObject _tutorialMessage;
        #endregion

        #region Methods
        #region Initialization
        public Tutorial(string message, Func<bool> messageStartTrigger, Func<bool> messageEndTrigger)
        {
            _message = message;
            _messageStartTrigger = messageStartTrigger;
            _messageEndTrigger = messageEndTrigger;
            IsDisplay = false;
        }
        #endregion

        public void CheckTrigger()
        {
            if (!_messageStartTrigger.Invoke()) return;

            GameManager.CurrentTutorial++;

            DisplayMessage();
        }

        public void CheckDisplay()
        {
            if (!IsDisplay) return;

            try {
                if (!_messageEndTrigger.Invoke()) return;
            } catch {
                return;
            }

            RemoveMessage();
        }

        private void DisplayMessage()
        {
            Time.timeScale = 0.0f;

            _tutorialMessage = new GameObject("TutorialMessage");
            _tutorialMessage.transform.SetParent(GameObject.Find("Canvas").transform);

            var messageText = _tutorialMessage.AddComponent<Text>();
            messageText.text = _message;

            var arialFont = (Font) Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            messageText.font = arialFont;
            messageText.material = arialFont.material;

            messageText.transform.localPosition = Vector3.zero;

            IsDisplay = true;

            Debug.Log("Tutorial display " + _message);
        }

        private void RemoveMessage()
        {
            UnityEngine.Object.Destroy(_tutorialMessage);

            IsDisplay = false;

            Debug.Log("Tutorial remove display " + _message);
        }
        #endregion
    }
}