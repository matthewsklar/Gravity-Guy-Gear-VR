using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Application : MonoBehaviour
    {
        public void Start()
        {
            OVRTouchpad.TouchHandler += HandleTouchHandler;
        }

        private void HandleTouchHandler(object sender, EventArgs e)
        {
            var touchArgs = (OVRTouchpad.TouchArgs) e;

            switch (touchArgs.TouchType) {
                case OVRTouchpad.TouchEvent.SingleTap:
                    Debug.Log("Single Tap");
                    break;
                case OVRTouchpad.TouchEvent.Left:
                    if (Time.timeScale > 5.0f) Time.timeScale -= 5.0f;
                    break;
                case OVRTouchpad.TouchEvent.Right:
                    if (Time.timeScale < 100.0f) Time.timeScale += 5.0f;
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