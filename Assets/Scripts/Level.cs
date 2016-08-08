using Assets.Scripts.GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Level
    {
        #region Fields
        /// <summary>
        /// The current level number
        /// </summary>
        public int LevelIndex;

        /// <summary>
        /// The current level name
        /// </summary>
        private string _levelName;
        #endregion
        // TODO: Test different starting planet masses on level 3 to see if can get more circular orbit
        #region Methods
        #region Level handling
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
            StartLevel(LevelIndex + 1);

            Debug.Log("Player has finished level " + LevelIndex + 1);
        }

        /// <summary>
        /// Restart the current level
        /// </summary>
        public void RestartLevel()
        {
            EndLevel(LevelIndex);
            StartLevel(LevelIndex);
        }
        #endregion

        // TODO: Implement better
        public void MainMenu()
        {
            SceneManager.LoadScene("Main");
        }

        /// <summary>
        /// Show the victory screen
        /// </summary>
        /// <param name="rigidbody">Player rigidbody</param>
        /// <param name="transform">Player transform</param>
        public void VictoryScreen(Rigidbody rigidbody, Transform transform)
        {
            rigidbody.isKinematic = true;
            transform.position = GameObject.Find("View").GetComponent<Game>().VictoryPosition;

            Object.Destroy(GameObject.Find("Canvas"));
            Object.Instantiate(Resources.Load("VictoryCanvas"), transform.position + Camera.main.transform.forward * 100,
                Camera.main.transform.rotation);
        }

        #region Utilities
        /// <summary>
        /// Get the name of the level with with the given index
        /// </summary>
        /// <param name="index">The index of the level</param>
        /// <returns>The name of the level</returns>
        private static string LevelIndexToLevelName(int index)
        {
            return "Level " + index;
        }
        #endregion
        #endregion
    }
}
