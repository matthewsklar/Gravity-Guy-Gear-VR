using System;
using Assets.Scripts.GameManagement;

namespace Assets.Scripts.MainMenu
{
    public partial class MainMenu
    {
        #region Methods
        #region Input handling
        private static void HandleTouchHandler(object sender, EventArgs e)
        {
            var touchArgs = (OVRTouchpad.TouchArgs) e;

            if (touchArgs.TouchType != OVRTouchpad.TouchEvent.SingleTap) return;

            GameManager.CurrentLevel.StartLevel(1);

            OVRTouchpad.TouchHandler -= HandleTouchHandler;
        }
        #endregion
        #endregion
    }
}
