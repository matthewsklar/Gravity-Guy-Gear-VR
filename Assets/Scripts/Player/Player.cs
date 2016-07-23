using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The speed that the player will jump at
        /// </summary>
        private float _launchSpeed;
        #endregion

        #region Methods
        #region Initialization
        private void Awake()
        {
            _launchSpeed = 50.0f;

            Touchpad.TouchHandler += HandleTouchHandler;
        }
        #endregion

        #region Update
        private void Update()
        {
            UpdateText();
        }

        private void FixedUpdate()
        {
            //_player.UpdateTrajectory(transform.position, GetComponent<Rigidbody>().velocity + Camera.main.transform.forward * 30.0f, GetComponent<CelestialBodyModel>().GravitationalForce);
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

            _launchSpeed = Mathf.Clamp(_launchSpeed += touchArgs.YSwipe * 30, 0.0f, 100.0f);

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