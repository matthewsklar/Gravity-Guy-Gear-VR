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
        #endregion
    }
}
