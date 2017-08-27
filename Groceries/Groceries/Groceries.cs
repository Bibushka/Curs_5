using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Groceries
{

    public struct Product
    {
        public string name;
        public double price;

        public Product(string name, double price)
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
            var groceriesCart = new Product[] { new Product("Bread", 4), new Product("Wine", 56) };
            Assert.AreEqual(0, CalculateTotal(groceriesCart));
        }

        [TestMethod]
        public void GetTotalPrice()
        {
            var groceriesCart = new Product[] { new Product("Bread", 4), new Product("Wine", 56) };
            Assert.AreEqual(60, CalculateTotal(groceriesCart));
        }

        [TestMethod]
        public void GetLesserPriceFail()
        {
            var groceriesCart = new Product[]
            { new Product("Bread", 4), new Product("Wine", 56), new Product("Milk", 8) };
            Assert.AreEqual(8, CalculateSmallerPrice(groceriesCart));
        }

        [TestMethod]
        public void GetLesserPrice()
        {
            var groceriesCart = new Product[]
            { new Product("Bread", 4), new Product("Wine", 56), new Product("Milk", 8) };
            Assert.AreEqual(4, CalculateSmallerPrice(groceriesCart));
        }

        [TestMethod]
        public void EliminateTheMostExpensiveItem()
        {
            var groceriesCart = new Product[]
            { new Product("Bread", 9), new Product("Wine", 56), new Product("Milk", 8) };
            EliminateExpensiveItem(groceriesCart);
            Assert.AreEqual(0, CalculateGreaterPrice(groceriesCart));
        }

        [TestMethod]
        public void AddProduceToCart()
        {
            var groceriesCart = new Product[]
            { new Product("Bread", 4), new Product("Wine", 56), new Product("Milk", 8) };

        }

        public double CalculateTotal(Product[] groceriesCart)
        {
            double total = 0;
            for (int i = 0; i < groceriesCart.Length; i++)
                total += groceriesCart[i].price;
            return total;
        }

        public double CalculateSmallerPrice(Product[] groceriesCart)
        {
            double smallestPrice = groceriesCart[0].price;
            for (int i = 1; i < groceriesCart.Length; i++)
                if (groceriesCart[i].price < smallestPrice)
                    smallestPrice = groceriesCart[i].price;
            return smallestPrice;
        }

        public int CalculateGreaterPrice(Product[] groceriesCart)
        {
            double greaterPrice = groceriesCart[0].price;
            int index = 0;
            for (int i = 1; i < groceriesCart.Length; i++)
                if (groceriesCart[i].price > greaterPrice)
                {
                    greaterPrice = groceriesCart[i].price;
                    index = i;
                }
            return index;
        }

        public void EliminateExpensiveItem(Product[] groceriesCart)
        {
            int index = CalculateGreaterPrice(groceriesCart);
            for (int i = index; i < groceriesCart.Length-1; i++)
                groceriesCart[i] = groceriesCart[i + 1];
            Array.Resize(ref groceriesCart, groceriesCart.Length-1);
        }
    }
}
