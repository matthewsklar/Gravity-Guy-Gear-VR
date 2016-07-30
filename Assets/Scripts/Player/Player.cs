using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public partial class Player : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The speed that the player will jump at
        /// </summary>
        private float _launchSpeed;

        private bool _firstSwipe;
        #endregion

        #region Methods
        #region Initialization
        private void Awake()
        {
            _launchSpeed = 50.0f;
            _firstSwipe = true;

            Touchpad.TouchHandler += HandleTouchHandler;
        }
        #endregion

        #region Update
        private void Update()
        {
            UpdateText();

            float clampedY = Input.GetAxis("Mouse Y");
            float absY = Math.Abs(clampedY);

            if (absY < 0.5f) {
                _firstSwipe = true;

                return;
            }

            if (_firstSwipe) {
                _firstSwipe = false;

                return;
            }

            UpdateVelocity(absY, clampedY);
        }
        #endregion

        #region Collision detection
        private void OnCollisionEnter(Collision collision)
        {
            Land(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            LeavePlanet(collision);
        }
        #endregion

        #region Input handling
        private void HandleTouchHandler(object sender, EventArgs e)
        {
            var touchArgs = (Touchpad.TouchEventArgs) e;

            if (touchArgs.SingleTap) Jump();
        }
        #endregion

        #region Cleanup
        private void OnDestroy()
        {
            Dump();
        }
        #endregion
        #endregion
    }
}