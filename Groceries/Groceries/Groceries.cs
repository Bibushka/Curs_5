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
            Assert.AreEqual(1, GetIndexForLessExpensiveProduct(groceriesCart));
        }

        [TestMethod]
        public void GetLesserPrice()
        {
            var groceriesCart = new Product[]
            { new Product("Bread", 4), new Product("Wine", 56), new Product("Milk", 8) };
            Assert.AreEqual(0, GetIndexForLessExpensiveProduct(groceriesCart));
        }

        [TestMethod]
        public void EliminateTheMostExpensiveItem()
        {
            var groceriesCart = new Product[]
            { new Product("Bread", 4), new Product("Wine", 56), new Product("Milk", 8) };
            Product[] newGroceriesCart = (Product[]) EliminateExpensiveItem(groceriesCart);
            Assert.AreEqual(2, newGroceriesCart.Length);
            Assert.AreEqual(1, GetIndexForTheMostExpensiveProduct(newGroceriesCart));
        }

        [TestMethod]
        public void AddProductToCart()
        {
            var groceriesCart = new Product[]
            {new Product("Bread", 4), new Product("Wine", 56), new Product("Milk", 8) };
            Product[] newGroceriesCart = (Product[]) AddNewProduct("Sugar", 6, groceriesCart);
            Assert.AreEqual(4, newGroceriesCart.Length);
        }

        [TestMethod]
        public void GetMidPriceInCart()
        {
            var groceriesCart = new Product[]
            {new Product("Bread", 4), new Product("Wine", 56), new Product("Milk", 9) };
            Assert.AreEqual(23, CalculateMidPriceInCart(groceriesCart));
        }

        public double CalculateTotal(Product[] groceriesCart)
        {
            double total = 0;
            for (int i = 0; i < groceriesCart.Length; i++)
                total += groceriesCart[i].price;
            return total;
        }

        public int GetIndexForLessExpensiveProduct(Product[] groceriesCart)
        {
            double smallestPrice = 0;
            int index = 0;
            for (int i = 1; i < groceriesCart.Length; i++)
                if (groceriesCart[i].price < smallestPrice)
                {
                    smallestPrice = groceriesCart[i].price;
                    index = i;
                }
            return index;
        }

        public int GetIndexForTheMostExpensiveProduct(Product[] groceriesCart)
        {
            double greaterPrice = 0;
            int index = 0;
            for (int i = 1; i < groceriesCart.Length; i++)
                if (groceriesCart[i].price > greaterPrice)
                {
                    greaterPrice = groceriesCart[i].price;
                    index = i;
                }
            return index;
        }

        public Array EliminateExpensiveItem(Product[] groceriesCart)
        {
            for (int i = GetIndexForTheMostExpensiveProduct(groceriesCart); i < groceriesCart.Length-1; i++)
                groceriesCart[i] = groceriesCart[i + 1];
            Array.Resize(ref groceriesCart, groceriesCart.Length-1);
            return groceriesCart;
        }

        public Array AddNewProduct(string name, int price, Product[] groceriesCart)
        {
            Array.Resize(ref groceriesCart, groceriesCart.Length + 1);
            groceriesCart[groceriesCart.Length - 1] = new Product(name, price);
            return groceriesCart;
        }

        public double CalculateMidPriceInCart(Product[] groceriesCart)
        {
            return CalculateTotal(groceriesCart) / groceriesCart.Length;
        }
    }
}
