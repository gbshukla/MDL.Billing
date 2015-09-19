﻿
namespace MDL.Billing
{
    #region Namespaces
    using System;
    using System.Collections.Generic;
    #endregion Namespaces

    /// <summary>
    /// Common utility functions for billing.  The common utilities are abstracted to avoid any code change in case of the business rules are changed or new rules are introduced e.g., start giving 7% to users older than 5 years.
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

        public static double GetFixedDiscount(double totalBillAmount)
        {
            int hundreds = Convert.ToInt32(Math.Floor(totalBillAmount / 100));

            return (hundreds) * 5;
        }

        public static double GetTotalAmountBasedOnPercentageDiscount(List<IProduct> products, IUser user)
        {
            double totalPercentageDiscount = 0;

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
        private static float GetPercentageElligibility(IUser user)
        {
            int percentageElligibility = 0;
            switch (user.Type)
            {
                case UserType.Employee:
                    percentageElligibility = 30;
                    break;

                case UserType.Affiliate:
                    percentageElligibility = 10;
                    break;

                default:
                    percentageElligibility = IsUserOlderThen(user.JoiningDate, 2) ? 5 : 0;
                    break;
            }
            return percentageElligibility;
        }
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