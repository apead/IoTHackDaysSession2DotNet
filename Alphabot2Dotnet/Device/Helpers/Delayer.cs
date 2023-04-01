using System.Diagnostics;
using System.Threading;

namespace Alphabot2.Device.Helpers
{
    public static class Delayer
    {
        public static void DelayMicroseconds(int microseconds, bool allowThreadYield)
        {
            long start = Stopwatch.GetTimestamp();
            ulong minimumTicks = (ulong)(microseconds * Stopwatch.Frequency / 1_000_000);

            if (!allowThreadYield)
            {
                do
                {
                    Thread.SpinWait(1);
                }
                while ((ulong)(Stopwatch.GetTimestamp() - start) < minimumTicks);
            }
            else
            {
                SpinWait spinWait = new SpinWait();
                do
                {
                    spinWait.SpinOnce();
                }
                while ((ulong)(Stopwatch.GetTimestamp() - start) < minimumTicks);
            }
        }

        public static void DelayMilliseconds(int milliseconds, bool allowThreadYield)
        {
            DelayMicroseconds(milliseconds * 1000, allowThreadYield);
        }
    }
}
