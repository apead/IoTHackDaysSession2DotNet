using System;
using System.Device.Gpio;
using System.Threading;
using System.Threading.Tasks;
using Alphabot2;

namespace CognitiveBots
{

    class Program
    {
        static async Task Main(string[] args)
        {
            var gpioController = new GpioController(PinNumberingScheme.Logical);

            var buzzer = new Buzzer(gpioController);
       /*     buzzer.Buzz();
            Thread.Sleep(1000);
            buzzer.Silence();

            var collision = new CollisionDetector(gpioController);
            collision.LeftCollision += Collission_Left;
            collision.RightCollision += Collission_Right;

            var joystickController = new JoystickMotorController(gpioController);
*/
            var rainbowLeds = new RainbowLeds();
            rainbowLeds.Run(1000);

            Thread.Sleep(Timeout.Infinite);

        }

        private static void Collission_Left(object sender, EventArgs e)
        {
            Console.WriteLine("Left Collision");
        }

        private static void Collission_Right(object sender, EventArgs e)
        {
            Console.WriteLine("Right Collision");
        }

    }
}