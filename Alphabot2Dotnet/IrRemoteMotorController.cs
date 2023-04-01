using System;
using System.Device.Gpio;

namespace Alphabot2
{
    public class IrRemoteMotorController
    {
        private MotorControl _alphabot;
        private IrRemoteController _irRemoteController;
        private GpioController _gpioController;

        private int _checkCounter = 0;

        public IrRemoteMotorController(GpioController controller)
        {


            if (controller == null)
                throw new ArgumentNullException("Please supply a GpioController.");

            _gpioController = controller;

            _alphabot = new MotorControl();
            _irRemoteController = new IrRemoteController(controller);

            _irRemoteController.RemotePressed += _irRemoteController_RemotePressed;
            //     _irRemoteController.NoKeyPressed += _irRemoteController_NoKeyPressed;

        }

        private void _irRemoteController_NoKeyPressed(object sender, EventArgs e)
        {
            _checkCounter++;
            if (_checkCounter > 20000)
            {
                _checkCounter = 0;
                _alphabot.Stop();
            }

        }

        private void _irRemoteController_RemotePressed(object sender, IrRemotePressedEventArgs e)
        {
            var key = e.KeyValue;

            _checkCounter = 0;

            switch (key)
            {
                case 0x18:
                    _alphabot.Stop();
                    _alphabot.Forward();
                    Console.WriteLine("Forward");
                    break;

                case 0x08:
                    _alphabot.Stop();
                    _alphabot.Left();
                    Console.WriteLine("Left");
                    break;

                case 0x1c:
                    _alphabot.Stop();
                    Console.WriteLine("Stop");
                    break;

                case 0x5a:
                    _alphabot.Stop();
                    _alphabot.Right();
                    Console.WriteLine("Right");
                    break;

                case 0x52:
                    _alphabot.Stop();
                    _alphabot.Backwards();
                    Console.WriteLine("Backwards");
                    break;
            }
        }

        public void Run()
        {
            _irRemoteController.Run();
        }

    }
}
