using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlarmClock
{

    [Flags]
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

    public struct Time
    {
        public Day day;
        public int hour;
        public int minute;

        public Time(Day day, int hour, int minute)
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
        public void CheckWeekDayAlarm()
        {
            var weekDayAlarm = 
                new Alarm(Day.Monday|Day.Tuesday|Day.Wednesday|Day.Thursday|Day.Friday, 6, 0);
            var currentTime = new Time(Day.Tuesday, 6, 0);
            Assert.IsTrue(CheckAlarm(weekDayAlarm, currentTime));
        }

        [TestMethod]
        public void CheckWeekendDayAlarm()
        {
            var weekendDayAlarm = new Alarm(Day.Saturday|Day.Sunday, 8, 0);
            var currentTime = new Time(Day.Saturday, 8, 0);
            Assert.IsTrue(CheckAlarm(weekendDayAlarm, currentTime));
        }

        [TestMethod]
        public void CheckWeekendDayAlarmFail()
        {
            var weekendDayAlarm = new Alarm(Day.Saturday | Day.Sunday, 8, 0);
            var currentTime = new Time(Day.Monday, 8, 0);
            Assert.IsTrue(CheckAlarm(weekendDayAlarm, currentTime));
        }


        public bool CheckAlarm(Alarm setAlarm, Time currentTime)
        {
            if (setAlarm.day != currentTime.day && setAlarm.hour == currentTime.hour &&
                setAlarm.minute == currentTime.minute)
                return true;
            return false;
        }
    }
}
