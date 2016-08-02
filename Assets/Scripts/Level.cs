using System;
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
        public int LevelIndex;

        /// <summary>
        /// The current level name
        /// </summary>
        private string _levelName;
        #endregion

        #region Methods
        /// <summary>
        /// Start the level
        /// </summary>
        /// <param name="index">The level number</param>
        public void StartLevel(int index)
        {
            LevelIndex = index;
            _levelName = LevelIndexToLevelName(LevelIndex);

            SceneManager.LoadScene(_levelName);
        }

        /// <summary>
        /// End a level
        /// </summary>
        /// <param name="index">The index of the level to unload</param>
        public void EndLevel(int index)
        {
            SceneManager.UnloadScene(LevelIndexToLevelName(index));
        }

        /// <summary>
        /// Unload the current level and start the next level
        /// </summary>
        public void FinishLevel()
        {
            EndLevel(LevelIndex);

            // TODO: Handle differently (prompt user what they want to do)
            StartLevel(LevelIndex + 1);

            Debug.Log("Player has finished level " + LevelIndex + 1);
        }

        /// <summary>
        /// Get the name of the level with with the given index
        /// </summary>
        /// <param name="index">The index of the level</param>
        /// <returns>The name of the level</returns>
        private string LevelIndexToLevelName(int index)
        {
            return "Level " + index;
        }
        #endregion
    }
}
