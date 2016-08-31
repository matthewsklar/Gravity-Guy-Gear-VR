using Assets.Scripts.GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

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

            GameManager.CurrentTutorial = -1;

            ImplementTutorials();

            SceneManager.LoadScene(_levelName);
        }

        /// <summary>
        /// End a level
        /// </summary>
        /// <param name="index">The index of the level to unload</param>
        public void EndLevel(int index)
        {
            SceneManager.UnloadScene(LevelIndexToLevelName(index));

            GameManager.DeregisterTutorials();
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

        // TODO: Make work with different levels although now for testing it will only work for level 1 and check for more effective implementation other than switch/case
        public void ImplementTutorials()
        {
            switch (LevelIndex) {
                case 1:
                    GameManager.RegisterTutorial(new Tutorial("Welcome to GRAVITY GUY\nTAP to continue",
                        () => GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.SingleTap));

                    GameManager.RegisterTutorial(new Tutorial("Take a look at your surroundings\nTAP to continue",
                        () => GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.SingleTap));

                    GameManager.RegisterTutorial(new Tutorial("Swipe RIGHT to speed up time",
                        () => GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.Left));

                    GameManager.RegisterTutorial(new Tutorial("Swipe LEFT to slow down time",
                        () => GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.Left,
                        () => GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.Right));

                    GameManager.RegisterTutorial(new Tutorial("Swipe UP or DOWN to toggle map view",
                        () =>
                            GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.Up ||
                            GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.Down));

                    GameManager.RegisterTutorial(
                        new Tutorial("If you get stuck, you can press BACK to restart the level\nTAP to continue",
                            () => GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.SingleTap));

                    GameManager.RegisterTutorial(new Tutorial("TAP to jump\nTAP to continue",
                        () => GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.SingleTap));

                    GameManager.RegisterTutorial(new Tutorial("Get to the glowing planet to win\nTAP to end tutorial",
                        () => GameManager.LastTouchEvent.TouchType == OVRTouchpad.TouchEvent.SingleTap));
                    break;
            }
        }

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