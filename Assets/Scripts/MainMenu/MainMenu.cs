using System;
using Assets.Scripts.GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.StartGame();
        }

        // Use this for initialization
        private void Start()
        {
            OVRTouchpad.TouchHandler += HandleTouchHandler;
        }

        // Update is called once per frame
        private void Update()
        {

        }

        private void HandleTouchHandler(object sender, EventArgs e)
        {
            var touchArgs = (OVRTouchpad.TouchArgs)e;

            switch (touchArgs.TouchType)
            {
                case OVRTouchpad.TouchEvent.SingleTap:
                    SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
                    GameManager.CurrentLevel.StartLevel(1);

                    OVRTouchpad.TouchHandler -= HandleTouchHandler;
                    break;
                case OVRTouchpad.TouchEvent.Left:
                    break;
                case OVRTouchpad.TouchEvent.Right:
                    break;
                case OVRTouchpad.TouchEvent.Up:
                    break;
                case OVRTouchpad.TouchEvent.Down:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}