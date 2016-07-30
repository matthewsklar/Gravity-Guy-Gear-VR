using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public class Game : MonoBehaviour
    {
        #region Methods
        #region Initialization
        private void Start()
        {
            Touchpad.Create();
            Touchpad.TouchHandler += HandleTouchpadHandler;
        }
        #endregion

        #region Update
        private void Update()
        {
            Touchpad.Update();
        }
        #endregion

        #region Input dandling
        private static void HandleTouchpadHandler(object sender, EventArgs e)
        {
            var touchArgs = (Touchpad.TouchEventArgs)e;

            float x = touchArgs.XSwipe;
            float y = touchArgs.YSwipe;

            float absX = Math.Abs(x);
            float absY = Math.Abs(y);

            if (absY > absX) return;

            Time.timeScale = Mathf.Clamp(Time.timeScale + x / absX * 10.0f, 0.0f, 100.0f);
            Utilities.SetText("Timescale: " + Time.timeScale + "%", GameObject.Find("TimescaleText"));
        }
        #endregion
        #endregion
    }
}