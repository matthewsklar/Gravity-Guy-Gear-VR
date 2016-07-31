using System.Diagnostics.CodeAnalysis;
using Assets.Scripts.GameManagement;
using UnityEngine;

namespace Assets.Scripts.MainMenu
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public partial class MainMenu : MonoBehaviour
    {
        #region Methods
        #region Initialization
        private void Awake()
        {
            GameManager.StartGame();
        }

        private void Start()
        {
            OVRTouchpad.TouchHandler += HandleTouchHandler;
        }
        #endregion
        #endregion
    }
}