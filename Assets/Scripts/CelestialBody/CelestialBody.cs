using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CelestialBody
{
    /// <summary>
    /// Contains all data related to celestial bodies.
    /// </summary>
    public partial class CelestialBody : MonoBehaviour
    {
        #region Variables
        #region Parameters
        // TODO: Replace with more options such as elliptical max/min
        public bool IsCircularOrbit = false;

        public bool IsEllipticalOrbit = false;

        /// <summary>
        /// The initial velocity of the GameObject
        /// </summary>
        public Vector3 InitialVelocity = Vector3.zero;

        /// <summary>
        /// The initial direction of the GameObject
        /// </summary>
        public Vector3 InitialDirection = Vector3.zero;

        /// <summary>
        /// The position of the other apsis in the orbit
        /// </summary>
        public Vector3 Apsis = Vector3.zero;

        /// <summary>
        /// The main body that the GameObject is orbiting
        /// </summary>
        public Transform Parent;
        #endregion

        private Vector3 _initialRelativeVelocity;

        private Vector3 Force
        {
            get { return CalculateGravitationalForce(); }
        }

        private List<Transform> _parents;
        #endregion

        #region Methods
        #region Initialization
        public void Awake()
        {
            _parents = new List<Transform>();

            if (IsCircularOrbit) _initialRelativeVelocity = CircularOrbit();
            else if (IsEllipticalOrbit) _initialRelativeVelocity = EllipticalOrbit();
            else _initialRelativeVelocity = InitialVelocity;

            if (Parent != null) _parents.Add(Parent);
        }

        public void Start()
        {
            if (IsCircularOrbit || IsEllipticalOrbit) InitialVelocity = CalculateInitialVelocity();

            Utilities.SetVelocity(InitialVelocity, gameObject);
        }
        #endregion

        #region Update
        // FixedUpdate is called whenever the Physics Engine Updates
        public void FixedUpdate()
        {
            ApplyForces();
        }
        #endregion

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log(gameObject.name);
        }
        #endregion
    }
}
