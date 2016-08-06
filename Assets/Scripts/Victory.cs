using UnityEngine;

namespace Assets.Scripts
{
    public class Victory : MonoBehaviour {
        #region Fields
        /// <summary>
        /// The player camera
        /// </summary>
        public Camera PlayerCamera;

        /// <summary>
        /// The camera for the victory view
        /// </summary>
        public Camera VictoryCamera;
        #endregion

        #region Methods
        private void Start()
        {
            PlayerCamera.enabled = true;
            VictoryCamera.enabled = false;
        }
        #endregion
    }
}
