using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Groceries
{

    public struct Produse
    {
        public string name;
        public double price;

        public Produse(string name, double price)
        {
            this.name = name;
            this.price = price;
        }
    }

    [TestClass]
    public class Groceries
    {
        [TestMethod]
        public void GetTotalPriceFail()
        {
            var groceriesCart = new Produse[] { new Produse("Bread", 4), new Produse("Wine", 56) };
            Assert.AreEqual(0, CalculateTotal(groceriesCart));
        }

        [TestMethod]
        public void GetTotalPrice()
        {
            var groceriesCart = new Produse[] { new Produse("Bread", 4), new Produse("Wine", 56) };
            Assert.AreEqual(60, CalculateTotal(groceriesCart));
        }

        [TestMethod]
        public void GetLesserPriceFail()
        {
            var groceriesCart = new Produse[]
            { new Produse("Bread", 4), new Produse("Wine", 56), new Produse("Milk", 8) };
            Assert.AreEqual(8, CalculateSmallerPrice(groceriesCart));
        }

        [TestMethod]
        public void GetLesserPrice()
        {
            var groceriesCart = new Produse[]
            { new Produse("Bread", 4), new Produse("Wine", 56), new Produse("Milk", 8) };
            Assert.AreEqual(4, CalculateSmallerPrice(groceriesCart));
        }

        public double CalculateTotal(Produse[] groceriesCart)
        {
            double total = 0;
            for (int i = 0; i < groceriesCart.Length; i++)
                total += groceriesCart[i].price;
            return total;
        }

        public double CalculateSmallerPrice(Produse[] groceriesCart)
        {
            double smallestPrice = groceriesCart[0].price;
            for (int i = 1; i < groceriesCart.Length; i++)
                if (groceriesCart[i].price < smallestPrice)
                    smallestPrice = groceriesCart[i].price;
            return smallestPrice;
        }
    }
}
