using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
    public class GameManager
    {
        #region Fields
        /// <summary>
        /// All the information and actions related to the current level
        /// </summary>
        public static Level CurrentLevel;

        /// <summary>
        /// The main camera for the player
        /// </summary>
        public static Camera MainCamera;

        /// <summary>
        /// The view camera for the player
        /// </summary>
        public static Camera ViewCamera;

        /// <summary>
        /// If the player has beaten the current level
        /// </summary>
        public static bool IsVictory;

        /// <summary>
        /// The current part of tutorial in the current level
        /// </summary>
        public static int CurrentTutorial;

        /// <summary>
        /// The registered parts of a tutorial for the current level
        /// </summary>
        public static List<Tutorial> RegisteredTutorials = new List<Tutorial>();

        /// <summary>
        /// The last touch event
        /// </summary>
        public static OVRTouchpad.TouchArgs LastTouchEvent;
        #endregion

        #region Methods
        /// <summary>
        /// Start the game
        /// </summary>
        public static void StartGame()
        {
            CurrentLevel = new Level();
            IsVictory = false;
        }

        /// <summary>
        /// Change the active camera
        /// </summary>
        public static void ChangeCamera()
        {
            MainCamera.enabled = !MainCamera.enabled;
            ViewCamera.enabled = !ViewCamera.enabled;
        }

        /// <summary>
        /// Register tutorials
        /// </summary>
        /// <param name="tutorial">The tutorial to register</param>
        public static void RegisterTutorial(Tutorial tutorial)
        {
            RegisteredTutorials.Add(tutorial);
        }

        /// <summary>
        /// Deregister all the registered tutorials
        /// </summary>
        public static void DeregisterTutorials()
        {
            RegisteredTutorials.Clear();
        }
        #endregion
    }
}
