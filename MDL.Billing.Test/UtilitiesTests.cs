using System;
using Xunit;

namespace MDL.Billing.Test
{
    public class UtilitiesTests
    {
        [Theory]
        [InlineData("09/11/2001",2, true)]
        [InlineData("09/11/2015",2, false)]
        public void UserElligibleForFivePercentOrNot(string dateString,int yearsToCompare, bool result)
        {
            DateTime inputDate = DateTime.Parse(dateString);
            Assert.Equal(CommonFunctions.IsUserOlderThen(inputDate, yearsToCompare), result);
        }
    }
}
