using System;
using System.Device.Gpio;
using System.Threading.Tasks;

namespace Alphabot2
{
    public class Buzzer
    {
        const int BuzzerPin = 4;
        private GpioController _gpioController;


        public Buzzer(GpioController controller)
        {
            if (controller == null)
                throw new ArgumentNullException("Please supply a GpioController.");

            _gpioController = controller;

            _gpioController.OpenPin(BuzzerPin, PinMode.Output);
        }

        public void Buzz()
        {
            _gpioController.Write(BuzzerPin, PinValue.High);
        }

        public void Silence()
        {
            _gpioController.Write(BuzzerPin, PinValue.Low);
        }

        public async Task Alarm()
        {
            await Task.Run(async () =>
            {
                while (true)
                {

                    Buzz();

                    await Task.Delay(500);

                    Silence();

                    await Task.Delay(500);
                }
            }
            );
        }
    }
}
