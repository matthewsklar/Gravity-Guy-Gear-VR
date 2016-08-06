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
        #endregion
    }
}
