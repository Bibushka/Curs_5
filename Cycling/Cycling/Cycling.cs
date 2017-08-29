using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cycling
{

    public struct Cyclists
    {
        public string name;
        public double diametre;
        public double numberOfRotations;
        public double[] rotationsPerSecond;

        public Cyclists(string name, double diametre, double numberOfRotations)
        {
            this.name = name;
            this.diametre = diametre;
            this.numberOfRotations = numberOfRotations;
            this.rotationsPerSecond = new double [3];
        }
    }

    [TestClass]
    public class Cycling
    {
        [TestMethod]
        public void GetDistance()
        {
            var cyclingTeam = new Cyclists[]
            {new Cyclists ("Steve", 1, 200, [23, 26, 27 ]), new Cyclists("Harvey", 1.2, 215, { 25, 26, 21 }),
            new Cyclists("John", 1.5, 220, { 24, 26, 29 })};
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
