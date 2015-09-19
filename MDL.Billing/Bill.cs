using System;
using System.Collections.Generic;

namespace MDL.Billing
{
    public interface IBill
    {
        double CalculateNetAmountPayable();
    }

    public class Bill : IBill
    {
        // Dependency  (Bill must have a User and a list of products
        public Bill(User user, List<Product> products)
        {
            this.User = user;
            this.Products = products;
        }

        public User User { get; set; }

        public List<Product> Products { get; set; }

        public double TotalAmount { get; set; }

        //public double PercentageDiscount { get; set; }

        //public double FixedDiscount { get; set; }

        //public bool IsPercentageDiscountCalculated { get; set; }

        public double NetAmount { get; set; }

        public double CalculateNetAmountPayable()
        {
            return TotalAmount - (GetPercentageDiscount() + GetFixedDiscount());
        }

        private double GetPercentageDiscount()
        {
            int percentageElligibility = CommonFunctions.GetPercentageElligibility(User);

            // By default the discount percentage is 0.
            double totalPercentageBasedDiscount = 0;


            // No need to calculate if there is no elligibility.
            if (percentageElligibility != 0)
            {
                foreach (var product in Products)
                {
                    if (product.Type != ProductType.Grocery)
                    {
                        totalPercentageBasedDiscount += product.Price * percentageElligibility;
                    }
                }
            }

            return totalPercentageBasedDiscount;                                   
        }

        //private bool IsUserElligibleForFivePercent(DateTime joiningDate)
        //{
        //    throw new NotImplementedException();
        //}

        private int GetFixedDiscount()
        {
            throw new NotImplementedException();
        }
    }
}
