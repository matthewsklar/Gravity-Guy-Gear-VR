using System;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Touchpad
    {
        #region EventArgs
        public class TouchEventArgs : EventArgs
        {
            public bool SingleTap;

            public float XSwipe;
            public float YSwipe;
        }
        #endregion

        #region Variables
        public static bool IsCreated;

        #region Events
        public static event EventHandler TouchHandler;
        #endregion
        #endregion

        #region Methods
        public static void Create()
        {
            if (IsCreated) return;

            OVRTouchpad.TouchHandler += HandleTouchHandler;

            IsCreated = true;
        }

        private static void HandleTouchHandler(object sender, EventArgs e)
        {
            var touchArgs = (OVRTouchpad.TouchArgs) e;
            var touchEventArgs = new TouchEventArgs();

            float clampedX = Input.GetAxis("Mouse X");
            float clampedY = Input.GetAxis("Mouse Y");

            touchEventArgs.SingleTap = false;

            switch (touchArgs.TouchType)
            {
                case OVRTouchpad.TouchEvent.SingleTap:
                    touchEventArgs.SingleTap = true;
                    break;
                case OVRTouchpad.TouchEvent.Left:
                    touchEventArgs.XSwipe = clampedX;
                    break;
                case OVRTouchpad.TouchEvent.Right:
                    touchEventArgs.XSwipe = clampedX;
                    break;
                case OVRTouchpad.TouchEvent.Up:
                    touchEventArgs.YSwipe = clampedY;
                    break;
                case OVRTouchpad.TouchEvent.Down:
                    touchEventArgs.YSwipe = clampedY;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (TouchHandler != null) TouchHandler(null, touchEventArgs);
        }
        #endregion
    }
}
