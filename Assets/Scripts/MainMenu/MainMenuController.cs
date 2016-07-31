using System;
using Assets.Scripts.GameManagement;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.MainMenu
{
    public partial class MainMenu
    {
        private static void HandleTouchHandler(object sender, EventArgs e)
        {
            var touchArgs = (OVRTouchpad.TouchArgs) e;

            if (touchArgs.TouchType != OVRTouchpad.TouchEvent.SingleTap) return;

            SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
            GameManager.CurrentLevel.StartLevel(1);

            OVRTouchpad.TouchHandler -= HandleTouchHandler;
        }
    }
}
