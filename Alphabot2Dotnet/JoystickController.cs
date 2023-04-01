using System;
using System.Device.Gpio;

namespace Alphabot2
{
    public class JoysticController
    {

        private GpioController _gpioController;

        const int Ctr = 7;
        const int A = 8;
        const int B = 9;
        const int C = 10;
        const int D = 11;

        public event EventHandler CenterPressed;
        public event EventHandler APressed;
        public event EventHandler BPressed;
        public event EventHandler CPressed;
        public event EventHandler DPressed;



        public JoysticController(GpioController controller)
        {

            if (controller == null)
                throw new ArgumentNullException("Please supply a GpioController.");

            _gpioController = controller;


            _gpioController.OpenPin(Ctr, PinMode.InputPullUp);
            _gpioController.OpenPin(A, PinMode.InputPullUp);
            _gpioController.OpenPin(B, PinMode.InputPullUp);
            _gpioController.OpenPin(C, PinMode.InputPullUp);
            _gpioController.OpenPin(D, PinMode.InputPullUp);

            _gpioController.RegisterCallbackForPinValueChangedEvent(Ctr, PinEventTypes.Falling, CenterPressedCallback);
            _gpioController.RegisterCallbackForPinValueChangedEvent(A, PinEventTypes.Falling, APressedCallback);
            _gpioController.RegisterCallbackForPinValueChangedEvent(B, PinEventTypes.Falling, BPressedCallback);
            _gpioController.RegisterCallbackForPinValueChangedEvent(C, PinEventTypes.Falling, CPressedCallback);
            _gpioController.RegisterCallbackForPinValueChangedEvent(D, PinEventTypes.Falling, DPressedCallback);
        }

        private void APressedCallback(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            APressed?.Invoke(this, pinValueChangedEventArgs);
        }

        private void BPressedCallback(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            BPressed?.Invoke(this, pinValueChangedEventArgs);
        }


        private void CPressedCallback(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            CPressed?.Invoke(this, pinValueChangedEventArgs);
        }

        private void DPressedCallback(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            DPressed?.Invoke(this, pinValueChangedEventArgs);
        }

        private void CenterPressedCallback(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
            CenterPressed?.Invoke(this, pinValueChangedEventArgs);
        }
    }
}

