using System;
using System.Device.Gpio;
using System.Device.Pwm;
using System.Device.Pwm.Drivers;

namespace Alphabot2
{
    public class MotorControl
    {
        private int _ain1;
        private int _ain2;
        private int _bin1;
        private int _bin2;
        private int _ena;
        private int _enb;
        private double _pa;
        private double _pb;

        private PwmChannel _pwmA;
        private PwmChannel _pwmB;

        private GpioController _controller;

        public MotorControl(int ain1 = 12, int ain2 = 13, int bin1 = 20, int bin2 = 21, int ena = 6, int enb = 26)
        {
            _ain1 = ain1;
            _ain2 = ain2;
            _bin1 = bin1;
            _bin2 = bin2;
            _ena = ena;
            _enb = enb;
            _pa = 0.6;
            _pb = 0.6;



            _controller = new GpioController(PinNumberingScheme.Logical);
            _controller.OpenPin(_ain1, PinMode.Output);
            _controller.OpenPin(_ain2, PinMode.Output);
            _controller.OpenPin(_bin1, PinMode.Output);
            _controller.OpenPin(_bin2, PinMode.Output);
            _pwmA = new SoftwarePwmChannel(_ena, frequency: 500, usePrecisionTimer: true, dutyCycle: _pa, controller: _controller);
            _pwmB = new SoftwarePwmChannel(_enb, frequency: 500, usePrecisionTimer: true, dutyCycle: _pb, controller: _controller);
            _pwmA.Start();
            _pwmB.Start();

            Stop();
        }


        public void Stop()
        {
            _pwmA.DutyCycle = 0;
            _pwmB.DutyCycle = 0;

            _controller.Write(_ain1, PinValue.Low);
            _controller.Write(_ain2, PinValue.Low);
            _controller.Write(_bin1, PinValue.Low);
            _controller.Write(_bin2, PinValue.Low);
        }

        public void Forward()
        {
            _pwmA.DutyCycle = _pa;
            _pwmB.DutyCycle = _pb;
            _controller.Write(_ain1, PinValue.Low);
            _controller.Write(_ain2, PinValue.High);
            _controller.Write(_bin1, PinValue.Low);
            _controller.Write(_bin2, PinValue.High);

            Console.WriteLine("Forward");
        }

        public void Backwards()
        {
            _pwmA.DutyCycle = _pa;
            _pwmB.DutyCycle = _pb;
            _controller.Write(_ain1, PinValue.High);
            _controller.Write(_ain2, PinValue.Low);
            _controller.Write(_bin1, PinValue.High);
            _controller.Write(_bin2, PinValue.Low);

            Console.WriteLine("Backwards");
        }

        public void Left()
        {
            _pwmA.DutyCycle = 0.5;
            _pwmB.DutyCycle = 0.5;
            _controller.Write(_ain1, PinValue.High);
            _controller.Write(_ain2, PinValue.Low);
            _controller.Write(_bin1, PinValue.Low);
            _controller.Write(_bin2, PinValue.High);

            Console.WriteLine("Left");
        }

        public void Right()
        {
            _pwmA.DutyCycle = 0.5;
            _pwmB.DutyCycle = 0.5;
            _controller.Write(_ain1, PinValue.Low);
            _controller.Write(_ain2, PinValue.High);
            _controller.Write(_bin1, PinValue.High);
            _controller.Write(_bin2, PinValue.Low);

            Console.WriteLine("Right");
        }
    }
}
