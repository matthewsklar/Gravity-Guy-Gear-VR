namespace Assets.Scripts.GameManagement
{
    public class GameManager
    {
        #region Methods
        /// <summary>
        /// All the information and actions related to the current level
        /// </summary>
        public static Level CurrentLevel;

        /// <summary>
        /// Start the game
        /// </summary>
        public static void StartGame()
        {
            CurrentLevel = new Level();
        }
        #endregion
    }
}
