using Assets.Scripts.GameManagement;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        private void Jump()
        {
            Utilities.AddVelocity(Camera.main.transform.forward * _launchSpeed, gameObject);
        }

        private void Land(Collision collision)
        {
            Collider collisionCollider = collision.collider;

            Debug.Log("Player has landed on " + collisionCollider);

            Utilities.SetVelocity(collision.rigidbody.velocity, gameObject);

            Material material = collision.gameObject.GetComponent<Renderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);

            if (collisionCollider.gameObject.tag == "Finish") GameManager.CurrentLevel.FinishLevel();
        }

        private static void LeavePlanet(Collision collision)
        {
            Debug.Log("Player has left " + collision.collider);

            Material material = collision.gameObject.GetComponent<Renderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 1.0f);
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
                    "Launch:" +
                    " X: " + Utilities.Round(netVelocity.x) +
                    " Y: " + Utilities.Round(netVelocity.y) +
                    " Z: " + Utilities.Round(netVelocity.z),
                    GameObject.Find("NetVelocityText"));
        }

        /*private void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 force)
        {
            int numSteps = 10000;
            float dT = 1.0f / initialVelocity.magnitude;
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetVertexCount(numSteps);

            Vector3 position = initialPosition;
            Vector3 velocity = initialVelocity;

            for (int i = 0; i < numSteps; i++)
            {
                lineRenderer.SetPosition(i, position);

                position += velocity * dT + 0.5f * force * dT * dT;
                velocity += force * dT;
            }
        }*/

        private void Dump()
        {
            Touchpad.TouchHandler -= HandleTouchHandler;
        }
    }
}
