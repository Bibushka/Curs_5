using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlarmClock
{
    enum Days
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
    }

    public struct Time
    {
        public int hour;
        public int minute;

        public Time(int hour, int minute)
        {
            this.hour = hour;
            this.minute = minute;
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
