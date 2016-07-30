using System;
using Assets.Scripts.GameManagement;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        #region Methods
        #region Actions
        private void Jump()
        {
            Utilities.AddVelocity(Camera.main.transform.forward * _launchSpeed, gameObject);
        }
        #endregion

        #region Celestial Body Interactions
        private void Land(Collision collision)
        {
            Collider collisionCollider = collision.collider;

            Debug.Log("Player has landed on " + collisionCollider);

            Utilities.SetVelocity(collision.rigidbody.velocity, gameObject);

            Material material = collision.gameObject.GetComponent<Renderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);

            _landedBody = collisionCollider.gameObject;

            if (_landedBody.tag == "Finish") GameManager.CurrentLevel.FinishLevel();
        }

        private void LeavePlanet(Collision collision)
        {
            Debug.Log("Player has left " + collision.collider);

            Material material = collision.gameObject.GetComponent<Renderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 1.0f);

            _landedBody = null;
        }
        #endregion

        #region Update
        private void UpdateVelocity(float absY, float clampedY)
        {
            if (absY > 0.5f) _launchSpeed = Mathf.Clamp(_launchSpeed + clampedY / absY * 5.0f, 0.0f, 100.0f);
        }

        private void UpdateText()
        {
                Vector3 currentVelocity = gameObject.GetComponent<Rigidbody>().velocity;
                Vector3 launchVelocity = Camera.main.transform.forward * _launchSpeed;
                Vector3 netVelocity = currentVelocity + launchVelocity;

                Utilities.SetText(
                    "Current:" +
                    " X: " + Utilities.Round(currentVelocity.x) +
                    " Y: " + Utilities.Round(currentVelocity.y) +
                    " Z: " + Utilities.Round(currentVelocity.z),
                    GameObject.Find("CurrentVelocityText"));

               Utilities.SetText(
                    "Launch:" +
                    " X: " + Utilities.Round(launchVelocity.x) +
                    " Y: " + Utilities.Round(launchVelocity.y) +
                    " Z: " + Utilities.Round(launchVelocity.z),
                    GameObject.Find("LaunchVelocityText"));

                Utilities.SetText(
                    "Net:       " +
                    " X: " + Utilities.Round(netVelocity.x) +
                    " Y: " + Utilities.Round(netVelocity.y) +
                    " Z: " + Utilities.Round(netVelocity.z),
                    GameObject.Find("NetVelocityText"));
        }
        #endregion

        #region UI
        private void FacingCelestialBody()
        {
            RaycastHit hit;
            GameObject facingText = GameObject.Find("FacingText");

            if (!Physics.Raycast(transform.position, Camera.main.transform.forward, out hit) ||
                hit.collider.gameObject == _landedBody.gameObject) {
                Utilities.SetText("", facingText);

                return;
            }

            Vector3 velocity = hit.rigidbody.velocity;

            Utilities.SetText(
                "Current: " +
                " X: " + Mathf.Round(velocity.x) +
                " Y: " + Mathf.Round(velocity.y) +
                " Z: " + Mathf.Round(velocity.z),
                facingText);
        }
        #endregion

        #region Input handling
        private void HandleTouchHandler(object sender, EventArgs e)
        {
            var touchArgs = (OVRTouchpad.TouchArgs)e;

            if (touchArgs.TouchType == OVRTouchpad.TouchEvent.SingleTap) Jump();
        }

        private void HandleTouchpadHandler(object sender, EventArgs e)
        {
            var touchArgs = (Touchpad.TouchEventArgs)e;

            float x = touchArgs.XSwipe;
            float y = touchArgs.YSwipe;

            float absX = Mathf.Abs(x);
            float absY = Mathf.Abs(y);

            if (absX > absY) return;

            UpdateVelocity(absY, y);
        }
        #endregion

        #region Cleanup
        private void Dump()
        {
            OVRTouchpad.TouchHandler -= HandleTouchHandler;
            Touchpad.TouchHandler -= HandleTouchpadHandler;
        }
        #endregion
        #endregion
    }
}
