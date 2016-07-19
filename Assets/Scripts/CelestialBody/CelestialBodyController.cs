using System.Collections.Generic;
using System.Linq;
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
        private Vector3 CircularOrbit()
        {
            float mu = Utilities.GRAVITATIONAL_CONSTANT * (GetComponent<Rigidbody>().mass + Parent.gameObject.GetComponent<Rigidbody>().mass);
            float distance = (transform.localPosition - Parent.transform.localPosition).magnitude;

            Vector3 relativeVelocity = InitialDirection * Mathf.Sqrt(mu / distance);

            Debug.Log("Calculated circular orbit of " + gameObject.name + " with relative initial velocity of " + relativeVelocity);

            return relativeVelocity;
        }

        private Vector3 EllipticalOrbit()
        {
            float mu = Utilities.GRAVITATIONAL_CONSTANT * (GetComponent<Rigidbody>().mass + Parent.gameObject.GetComponent<Rigidbody>().mass);
            float distance = (transform.position - Parent.transform.position).magnitude;
            float axis = (transform.position - Apsis).magnitude;

            Debug.Log("mu = " + mu + " r = " + distance + " axis = " + axis);

            Vector3 orbitalSpeed = InitialDirection * Mathf.Sqrt(mu * (2 / distance - 1 / (axis/2)));

            Debug.Log("Calculated elliptical orbit of " + gameObject.name + " with initial velocity of " + orbitalSpeed);

            return orbitalSpeed;

        }
        #endregion

        #region Calculations
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

        private Vector3 CalculateGravitationalForce()
        {
            Vector3 resultForce = Vector3.zero;

            foreach (Collider c in Physics.OverlapSphere(transform.position, Mathf.Infinity))
            {
                if (GetComponent<Collider>() == c) continue;

                Vector3 forceVector = transform.position - c.transform.position;
                Vector3 gravitationalForce = -Utilities.GRAVITATIONAL_CONSTANT * forceVector.normalized *
                                             ((GetComponent<Rigidbody>().mass * c.attachedRigidbody.mass) /
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
