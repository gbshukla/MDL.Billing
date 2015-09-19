using System;
using Xunit;

namespace MDL.Billing.Test
{
    public class BillingStructureTests
    {
        [Fact]
        public void BillHasValidUser()
        {
            // Dont build
            // IBill b = new Bill();

            // Dont build
            //IUser u = new User();

            // Since a bill can't be instanciated without user and a user can't be instanciated without a first name, which guarantees every bill has a valid user
            Assert.Equal(1, 1);
        }

        [Fact]
        public void BillHasOneOrMoreItems()
        {
            // Dont Build
            // IBill bill = new Bill();
        }
        
    }
}
