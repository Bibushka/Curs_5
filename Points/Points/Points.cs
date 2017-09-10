using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Points
{

    public struct Point
    {
        public int[,] trail;
        public int[,] trailToCheck;
        public Point(int[,] trail)
        {
            this.trail = trail;
            this.trailToCheck = new int[trail.Length, 2];
            for (int i = 0; i < trail.Length; i++)
            {
                trailToCheck[i, 0] += trail[i, 0];
                trailToCheck[i, 1] += trail[i, 1];
            }
        }
    }
    
    [TestClass]
    public class Points
    {
        [TestMethod]
        public void TestMethod1()
        {
            var initialPoint = new Point (new int[,] { { -1, 1 }, { 1, 1 } });
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
