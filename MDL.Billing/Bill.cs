namespace MDL.Billing
{
    #region Namespaces
    using System.Collections.Generic;
    using System.Linq;
    #endregion Namespaces

    /// <summary>
    /// Interface for the Bill Class
    /// </summary>
    public interface IBill
    {
        IUser User { get; set; }
        
        List<IProduct> Products { get; set; }

        //double CalculateNetAmountPayable();
        
        double NetPayableAmount { get; }
    }

    /// <summary>
    /// Concrete Bill Class
    /// </summary>
    public class Bill : IBill
    {
        // Dependency  (Bill must have a User and a list of products)
        public Bill(IUser user, List<IProduct> products)
        {
            this.User = user;
            this.Products = products;
        }
        
        // Products bought
        public List<IProduct> Products
        {
            get; set;
        }
        
        // The buyer
        public IUser User
        {
            get; set;
        }

        public double NetPayableAmount
        {
            get
            {
                // Calculate Total Bill Amount
                double totalBillAmount = Products.Sum(t => t.Price);

                // Get Total Percentage Based Discount
                double totalPercentageDiscount = CommonFunctions.GetPercentageDiscount(Products, User);

                // Get Total Fixed Discount
                double totalFixedDiscount = CommonFunctions.GetFixedDiscount(totalBillAmount);

                // Net AmountP ayable After Discounts = Total Bill Amount - All Discounts
                return totalBillAmount - (totalPercentageDiscount + totalFixedDiscount);
            }
        }

        /// <summary>
        /// Calculates the net amount payable
        /// </summary>
        /// <returns>Net amount payable</returns>
        //public double CalculateNetAmountPayable()
        //{
        //    // Calculate Total Bill Amount
        //    double totalBillAmount = Products.Sum(t => t.Price);

        //    // Get Total Percentage Based Discount
        //    double totalPercentageDiscount = CommonFunctions.GetPercentageDiscount(Products, User);

        //    // Get Total Fixed Discount
        //    double totalFixedDiscount = CommonFunctions.GetFixedDiscount(totalBillAmount);
            
        //    // Net AmountP ayable After Discounts = Total Bill Amount - All Discounts
        //    return totalBillAmount - (totalPercentageDiscount + totalFixedDiscount); 
        //}        
    }
}
