using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Points
{

    public struct Point
    {
        public int x;
        public int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point Move(Point trailToCheck)
        {
            return new Point ( this.x = this.x + trailToCheck.x, this.y = this.y + trailToCheck.y);
        }

        public bool Check(Point trailToCheckPoint)
        {
            return this.x == trailToCheckPoint.x && this.y == trailToCheckPoint.y;
        }
    }
    
    [TestClass]
    public class Points
    {
        [TestMethod]
        public void CheckIfThereIsAnInterseciom()
        {
            var initialPoint = new Point (0, 0);
            var secondPoint = new Point(-1, 1);
            var thirdPoint = new Point (1, -1);
            var trail = new Point[] {initialPoint, secondPoint, thirdPoint};
            Assert.IsTrue(CheckIntersection(trail));
        }

        [TestMethod]
        public void CheckIfThereIsNotAnInterseciom()
        {
            var initialPoint = new Point(0, 0);
            var secondPoint = new Point(-1, 1);
            var thirdPoint = new Point(1, 1);
            var trail = new Point[] { initialPoint, secondPoint, thirdPoint };
            Assert.IsFalse(CheckIntersection(trail));
        }

        public bool CheckIntersection(Point[] trail)
        {
            var trailToCheck = new Point[trail.Length];
            for (int i = 1; i < trailToCheck.Length; i++)
                trailToCheck[i] = trail[i].Move(trailToCheck[i - 1]);
            return CheckTrail(trailToCheck);
        }

        public bool CheckTrail(Point[] trailToCheck)
        {
            for (int i = 0; i < trailToCheck.Length - 1; i++)
                for (int j = i + 1; j < trailToCheck.Length; j++)
                    if (trailToCheck[i].Check(trailToCheck[j]))
                        return true;
            return false;
        }
    }
}
