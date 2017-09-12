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
            Assert.IsTrue(CheckIntersection(trail));
        }

        public bool CheckIntersection(Point[] trail)
        {
            var trailToCheck = new Point[trail.Length];
            for (int i = 1; i < trailToCheck.Length; i++)
            {
                trailToCheck[i].x = trailToCheck[i - 1].x + trail[i].x;
                trailToCheck[i].y = trailToCheck[i - 1].y + trail[i].y;
            }
            return CheckTrail(trailToCheck);
        }

        public bool CheckTrail(Point[] trailToCheck)
        {
            for (int i = 0; i < trailToCheck.Length - 1; i++)
                for (int j = 1; j < trailToCheck.Length; j++)
                    if ((trailToCheck[i].x == trailToCheck[j].x) &&
                        (trailToCheck[i].y == trailToCheck[i].y))
                        return true;
            return false;
        }
    }
}
