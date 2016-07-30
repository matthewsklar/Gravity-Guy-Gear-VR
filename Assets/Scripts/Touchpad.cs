﻿using System;
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

        private static bool _firstSwipe;

        #region Events
        public static event EventHandler TouchHandler;
        #endregion
        #endregion

        #region Methods
        public static void Create()
        {
            if (IsCreated) return;

            _firstSwipe = true;

            IsCreated = true;
        }

        public static void Update()
        {
            const float minimumSwipe = 0.5f;

            var touchEventArgs = new TouchEventArgs()
            {
                XSwipe = Input.GetAxis("Mouse X"),
                YSwipe = Input.GetAxis("Mouse Y")
            };

            if (Mathf.Max(Mathf.Abs(touchEventArgs.XSwipe), Mathf.Abs(touchEventArgs.YSwipe)) < minimumSwipe)
            {
                _firstSwipe = true;

                return;
            }

            if (_firstSwipe)
            {
                _firstSwipe = false;

                return;
            }

            if (TouchHandler == null) return;

            TouchHandler(null, touchEventArgs);
        }
        #endregion
    }
}
