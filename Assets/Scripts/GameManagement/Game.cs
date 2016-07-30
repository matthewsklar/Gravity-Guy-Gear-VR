using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class Game : MonoBehaviour
    {
        #region Variables
        private bool _firstSwipe;
        #endregion

        #region Methods
        #region Initialization
        private void Start()
        {
            Touchpad.Create();

            _firstSwipe = true;
        }
        #endregion

        #region Update
        private void Update()
        {
            float clampedX = Input.GetAxis("Mouse X");
            float absX = Math.Abs(clampedX);

            if (absX < 0.5f)
            {
                _firstSwipe = true;

                return;
            }

            if (_firstSwipe)
            {
                _firstSwipe = false;

                return;
            }

            Time.timeScale = Mathf.Clamp(Time.timeScale + clampedX / absX * 10.0f, 0.0f, 100.0f);
            Utilities.SetText("Timescale: " + Time.timeScale + "%", GameObject.Find("TimescaleText"));
        }
        #endregion

        #region UI
        private void FacingCelestialBody()
        {
            //Raycast
        }
        #endregion
        #endregion
    }
}