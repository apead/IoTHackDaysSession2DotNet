using rpi_ws281x;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Alphabot2
{
    public class RainbowLeds
    {
        private static int colorOffset = 0;

        public void Run(int colorCycles)
        {
            int cycleIndex = 0;

            var settings = Settings.CreateDefaultSettings();


            var controller = settings.AddController(16, Pin.Gpio18, StripType.WS2812_STRIP, ControllerType.PWM0, 255, false);

            using (var rpi = new WS281x(settings))
            {
                var colors = GetAnimationColors();
                while (cycleIndex < colorCycles)
                {

                    for (int i = 0; i <= controller.LEDCount - 1; i++)
                    {
                        var colorIndex = (i + colorOffset) % colors.Count;
                        controller.SetLED(i, colors[colorIndex]);
                    }

                    rpi.Render();

                    if (colorOffset == int.MaxValue)
                    {
                        colorOffset = 0;
                    }
                    colorOffset++;
                    Thread.Sleep(50);

                    cycleIndex++;
                }
            }
        }

        private static List<Color> GetAnimationColors()
        {
            var result = new List<Color>();

            result.Add(Color.FromArgb(0x201000));
            result.Add(Color.FromArgb(0x202000));
            result.Add(Color.Green);
            result.Add(Color.FromArgb(0x002020));
            result.Add(Color.Blue);
            result.Add(Color.FromArgb(0x100010));
            result.Add(Color.FromArgb(0x200010));

            return result;
        }
    }
}