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

        public double GetDistance(double rotationsPerSecond)
        {
            return 3.14 * rotationsPerSecond * diametre;

        }
    }

    [TestClass]
    public class Cycling
    {
        [TestMethod]
        public void GetDistance()
        {
            var steve = new Cyclist("steve", 1, new double[] { 23, 26, 27 });
            var harvey = new Cyclist("Harvey", 1.2, new double[] { 25, 26, 21 });
            var john = new Cyclist("John", 1.5, new double[] { 24, 26, 29 });
            var cyclingTeam = new Cyclist[] { steve, harvey, john };
            Assert.AreEqual(882.026, CalculateDistance(cyclingTeam),3);
        }

        public double CalculateDistance(Cyclist[] cyclingTeam)
        {
            double distance = 0;
            for (int i = 0; i < cyclingTeam.Length; i++)
                for (int j=0; j < cyclingTeam[i].rotationsPerSecond.Length; j++)
                    distance += cyclingTeam[i].GetDistance(cyclingTeam[i].rotationsPerSecond[j]);
            return distance;
        }
        
    }
}
