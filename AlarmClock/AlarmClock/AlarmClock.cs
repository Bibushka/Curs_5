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

        [TestMethod]
        public void CheckWeekDayAlarm()
        {
            var weekDayAlarm = new Alarm(Day.Thursday, 6, 0);
            Assert.IsTrue(CheckWeekDayAlarm(weekDayAlarm));
        }

        [TestMethod]
        public void CheckWeekendDayAlarm()
        {
            var weekendDayAlarm = new Alarm(Day.Saturday, 8, 0);
            Assert.IsTrue(CheckWeekendDayAlarm(weekendDayAlarm));
        }

        public bool CheckWeekDayAlarm(Alarm setAlarm)
        {
            if (CheckDay(setAlarm.day) && setAlarm.hour == 6 && setAlarm.minute == 0)
                return true;
            return false;
        }

        public bool CheckWeekendDayAlarm(Alarm setAlarm)
        {
            if (!CheckDay(setAlarm.day) && setAlarm.hour == 8 && setAlarm.minute == 0)
                return true;
            return false;
        }

        public bool CheckDay(Day day)
        {
            if (day == Day.Saturday || day == Day.Sunday)
                return false;
            return true;
        }
    }
}
