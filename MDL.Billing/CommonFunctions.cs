using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDL.Billing
{
    /// <summary>
    /// Common utility functions for billing.  The common utilities are abstracted to avoid any code change in case of the business rules are changed or new rules are introduced e.g., start giving 7% to users older than 5 years.
    /// Its a singleton to avoid multiple instances creation and destroying.
    /// </summary>
    public sealed class CommonFunctions
    {
        private static readonly Lazy<CommonFunctions> lazy =
            new Lazy<CommonFunctions>(() => new CommonFunctions());

        public static CommonFunctions Instance { get { return lazy.Value; } }

        private CommonFunctions()
        {
        }

        internal static int GetPercentageElligibility(User user)
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

        public static bool IsUserOlderThen(DateTime joiningDate, int yearsToCompare)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            TimeSpan span = DateTime.Now - joiningDate;

            int years = (zeroTime + span).Year - 1;

            return years >= yearsToCompare;
        }
    }
}
