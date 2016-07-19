using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player : MonoBehaviour
    {
        public bool IsLanded;

        public float LaunchSpeed = 50.0f;

        public void Awake()
        {
            IsLanded = false;

            OVRTouchpad.TouchHandler += HandleTouchHandler;
        }

        private void FixedUpdate()
        {
            //_player.UpdateTrajectory(transform.position, GetComponent<Rigidbody>().velocity + Camera.main.transform.forward * 30.0f, GetComponent<CelestialBodyModel>().GravitationalForce);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Land(collision);

            IsLanded = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            LeavePlanet(collision);

            IsLanded = false;
        }

        private void HandleTouchHandler(object sender, EventArgs e)
        {
            var touchArgs = (OVRTouchpad.TouchArgs) e;

            switch (touchArgs.TouchType) {
                case OVRTouchpad.TouchEvent.SingleTap:
                    Jump();
                    break;
                case OVRTouchpad.TouchEvent.Left:
                    break;
                case OVRTouchpad.TouchEvent.Right:
                    break;
                case OVRTouchpad.TouchEvent.Up:
                    if (LaunchSpeed < 100.0f) LaunchSpeed += 5.0f;
                    break;
                case OVRTouchpad.TouchEvent.Down:
                    if (LaunchSpeed > 5.0f) LaunchSpeed -= 5.0f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}