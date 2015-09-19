using System;

namespace MDL.Billing
{
    /// <summary>
    /// Users of the store
    /// </summary>
    public class User
    {
        // UserID
        public long Id { get; set; }

        // First Name 
        public string FirstName { get; set; }

        // Last Name
        public string LastName { get; set; }

        // First time transaction
        public DateTime JoiningDate { get; set; }

        // Type of the user
        public UserType Type { get; set; }
    }

}
    
    /// <summary>
    /// Type of the user. e.g., Employee, Affiliate etc
    /// </summary>
    public enum UserType
    {
        // For Employees
        Employee,

        // For Affiliates
        Affiliate
    }

