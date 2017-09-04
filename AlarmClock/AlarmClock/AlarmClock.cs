using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlarmClock
{
    public enum Day
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
    }

    public struct Alarm
    {
        public Day day;
        public int hour;
        public int minute;

        public Alarm(Day day, int hour, int minute)
        {
            this.day = day;
            this.hour = hour;
            this.minute = minute;
        }
    }

    [TestClass]
    public class AlarmClock
    {
        [TestMethod]
        public void CheckMondayAlarmFail()
        {
            var mondayAlarm = new Alarm (Day.Monday, 6, 1);
            Assert.IsFalse(CheckWeekDayAlarm(mondayAlarm));
        }

        [TestMethod]
        public void CheckMondayAlarm()
        {
            var mondayAlarm = new Alarm(Day.Monday, 6, 0);
            Assert.IsTrue(CheckWeekDayAlarm(mondayAlarm));
        }

        public bool CheckWeekDayAlarm(Alarm setAlarm)
        {
            if (setAlarm.day == Day.Monday && setAlarm.hour == 6 && setAlarm.minute == 0)
                return true;
            return false;
        }
    }
}
