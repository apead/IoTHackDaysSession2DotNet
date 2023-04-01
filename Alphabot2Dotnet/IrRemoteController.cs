using System;
using System.Device.Gpio;
using Alphabot2.Device.Helpers;

namespace Alphabot2
{
    public class IrRemotePressedEventArgs : EventArgs
    {
        public int KeyValue { get; set; }

        public IrRemotePressedEventArgs(int keyValue)
        {
            KeyValue = keyValue;
        }

    }

    public class IrRemoteController
    {

        private GpioController _gpioController;

        const int Ir = 17;


        public event EventHandler<IrRemotePressedEventArgs> RemotePressed;
        public event EventHandler NoKeyPressed;

        public IrRemoteController(GpioController controller)
        {

            if (controller == null)
                throw new ArgumentNullException("Please supply a GpioController.");

            _gpioController = controller;


            _gpioController.OpenPin(Ir, PinMode.Input);
        }

        public void Run()
        {
            int key = 0;

            while (true)
            {
                key = GetKey();

                if (key != 0)
                {
                    var args = new IrRemotePressedEventArgs(key);

                    RemotePressed?.Invoke(this, args);
                }
                else
                    NoKeyPressed?.Invoke(this, null);

                Delayer.DelayMilliseconds(250, true);
            }
        }

        private int GetKey()
        {
            if (_gpioController.Read(Ir) == PinValue.Low)
            {
                byte index = 0;
                byte cnt = 0;
                uint count = 0;
                int[] data = { 0, 0, 0, 0 };


                while (_gpioController.Read(Ir) == PinValue.Low && count++ < 200)   //9ms
                {
                    //	count++;
                    Delayer.DelayMicroseconds(60, true);

                }

                count = 0;
                while (_gpioController.Read(Ir) == PinValue.High && count++ < 80)    //4.5ms
                    Delayer.DelayMicroseconds(60, true);

                for (int i = 0; i < 32; i++)
                {
                    count = 0;
                    while (_gpioController.Read(Ir) == PinValue.Low && count++ < 15)  //0.56ms
                        Delayer.DelayMicroseconds(60, true);

                    count = 0;
                    while (_gpioController.Read(Ir) == PinValue.High && count++ < 40)  //0: 0.56ms; 1: 1.69ms
                        Delayer.DelayMicroseconds(60, true);

                    if (count > 20) data[index] |= 1 << cnt;

                    if (cnt == 7)
                    {
                        cnt = 0;
                        index++;
                    }
                    else cnt++;
                }
                if (data[0] + data[1] == 0xFF && data[2] + data[3] == 0xFF)  //check 
                {
                    Console.WriteLine("Data:" + data[2]);

                    return data[2];
                }
                if (data[0] == 0xFF && data[1] == 0xFF && data[2] == 0xFF && data[3] == 0xFF)
                {
                    Console.WriteLine("Repeat");

                    return 0xFF;
                }

            }

            return 0;

        }
    }
}

