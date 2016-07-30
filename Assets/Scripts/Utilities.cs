using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public static class Utilities
    {
        #region Constants
        /// <summary>
        /// The Gravitational Constant
        /// </summary>
        public const float GRAVITATIONAL_CONSTANT = 3000f;
        #endregion

        #region Methods
        #region Velocity
        /// <summary>
        /// Set the velocity of the GameObject
        /// </summary>
        /// <param name="velocity">3D vector containing the velocity in each direction</param>
        /// <param name="g">The GameObject</param>
        public static void SetVelocity(Vector3 velocity, GameObject g)
        {
            g.GetComponent<Rigidbody>().velocity = velocity;
        }

        /// <summary>
        /// Set the velocity of the GameObject
        /// </summary>
        /// <param name="xVelocity">Velocity in the X-Direction</param>
        /// <param name="yVelocity">Velocity in the Y-Direction</param>
        /// <param name="zVelocity">Velocity in the Z-Direction</param>
        /// <param name="g">The GameObject</param>
        public static void SetVelocity(float xVelocity, float yVelocity, float zVelocity, GameObject g)
        {
            g.GetComponent<Rigidbody>().velocity = new Vector3(xVelocity, yVelocity, zVelocity);
        }

        /// <summary>
        /// Add a velocity to the current velocity of the GameObject
        /// </summary>
        /// <param name="velocity">3D vector containing the velocity in each direction</param>
        /// <param name="g">The GameObject</param>
        public static void AddVelocity(Vector3 velocity, GameObject g)
        {
            g.GetComponent<Rigidbody>().velocity += velocity;
        }

        /// <summary>
        /// Add a velocity to the current velocity of the GameObject
        /// </summary>
        /// <param name="xVelocity">Velocity in the X-Direction</param>
        /// <param name="yVelocity">Velocity in the Y-Direction</param>
        /// <param name="zVelocity">Velocity in the Z-Direction</param>
        /// <param name="g">The GameObject</param>
        public static void AddVelocity(float xVelocity, float yVelocity, float zVelocity, GameObject g)
        {
            g.GetComponent<Rigidbody>().velocity += new Vector3(xVelocity, yVelocity, zVelocity);
        }
        #endregion

        #region Text
        /// <summary>
        /// Set the text of the GameObject
        /// </summary>
        /// <param name="text">The text to set</param>
        /// <param name="g">The text GameObject</param>
        public static void SetText(string text, GameObject g)
        {
            g.GetComponent<Text>().text = text;
        }
        #endregion

        #region Rounding
        /// <summary>
        /// Round the given number to the specified decimal
        /// </summary>
        /// <param name="value">The value to round</param>
        /// <param name="decimals">The amount of decimals to round to (default = 1)</param>
        /// <returns></returns>
        public static decimal Round(float value, int decimals = 1)
        {
            return Math.Round((decimal)value, decimals, MidpointRounding.AwayFromZero);
        }
        #endregion
        #endregion
    }
}