using System;
using System.Device.Gpio;

namespace Alphabot2
{
    public class JoystickMotorController
    {
        private MotorControl _alphabot;
        private GpioController _gpioController;

        const int Ctr = 7;
        const int A = 8;
        const int B = 9;
        const int C = 10;
        const int D = 11;

        public JoystickMotorController(GpioController controller)
        {
            _alphabot = new MotorControl();

            if (controller == null)
                throw new ArgumentNullException("Please supply a GpioController.");

            _gpioController = controller;


            _gpioController.OpenPin(Ctr, PinMode.InputPullUp);
            _gpioController.OpenPin(A, PinMode.InputPullUp);
            _gpioController.OpenPin(B, PinMode.InputPullUp);
            _gpioController.OpenPin(C, PinMode.InputPullUp);
            _gpioController.OpenPin(D, PinMode.InputPullUp);

            _gpioController.RegisterCallbackForPinValueChangedEvent(Ctr, PinEventTypes.Falling, CenterPressed);
            _gpioController.RegisterCallbackForPinValueChangedEvent(A, PinEventTypes.Falling, APressed);
            _gpioController.RegisterCallbackForPinValueChangedEvent(B, PinEventTypes.Falling, BPressed);
            _gpioController.RegisterCallbackForPinValueChangedEvent(C, PinEventTypes.Falling, CPressed);
            _gpioController.RegisterCallbackForPinValueChangedEvent(D, PinEventTypes.Falling, DPressed);
        }

        private void APressed(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            _alphabot.Forward();
        }

        private void BPressed(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            _alphabot.Right();
        }


        private void CPressed(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            _alphabot.Left();
        }

        private void DPressed(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            _alphabot.Backwards();
        }
        private void CenterPressed(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            _alphabot.Stop();
        }
    }
}

