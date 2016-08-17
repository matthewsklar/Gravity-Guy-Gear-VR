using System;
using System.Linq;
using Assets.Scripts.GameManagement;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        #region Methods
        #region Actions
        /// <summary>
        /// Handle the jump action for the player
        /// </summary>
        private void Jump()
        {
            // TODO: Fix jumping in jump when paused
            Vector3 forward = GameManager.MainCamera.transform.forward;

            if (_isLanded) Utilities.AddVelocity(forward * _launchSpeed, gameObject);

            if (!Time.timeScale.Equals(0.0f)) return;

            _isLanded = !_isLanded;

            if (_isLanded) Utilities.AddVelocity(-forward * _launchSpeed, gameObject);

        }

        private void Victory()
        {
            GameManager.CurrentLevel.VictoryScreen(gameObject.GetComponent<Rigidbody>(), transform);

            GameManager.IsVictory = true;
        }
        #endregion

        #region Celestial Body Interactions
        /// <summary>
        /// Handle the player landing on a celestial body
        /// </summary>
        /// <param name="collision">Information about the collision objects</param>
        private void Land(Collision collision)
        {
            Collider collisionCollider = collision.collider;

            Debug.Log("Player has landed on " + collisionCollider);

            Utilities.SetVelocity(collision.rigidbody.velocity, gameObject);

            Material material = collision.gameObject.GetComponent<Renderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);

            _isLanded = true;
            _landedBody = collisionCollider.gameObject;

            if (_landedBody.tag == "Finish") Victory();
        }

        /// <summary>
        /// Handle the player leaving a celestial body
        /// </summary>
        /// <param name="collision">Information about the collision objects</param>
        private void LeavePlanet(Collision collision)
        {
            Debug.Log("Player has left " + collision.collider);

            Material material = collision.gameObject.GetComponent<Renderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 1.0f);

            _isLanded = false;
            _landedBody = null;
        }
        #endregion

        #region Update
        private void UpdateText()
        {
            Vector3 currentVelocity = gameObject.GetComponent<Rigidbody>().velocity;
            Vector3 launchVelocity = GameManager.MainCamera.transform.forward * _launchSpeed;
            Vector3 netVelocity = currentVelocity + launchVelocity;

            Utilities.SetText(
                "Current:" +
                " X: " + Mathf.Round(currentVelocity.x) +
                " Y: " + Mathf.Round(currentVelocity.y) +
                " Z: " + Mathf.Round(currentVelocity.z),
                GameObject.Find("CurrentVelocityText"));

            Utilities.SetText(
                "Launch:" +
                " X: " + Mathf.Round(launchVelocity.x) +
                " Y: " + Mathf.Round(launchVelocity.y) +
                " Z: " + Mathf.Round(launchVelocity.z),
                GameObject.Find("LaunchVelocityText"));

            Utilities.SetText(
                "Net:       " +
                " X: " + Mathf.Round(netVelocity.x) +
                " Y: " + Mathf.Round(netVelocity.y) +
                " Z: " + Mathf.Round(netVelocity.z),
                GameObject.Find("NetVelocityText"));

            Utilities.SetText(_isLanded ? "" : "Jump", GameObject.Find("JumpText"));
        }

        private void FacingCelestialBody()
        {
            RaycastHit hit;
            GameObject facingText = GameObject.Find("FacingText");

            if (!Physics.Raycast(transform.position, GameManager.MainCamera.transform.forward, out hit)) {
                try {
                    if (hit.collider.gameObject == _landedBody.gameObject) return;
                    // ReSharper disable once UnusedVariable
                } catch(NullReferenceException e) {
                    //I would throw the error but it happens all the time so it would cluster the log but here's a new throw
                    //   eee
                    // ee   ee
                    //e       e
                    //Debug.Log(e);
                }

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
            var touchArgs = (OVRTouchpad.TouchArgs) e;

            if (touchArgs.TouchType != OVRTouchpad.TouchEvent.SingleTap) return;

            if (GameManager.RegisteredTutorials.Any(t => t.IsDisplay)) return;

            Jump();
        }

        // TODO: Implement better
        public void ButtonHandler(int index)
        {
            if (index == 0) GameManager.CurrentLevel.MainMenu();
            else GameManager.CurrentLevel.FinishLevel();
        }
        #endregion

        #region Cleanup
        private void Dump()
        {
            OVRTouchpad.TouchHandler -= HandleTouchHandler;
        }
        #endregion
        #endregion
    }
}