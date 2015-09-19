
namespace MDL.Billing
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    #endregion Namespaces

    /// <summary>
    /// Common utility functions for billing.  
    /// The common utilities are abstracted to avoid any code change in case of the business rules are changed 
    /// or new rules are introduced e.g., start giving 7% to users older than 5 years.
    /// Its a singleton to avoid multiple instances creation and destroying.
    /// </summary>
    public sealed class CommonFunctions
    {
        #region SingletonCode

        private static readonly Lazy<CommonFunctions> lazy =
            new Lazy<CommonFunctions>(() => new CommonFunctions());

        public static CommonFunctions Instance { get { return lazy.Value; } }

        private CommonFunctions()
        {
        }
        #endregion SingletonCode

        #region Utilities

        /// <summary>
        /// Offer $5 for every $100 purchase
        /// </summary>
        /// <param name="totalBillAmount">Total amount without any discount</param>
        /// <returns>Total fixed discount</returns>
        public static double GetFixedDiscount(double totalBillAmount)
        {
            int hundreds = Convert.ToInt32(Math.Floor(totalBillAmount / 100));

            return (hundreds) * 5;
        }

        public static double GetTotalAmountBasedOnPercentageDiscount(List<IProduct> products, IUser user)
        {
            // Initialize with 0 discount
            double totalPercentageDiscount = 0;

            // Get elligibility of percentage discount
            float percentageElligibility = GetPercentageElligibility(user);

            if (percentageElligibility > 0)
            {
                foreach (var product in products)
                {
                    if (product.Type != ProductType.Grocery)
                    {
                        totalPercentageDiscount += ((product.Price * percentageElligibility) / 100);
                    }
                }
            }

            return totalPercentageDiscount;
        }

        #endregion Utilities

        #region PrivateMethods
        /// <summary>
        /// Get elligibility of percentage discount
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static float GetPercentageElligibility(IUser user)
        {
            // Initialize with 0 percent
            int percentageElligibility = 0;

            switch (user.Type)
            {
                // Employee gets 30% discount
                case UserType.Employee:
                    percentageElligibility = 30;
                    break;

                // Affiliate gets 10% discount
                case UserType.Affiliate:
                    percentageElligibility = 10;
                    break;

                // Two years older customer gets 5%
                default:
                    percentageElligibility = IsUserOlderThen(user.JoiningDate, 2) ? 5 : 0;
                    break;
            }

            return percentageElligibility;
        }

        /// <summary>
        /// Utility method that confirms if a date is older than certain years
        /// </summary>
        /// <param name="joiningDate">The date</param>
        /// <param name="yearsToCompare">Years to compare</param>
        /// <returns>Confirmation</returns>
        private static bool IsUserOlderThen(DateTime joiningDate, int yearsToCompare)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            TimeSpan span = DateTime.Now - joiningDate;

            int years = (zeroTime + span).Year - 1;

            return years >= yearsToCompare;
        }
        #endregion PrivateMethods
    }
}