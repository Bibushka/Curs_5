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

        public void MovePoint(int x, int y)
        {
            this.x += x;
            this.y += y;
        }
    }

    [TestClass]
    public class Points
    {
        [TestMethod]
        public void TestMethod1()
        {
            var initialPoint = new Point (0, 0);
            int[,] trail = new int[,] { { -1, 1 }, { 1, 1 } };
            Assert.IsTrue(CheckIntersection(initialPoint, trail));
        }

        public bool CheckIntersection(Point initialPoint, int[,] trail)
        {
            int[,] trailToCheck = new int[trail.Length+1, 2];
            trailToCheck[0,0] = initialPoint.x;
            trailToCheck[0,1] = initialPoint.y;
            for (int i = 0; i < trail.Length; i++)
            {
                initialPoint.MovePoint(trail[i, 0], trail[i, 1]);
                trailToCheck[i, 0] = initialPoint.x;
                trailToCheck[i, 1] = initialPoint.y;
            }
            return CheckTrail(trailToCheck);
        }

        public bool CheckTrail(int[,] trailToCheck)
        {
            int ok = 0;
            for (int i = 0; i < trailToCheck.Length - 1; i++)
                for (int j = 1; j < trailToCheck.Length; i++)
                    if (trailToCheck[i, 0] == trailToCheck[j, 0] && trailToCheck[i, 1] == trailToCheck[j, 1])
                        ok = 1;
            return (ok == 1);
        }
    }
}
