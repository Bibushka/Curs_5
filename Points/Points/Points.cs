using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Points
{

    public struct Point
    {
        int x;
        int y;
        public int[,] trail;
        public int[,] trailToCheck;
        public Point(int x, int y, int[,] trail)
        {
            this.x = x;
            this.y = y;
            this.trail = trail;
            this.trailToCheck = new int[trail.Length+1, 2];
            trailToCheck[0, 0] = x;
            trailToCheck[0, 1] = y;
            for (int i = 0; i < trail.Length; i++)
            {
                trailToCheck[i+1, 0] += trail[i, 0];
                trailToCheck[i+1, 1] += trail[i, 1];
            }
        }
    }
    
    [TestClass]
    public class Points
    {
        [TestMethod]
        public void TestMethod1()
        {
            var initialPoint = new Point (0, 0, new int[,] { { -1, 1 }, { 1, 1 } });
            Assert.IsTrue(CheckIntersection(initialPoint));
        }

        public bool CheckIntersection(Point initialPoint)
        {
            int ok = 0;
            for (int i = 0; i < initialPoint.trailToCheck.Length - 1; i++)
                for (int j = 1; j < initialPoint.trailToCheck.Length; i++)
                    if (initialPoint.trailToCheck[i, 0] == initialPoint.trailToCheck[j, 0] && 
                        initialPoint.trailToCheck[i, 1] == initialPoint.trailToCheck[j, 1])
                        ok = 1;
            return (ok == 1);
        }
    }
}
