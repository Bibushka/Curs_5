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
            this.trailToCheck = new int[trail.Length/2+1, 2];
            trailToCheck[0, 0] = x;
            trailToCheck[0, 1] = y;
            for (int i = 0; i < trail.Length/2; i++)
            {
                trailToCheck[i+1, 0] = trailToCheck[i, 0] + trail[i, 0];
                trailToCheck[i+1, 1] = trailToCheck[i, 1] + trail[i, 1];
            }
        }
    }
    
    [TestClass]
    public class Points
    {
        [TestMethod]
        public void CheckIfThereIsAnInterseciom()
        {
            var initialPoint = new Point (0, 0, new int[,] { { -1, 1 }, { 1, -1 } });
            Assert.IsTrue(CheckIntersection(initialPoint.trailToCheck));
        }

        [TestMethod]
        public void CheckIfThereIsNotAnInterseciom()
        {
            var initialPoint = new Point(0, 0, new int[,] { { -1, 1 }, { 1, 1 } });
            Assert.IsFalse(CheckIntersection(initialPoint.trailToCheck));
        }

        public bool CheckIntersection(int[,] trailToCheck)
        {
            for (int i = 0; i < trailToCheck.Length / 2 - 1; i++)
                for (int j = 1; j < trailToCheck.Length / 2; i++)
                    if (trailToCheck[i, 0] == trailToCheck[j, 0] &&
                        trailToCheck[i, 1] == trailToCheck[j, 1])
                        return true;
            return false;
        }
    }
}
