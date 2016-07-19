using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        public void Jump()
        {
            Utilities.AddVelocity(Camera.main.transform.forward * LaunchSpeed, gameObject);
        }

        public void Land(Collision collision)
        {
            Debug.Log("Player has landed on " + collision.collider.ToString());

            Utilities.SetVelocity(collision.rigidbody.velocity, gameObject);

            Material material = collision.gameObject.GetComponent<Renderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 0.5f);
        }

        public void LeavePlanet(Collision collision)
        {
            Debug.Log("Player has left " + collision.collider.ToString());

            Material material = collision.gameObject.GetComponent<Renderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, 1.0f);
        }

        public void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 force)
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
        }
    }
}
