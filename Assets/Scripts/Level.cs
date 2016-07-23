using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Level
    {
        #region Variables
        /// <summary>
        /// The current level number
        /// </summary>
        private int _levelIndex;

        /// <summary>
        /// The current level name
        /// </summary>
        private string _levelName;
        #endregion

        #region Methods
        /// <summary>
        /// Start the level
        /// </summary>
        /// <param name="level">The level number</param>
        public void StartLevel(int level)
        {
            _levelIndex = level;
            _levelName = "Level " + _levelIndex;

            SceneManager.LoadScene(_levelName);
        }

        /// <summary>
        /// Unload the current level and start the next level
        /// </summary>
        public void FinishLevel()
        {
            SceneManager.UnloadScene(_levelName);

            // TODO: Handle differently (prompt user what they want to do)
            StartLevel(_levelIndex + 1);

            Debug.Log("Player has finished level " + _levelIndex + 1);
        }
        #endregion
    }
}
