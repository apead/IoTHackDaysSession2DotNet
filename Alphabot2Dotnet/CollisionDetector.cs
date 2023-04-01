using System;
using System.Device.Gpio;

namespace Alphabot2
{
    public class CollisionDetector
    {
        private GpioController _gpioController;

        private const int DR = 16;
        private const int DL = 19;

        public event EventHandler RightCollision;
        public event EventHandler LeftCollision;

        public event EventHandler Collision;


        public CollisionDetector(GpioController controller)
        {
            if (controller == null)
                throw new ArgumentNullException("Please supply a GpioController.");

            _gpioController = controller;

            _gpioController.OpenPin(DR, PinMode.InputPullUp);
            _gpioController.OpenPin(DL, PinMode.InputPullUp);

            _gpioController.RegisterCallbackForPinValueChangedEvent(DL, PinEventTypes.Falling, DetectLeftCallback);
            _gpioController.RegisterCallbackForPinValueChangedEvent(DR, PinEventTypes.Falling, DetectRightCallback);

        }

        private void DetectLeftCallback(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            LeftCollision?.Invoke(this, pinValueChangedEventArgs);
            Collision?.Invoke(this, pinValueChangedEventArgs);
        }

        private void DetectRightCallback(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            RightCollision?.Invoke(this, pinValueChangedEventArgs);
            Collision?.Invoke(this, pinValueChangedEventArgs);
        }
    }
}
