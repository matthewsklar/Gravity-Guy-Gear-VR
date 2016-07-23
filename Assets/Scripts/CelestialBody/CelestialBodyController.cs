using UnityEngine;

namespace Assets.Scripts.CelestialBody
{
    public partial class CelestialBody
    {
        #region Methods
        private void ApplyForces()
        {
            GetComponent<Rigidbody>().AddForce(CalculateGravitationalForce());
        }

        #region Orbits
        /// <summary>
        /// Calculate the initial velocity needed for the GameObject to have a circular orbit around the parent GameObject
        /// </summary>
        /// <returns>The initial velocity</returns>
        private Vector3 CircularOrbit()
        {
            float mu = Utilities.GRAVITATIONAL_CONSTANT * (GetComponent<Rigidbody>().mass + Parent.gameObject.GetComponent<Rigidbody>().mass);
            float distance = (transform.localPosition - Parent.transform.localPosition).magnitude;

            Vector3 initialOrbitVelocity = InitialDirection * Mathf.Sqrt(mu / distance);

            Debug.Log("Calculated circular orbit of " + gameObject.name + " with relative initial velocity of " + initialOrbitVelocity);

            return initialOrbitVelocity;
        }

        /// <summary>
        /// Calculate the initial velocity needed for the GameObject to have an elliptical orbit (e in range [0, 1)) 
        /// with apsides at the starting position and the specified location
        /// </summary>
        /// <returns>The initial velocity</returns>
        private Vector3 EllipticalOrbit()
        {
            float mu = Utilities.GRAVITATIONAL_CONSTANT * (GetComponent<Rigidbody>().mass + Parent.gameObject.GetComponent<Rigidbody>().mass);
            float distance = (transform.position - Parent.transform.position).magnitude;
            float semiMajorAxis = (transform.position - Apsis).magnitude / 2;

            Vector3 initialOrbitVelocity = InitialDirection * Mathf.Sqrt(mu * (2 / distance - 1 / semiMajorAxis));

            Debug.Log("Calculated elliptical orbit of " + gameObject.name + " with initial velocity of " + initialOrbitVelocity);

            return initialOrbitVelocity;
        }
        #endregion

        #region Calculations
        /// <summary>
        /// Calculate the initial velocity of the GameObject in respect to all parent GameObjects
        /// </summary>
        /// <returns>The initial velocity</returns>
        private Vector3 CalculateInitialVelocity()
        {
            Vector3 initialVelocity = _initialRelativeVelocity;

            for (var i = 0; i < _parents.Count; i++)
            {
                initialVelocity += _parents[i].gameObject.GetComponent<CelestialBody>()._initialRelativeVelocity;

                Transform parentParent = _parents[i].gameObject.GetComponent<CelestialBody>().Parent;

                if (parentParent != null) _parents.Add(parentParent);
            }

            Debug.Log("Calculated initial velocity of " + initialVelocity + " for " + gameObject.name);

            return initialVelocity;
        }

        /// <summary>
        /// Calculate the gravitational force on the GameObject
        /// </summary>
        /// <returns>The gravitational force</returns>
        private Vector3 CalculateGravitationalForce()
        {
            Vector3 resultForce = Vector3.zero;

            foreach (Collider c in Physics.OverlapSphere(transform.position, Mathf.Infinity))
            {
                if (GetComponent<Collider>() == c) continue;

                Vector3 forceVector = transform.position - c.transform.position;
                Vector3 gravitationalForce = -Utilities.GRAVITATIONAL_CONSTANT * forceVector.normalized *
                                             (GetComponent<Rigidbody>().mass * c.attachedRigidbody.mass /
                                              (forceVector.magnitude *
                                               forceVector.magnitude));

                resultForce += gravitationalForce;
            }

            return resultForce;
        }
        #endregion
        #endregion
    }
}
