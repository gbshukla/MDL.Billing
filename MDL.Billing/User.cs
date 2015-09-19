
namespace MDL.Billing
{
    #region Namespaces
    using System;
    #endregion Namespaces

    public interface IUser
    {
        // UserID
        long Id { get; set; }

        // First Name 
        string FirstName { get; set; }

        // Last Name
        string LastName { get; set; }

        // First time transaction
        DateTime JoiningDate { get; set; }

        // Type of the user
        UserType Type { get; set; }
    }

    /// <summary>
    /// Users of the store
    /// </summary>
    public class User : IUser
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

        // Dependency to ensure a user have at least first name
        public User(string firstName)
        {
            this.FirstName = firstName;
        }
    }
    
    /// <summary>
    /// Type of the user. e.g., Employee, Affiliate etc
    /// </summary>
    public enum UserType
    {
        // Default 
        Undefined, 

        // For Employees
        Employee,

        // For Affiliates
        Affiliate
    }
}