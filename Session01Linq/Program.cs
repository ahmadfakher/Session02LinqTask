using C48_Session01LinqG01.DataSources;
using C48_Session01LinqG01.Models;

namespace Session01Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> ProductList = Source.ProductList;
            List<Customer> CustomerList = Source.CustomerList;

            // Q1
            Console.WriteLine("----- Q1 ------");
            var top3expensiveProducts = ProductList.OrderByDescending(p => p.UnitPrice).Take(3);
            PrintCollection(top3expensiveProducts);

            // Q2
            Console.WriteLine("----- Q2 ------");
            var Page25Products = ProductList.Skip(5).Take(5);
            PrintCollection(Page25Products);

            // Q3
            Console.WriteLine("----- Q3 ------");
            var TakeProductsLessThan25 = ProductList.TakeWhile(p => p.UnitPrice < 25);
            PrintCollection(TakeProductsLessThan25);

            // Q4
            Console.WriteLine("----- Q4 ------");
            bool allSeafoodInStock = ProductList.Where(p => p.Category == "Seafood").All(p => p.UnitsInStock > 0);
            Console.WriteLine("All seafood products are in stock: " + allSeafoodInStock);

            // Q5
            Console.WriteLine("----- Q5 ------");
            int[] ids = { 3, 9, 13, 18 };
            var idListContain9 = ids.FirstOrDefault(id => id == 9);
            Console.WriteLine("Contains 9:" + idListContain9);

            // Q6
            Console.WriteLine("----- Q6 ------");
            var groupedProducts = ProductList.GroupBy(p => p.Category);
            foreach (var categoryGroup in groupedProducts)
            {
                Console.WriteLine($"Category: {categoryGroup.Key}");
                Console.WriteLine($"Product Count: {categoryGroup.Count()}");
            }

            // Q7
            Console.WriteLine("----- Q7 ------");
            var productNamesByCategory = ProductList.GroupBy(product => product.Category)
            .Select(categoryGroup => new
                {
                    Category = categoryGroup.Key,
                    ProductNames = categoryGroup.Select(
                        product => product.ProductName
                    )
                });
            foreach (var categoryGroup in productNamesByCategory)
            {
                Console.WriteLine(categoryGroup.Category);
                Console.WriteLine("--------------------");
                foreach (var productName in categoryGroup.ProductNames)
                {
                    Console.WriteLine(productName);
                }
                Console.WriteLine("--------------------");
            }

            // Q8
            Console.WriteLine("----- Q8 ------");
            var categoriesWithMoreThanThreeProducts = ProductList.GroupBy(product => product.Category)
            .Where(categoryGroup => categoryGroup.Count() > 3).Select(categoryGroup => categoryGroup.Key);
            PrintCollection(categoriesWithMoreThanThreeProducts);

            // Q9
            Console.WriteLine("----- Q9 ------");
            var customersByCountry = CustomerList.GroupBy(customer => customer.Country)
            .Select(countryGroup => new
            {
                Country = countryGroup.Key,
                Count = countryGroup.Count(),
                TotalOrderValue = countryGroup.Sum(
                    customer => customer.Orders.Sum(order => order.Total)
                )
            });
            PrintCollection(customersByCountry);

            // Q10
            Console.WriteLine("----- Q10 ------");
            var totalUnitsInStock = ProductList.Sum(product => product.UnitsInStock);
            Console.WriteLine($"Total units in stock: {totalUnitsInStock}");

            // Q11
            Console.WriteLine("----- Q11 ------");
            var cheapestPrice = ProductList.Min(product => product.UnitPrice);
            Console.WriteLine($"Cheapest: {cheapestPrice}");
            var mostExpensivePrice = ProductList.Max(product => product.UnitPrice);
            Console.WriteLine($"Most Expensive: {mostExpensivePrice}");

            // Q12
            Console.WriteLine("----- Q12 ------");
            var distinctCategories = ProductList.Select(product => product.Category).Distinct();
            PrintCollection(distinctCategories);

            // Q13
            Console.WriteLine("----- Q13 ------");
            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };
            var idsOnlyInSetA = setA.Except(setB);
            PrintCollection(idsOnlyInSetA);

            // Q14
            Console.WriteLine("----- Q14 ------");
            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };
            var countriesOnlyInList1 = list1.Except(list2, StringComparer.OrdinalIgnoreCase);
            PrintCollection(countriesOnlyInList1);

            // Q15
            Console.WriteLine("----- Q15 ------");
            Dictionary<int, Product> productsDictionary = ProductList.ToDictionary(product => product.ProductID);
            Product productWithId18 = productsDictionary[18];
            Console.WriteLine(productWithId18);

            // Q16
            Console.WriteLine("----- Q16 ------");
            var firstExpensiveProduct = ProductList.First(product => product.UnitPrice > 50);
            Console.WriteLine(firstExpensiveProduct);

            // Q17
            Console.WriteLine("----- Q17 ------");
            var firstProductAbove500 = ProductList.FirstOrDefault(product => product.UnitPrice > 500);
            Console.WriteLine($"First product above 500: {firstProductAbove500}");

            // Q18
            Console.WriteLine("----- Q18 ------");
            var multiplicationTableRow = Enumerable.Range(1, 10).Select(number => $"7 x {number} = {7 * number}");
            foreach (var equation in multiplicationTableRow)
            {
                Console.WriteLine(equation);
            }

            // Q19
            Console.WriteLine("----- Q19 ------");
            var evenNumbers = Enumerable.Range(1, 30).Where(number => number % 2 == 0);
            Console.WriteLine("Even Numbers (till 30)");
            PrintCollection(evenNumbers);

            // Q20
            Console.WriteLine("----- Q20 ------");
            var combinedNames = ProductList.Take(3).Select(product => product.ProductName)
            .Concat(CustomerList.Take(3).Select(customer => customer.CustomerName));
            PrintCollection(combinedNames);

            // Q21
            Console.WriteLine("----- Q21 ------");
            var productCustomerPairs = ProductList.Zip(CustomerList,
                (product, customer) =>$"{product.ProductName} sold to {customer.CustomerName}"
            );
            PrintCollection(productCustomerPairs);
        }

        static void PrintCollection<T>(IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}
