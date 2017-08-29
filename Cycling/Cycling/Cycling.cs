using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cycling
{

    public struct Cyclists
    {
        public double diametre;
        public double numberOfRotations;

        public Cyclists(double diametre, double numberOfRotations)
        {
            this.diametre = diametre;
            this.numberOfRotations = numberOfRotations;
        }
    }

    [TestClass]
    public class Cycling
    {
        [TestMethod]
        public void GetDistance()
        {
            var cyclingTeam = new Cyclists[]
            {new Cyclists (1, 200), new Cyclists(1.2, 215), new Cyclists(1.5, 220)};
            CollectionAssert.AreEqual(new double[] { 628, 810.12, 1036.2 }, CalculateDistance(cyclingTeam));
        }

        public Array CalculateDistance(Cyclists[] cyclingTeam)
        {
            double[] distance = new double [cyclingTeam.Length];
            for (int i = 0; i < distance.Length; i++)
                distance[i] = 3.14 * cyclingTeam[i].diametre * cyclingTeam[i].numberOfRotations;
            return distance;
        }
    }
}
