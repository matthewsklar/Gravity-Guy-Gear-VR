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

        /// <summary>
        /// If the player is landed on a GameObject after the next FixedUpdate call
        /// </summary>
        private bool _isLanded;

        /// <summary>
        /// The GameObject, if any, that the player is currently landed on
        /// </summary>
        private GameObject _landedBody;
        #endregion

        #region Methods
        #region Initialization
        private void Awake()
        {
            _launchSpeed = 50.0f;
            _isLanded = false;
            _landedBody = null;

            OVRTouchpad.TouchHandler += HandleTouchHandler;
            Touchpad.TouchHandler += HandleTouchpadHandler;
        }
        #endregion

        #region Update
        private void Update()
        {
            FacingCelestialBody();

            UpdateText();
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

        #region Cleanup
        private void OnDestroy()
        {
            Dump();
        }
        #endregion
        #endregion
    }
}