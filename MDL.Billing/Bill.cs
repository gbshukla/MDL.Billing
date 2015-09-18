using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int percentageElligibility = 0;

            // By default the discount percentage is 0.
            double totalPercentageBasedDiscount = 0;

            switch (User.Type)
            {
                case UserType.Employee:
                    percentageElligibility = 30;
                    break;
                case UserType.Affiliate:
                    percentageElligibility = 10;
                    break;

                default:
                    percentageElligibility = IsUserElligibleForFivePercent(User.JoiningDate) ? 5 : 0;
                    break;
                            

            }

            return totalPercentageBasedDiscount;                                   
        }

        private bool IsUserElligibleForFivePercent(DateTime joiningDate)
        {
            throw new NotImplementedException();
        }

        private int GetFixedDiscount()
        {
            throw new NotImplementedException();
        }
    }
}
