﻿using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class Game : MonoBehaviour
    {
        #region Fields
        /// <summary>
        /// The main camera for the player
        /// </summary>
        public Camera MainCamera;

        /// <summary>
        /// The view camera for the player
        /// </summary>
        public Camera ViewCamera;

        /// <summary>
        /// The position of the camera after a victory
        /// </summary>
        public Vector3 VictoryPosition;
        #endregion

        #region Methods
        #region Initialization
        private void Awake()
        {
            GameManager.MainCamera = MainCamera;
            GameManager.ViewCamera = ViewCamera;

            MainCamera.enabled = true;
            ViewCamera.enabled = false;
        }

        private void Start()
        {
            OVRTouchpad.TouchHandler += HandleTouchpadHandler;

            Time.timeScale = 50.0f;
        }
        #endregion

        #region Update
        private void Update()
        {
            if (GameManager.IsVictory) return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.CurrentLevel.RestartLevel();

                return;
            }

            Utilities.SetText("Timescale: " + Time.timeScale + "%", GameObject.Find("TimescaleText"));
        }
        #endregion

        #region Input handling
        private static void HandleTouchpadHandler(object sender, EventArgs e)
        {
            var touchArgs = (OVRTouchpad.TouchArgs) e;

            switch (touchArgs.TouchType) {
                case OVRTouchpad.TouchEvent.SingleTap:
                    break;
                case OVRTouchpad.TouchEvent.Left:
                    Time.timeScale = Mathf.Clamp(Time.timeScale - 25.0f, 0.0f, 100.0f);
                    break;
                case OVRTouchpad.TouchEvent.Right:
                    Time.timeScale = Mathf.Clamp(Time.timeScale + 25.0f, 0.0f, 100.0f);
                    break;
                case OVRTouchpad.TouchEvent.Up:
                    GameManager.ChangeCamera();
                    break;
                case OVRTouchpad.TouchEvent.Down:
                    GameManager.ChangeCamera();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Cleanup
        private void OnDestroy()
        {
            OVRTouchpad.TouchHandler -= HandleTouchpadHandler;
        }
        #endregion
        #endregion
    }
}