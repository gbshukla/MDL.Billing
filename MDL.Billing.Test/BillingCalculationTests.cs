
namespace MDL.Billing.Test
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    using Xunit;
    #endregion Namespaces

    public class BillingCalculationTests
    {
        #region Tests
        [Fact]
        public void EmployeeGets30PercentDiscount()
        {
            IUser user = GenerateEmployee();

            double discount = CommonFunctions.GetPercentageDiscount(GenerateProductListWithoutGrocery(), user);

            // Total price of products are 100 USD.  So, 30% of 100 will be 30 USD.
            Assert.Equal(30, discount);                   
        }

        [Fact]
        public void AffiliateGets10PercentDiscount()
        {
            IUser user = GenerateAffiliate();

            double discount = CommonFunctions.GetPercentageDiscount(GenerateProductListWithoutGrocery(), user);

            // Total price of products are 100 USD.  So, 10% of 100 will be 10 USD.
            Assert.Equal(10, discount);
        }

        [Fact]
        public void TwoYearOldCustomerGets5PercentDiscount()
        {
            IUser user = GenerateUser();
            user.JoiningDate = DateTime.Parse("26-Jan-2010");

            double discount = CommonFunctions.GetPercentageDiscount(GenerateProductListWithoutGrocery(), user);

            // Total price of products are 100 USD.  So, 5% of 100 will be 5 USD.
            Assert.Equal(discount, 5);
        }

        [Theory]
        [InlineData(990, 45)]
        [InlineData(100, 5)]
        [InlineData(99, 0)]
        [InlineData(500, 25)]
        public void USD5DiscountOnEvery100USDPurchase(double billAmount, int discount)
        {
            double fixedDiscount = CommonFunctions.GetFixedDiscount(billAmount);

            Assert.Equal(discount, fixedDiscount);
        }

        [Fact]
        public void NoPercentageDiscountOnGroceries()
        {
            IUser user = GenerateEmployee();

            double discount = CommonFunctions.GetPercentageDiscount(GenerateProductListWithGrocery(), user);

            // Total price of products are 120 USD.  So, 30% of 120 will be 36 USD.
            Assert.NotEqual(36, discount);

            // We remove grocery, the amount becomes 100 and discount becomes 30
            Assert.Equal(30, discount);
        }

        [Fact]
        public void OnlyOnePercentageDiscountApplicable()
        {
            IUser user = GenerateAffiliate();
            user.JoiningDate = DateTime.Parse("26-Jan-2010");

            double discount = CommonFunctions.GetPercentageDiscount(GenerateProductListWithoutGrocery(), user);

            // Total price of products are 100 USD.  So, 10% of 100 will be 10 USD.
            Assert.Equal(10, discount);
        }

        [Fact]
        public void NetAmountPayableForEmployee()
        {
            Bill bill = new Bill(GenerateEmployee(), GenerateProductListWithoutGrocery());

            // 100 - 30 (Percentage Employee Discount) - 5 (fixed discount for 100 USD)
            Assert.Equal(65, bill.CalculateNetAmountPayable());
        }

        [Fact]
        public void NetAmountPayableForAffiliate()
        {
            Bill bill = new Bill(GenerateAffiliate(), GenerateProductListWithoutGrocery());

            // 100 - 10 (Percentage Employee Discount) - 5 (fixed discount for 100 USD)
            Assert.Equal(85, bill.CalculateNetAmountPayable());
        }

        [Fact]
        public void NetAmountPayableForExistingCustomer()
        {
            IUser user = GenerateUser();
            user.JoiningDate = DateTime.Parse("26-Jan-2010");

            Bill bill = new Bill(user, GenerateProductListWithoutGrocery());

            // 100 - 10 (Percentage Employee Discount) - 5 (fixed discount for 100 USD)
            Assert.Equal(90, bill.CalculateNetAmountPayable());
        }

        #endregion Tests

        #region SupportingMethods
        private List<IProduct> GenerateProductListWithoutGrocery()
        {
            List<IProduct> products = new List<IProduct>();

            products.Add(new Product("2323", "Bat", ProductType.Toys, 20));
            products.Add(new Product("2323", "Jeans", ProductType.Clothing, 20));
            products.Add(new Product("2323", "Bluetooth Speaker", ProductType.Electronics, 20));
            products.Add(new Product("2323", "Football", ProductType.Sports, 20));
            products.Add(new Product("2323", "Table", ProductType.Furnuture, 20));

            return products;
        }

        private List<IProduct> GenerateProductListWithGrocery()
        {
            List<IProduct> products = new List<IProduct>();

            products.Add(new Product("2323", "Bat", ProductType.Toys, 20));
            products.Add(new Product("2323", "Jeans", ProductType.Clothing, 20));
            products.Add(new Product("2323", "Bluetooth Speaker", ProductType.Electronics, 20));
            products.Add(new Product("2323", "Football", ProductType.Sports, 20));
            products.Add(new Product("2323", "Cup Cakes", ProductType.Grocery, 20));
            products.Add(new Product("2323", "Table", ProductType.Furnuture, 20));

            return products;
        }

        private IUser GenerateUser()
        {
            return new User("TestUser");
        }

        private IUser GenerateEmployee()
        {
            IUser user = new User("TestUser");
            user.Type = UserType.Employee;

            return user;
        }

        private IUser GenerateAffiliate()
        {
            IUser user = new User("TestUser");
            user.Type = UserType.Affiliate;

            return user;
        }
        #endregion SupportingMethods
    }
}
