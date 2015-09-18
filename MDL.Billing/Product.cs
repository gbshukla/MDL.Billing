using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDL.Billing
{
    public class Product
    {
        // ID of the item
        public long Id { get; set; }

        // String based code of the item
        public string Code { get; set; }

        // Name of the item
        public string Name { get; set; }

        // Type of the item
        public ProductType Type { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ProductType
    {
        Clothing,
        Grocery,
        Furnuture,
        Sports,
        Electronics,
        Toys
    }
}
