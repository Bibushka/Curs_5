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

        public double GetDistance()
        {
            double distance = 0;
            for (int i = 0; i < rotationsPerSecond.Length; i++)
                distance += 3.14 * diametre * rotationsPerSecond[i];
            return distance;
        }

        public double GetAverageSpeed()
        {
            double averageSpeed = 0;
            for (int i = 0; i < rotationsPerSecond.Length; i++)
                averageSpeed += rotationsPerSecond[i];
            return averageSpeed / rotationsPerSecond.Length;
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

        [TestMethod]
        public void GetFastestCyclist()
        {
            var steve = new Cyclist("steve", 1, new double[] { 23, 26, 27 });
            var harvey = new Cyclist("Harvey", 1.2, new double[] { 25, 26, 21 });
            var john = new Cyclist("John", 1.5, new double[] { 24, 26, 29 });
            var cyclingTeam = new Cyclist[] { steve, harvey, john };
            var theFastestCyclist = new FastestCyclist();
            theFastestCyclist = FindFastestCyclist(cyclingTeam);
            Assert.AreEqual(new FastestCyclist (2, "John"), theFastestCyclist);
        }

        [TestMethod]
        public void GetBestMedSpeed()
        {
            var steve = new Cyclist("steve", 1, new double[] { 23, 26, 27 });
            var harvey = new Cyclist("Harvey", 1.2, new double[] { 25, 26, 21 });
            var john = new Cyclist("John", 1.5, new double[] { 24, 26, 29 });
            var cyclingTeam = new Cyclist[] { steve, harvey, john };
            Assert.AreEqual(26.33, FindBestAverageSpeed(cyclingTeam), 2);
        }

        public double CalculateDistance(Cyclist[] cyclingTeam)
        {
            double distance = 0;
            for (int i = 0; i < cyclingTeam.Length; i++)
                distance += cyclingTeam[i].GetDistance();
            return distance;
        }

        public FastestCyclist FindFastestCyclist(Cyclist[] cyclingTeam)
        {
            double rotation = 0;
            var theFastestCyclist = new FastestCyclist();
            for (int i = 0; i < cyclingTeam.Length; i++)
                for (int j = 0; j < cyclingTeam[i].rotationsPerSecond.Length; j++)
                    if (cyclingTeam[i].rotationsPerSecond[j] > rotation)
                    {
                        theFastestCyclist.name = cyclingTeam[i].name;
                        theFastestCyclist.index = j;
                        rotation = cyclingTeam[i].rotationsPerSecond[j];
                    }
            return theFastestCyclist;
        }

        public double FindBestAverageSpeed(Cyclist[] cyclingTeam)
        {
            double[] speeds = new double[cyclingTeam.Length];
            for (int i = 0; i < cyclingTeam.Length; i++)
                speeds[i] = cyclingTeam[i].GetAverageSpeed();
            return GetMaxValue(speeds);
        }

        public double GetMaxValue(double[] speeds)
        {
            double maxValue = 0;
            for (int i = 0; i < speeds.Length; i++)
                if (speeds[i] > maxValue)
                    maxValue = speeds[i];
            return maxValue;
        }
    }

    public struct FastestCyclist
    {
        public int index;
        public string name;

        public FastestCyclist(int index, string name)
        {
            this.index = index;
            this.name = name;
        }
    }
}
