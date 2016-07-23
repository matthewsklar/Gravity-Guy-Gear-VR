using System;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
    public class Game : MonoBehaviour
    {
        #region Methods
        #region Initialization
        private void Start()
        {
            Touchpad.Create();
            Touchpad.TouchHandler += HandleTouchHandler;
        }
        #endregion

        #region Input Handling
        // TODO: Fix lowering timescale
        private static void HandleTouchHandler(object sender, EventArgs e)
        {
            var touchArgs = (Touchpad.TouchEventArgs) e;

            Mathf.Clamp(Time.timeScale += touchArgs.XSwipe * 50, 0.0f, 100.0f);
            Utilities.SetText("Timescale: " + Utilities.Round(Time.timeScale) + "%", GameObject.Find("TimescaleText"));
        }
        #endregion

        #region Cleanup
        private void OnDestroy()
        {
            Touchpad.TouchHandler -= HandleTouchHandler;
        }
        #endregion
        #endregion
    }
}