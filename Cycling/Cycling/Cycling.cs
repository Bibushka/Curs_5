using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cycling
{

    public struct Cyclist
    {
        public string name;
        public double diametre;
        public double[] rotationsPerSecond;


        public Cyclist(string name, double diametre, double [] numberOfRotations)
        {
            this.name = name;
            this.diametre = diametre;
            this.rotationsPerSecond = numberOfRotations;
        }
    }

    [TestClass]
    public class Cycling
    {
        [TestMethod]
        public void GetDistance()
        {
            var steve = new Cyclist ("steve", 1, new double [] { 23, 26, 27 });
            var harvey = new Cyclist("Harvey", 1.2, new double[] { 25, 26, 21 });
            var john = new Cyclist ("John", 1.5, new double [] { 24, 26, 29 });
            var cyclingTeam = new Cyclist[] { steve, harvey, john };
            CollectionAssert.AreEqual(new double[] { 238.64000000000002, 271.296, 372.09000000000003 }, CalculateDistance(cyclingTeam));
        }

        public double[] CalculateDistance(Cyclist[] cyclingTeam)
        {
            double[] distance = new double [3];
            for (int i = 0; i < cyclingTeam.Length; i++)
                distance[i] = CalculateDistancePerCyclist(cyclingTeam[i].rotationsPerSecond, cyclingTeam[i].diametre);
            return distance;
        }

        public double CalculateDistancePerCyclist(double [] rotationsPerSecond, double diametre)
        {
            double distance = 0;
            for (int i = 0; i < rotationsPerSecond.Length; i++)
                distance += 3.14 * rotationsPerSecond[i] * diametre;
            return distance;
        }
    }
}
