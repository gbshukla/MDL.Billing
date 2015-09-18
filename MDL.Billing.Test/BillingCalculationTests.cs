using System;
using Xunit;

namespace MDL.Billing.Test
{
    public class BillingCalculationTests
    {
        [Fact]
        public void EmployeeGets30PercentDiscount()
        {

        }

        [Fact]
        public void AffiliateGets10PercentDiscount()
        {
        }

        [Fact]
        public void TwoYearOldCustomerGets10PercentDiscount()
        {
        }

        [Theory]
        [InlineData(990, 45)]
        [InlineData(100, 5)]
        [InlineData(99, 0)]
        [InlineData(500, 25)]
        public void USD5DiscountOnEvery100USDPurchase(double billAmount, int discount)
        {
        }

        [Fact]
        public void NoPercentageDiscountOnGroceries()
        {
        }

        [Fact]
        public void OnlyOnePercentageDiscountApplicable()
        {
        }

    }
}
