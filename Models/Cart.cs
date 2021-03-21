using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L3_DAVH_AFPE.Models
{
    public class Cart : IComparable
    {
        public string clientName { get; set; }
        public string NIT { get; set; }
        public string address { get; set; }
        public double amount { get; set; }
        public string product { get; set; }

        public int CompareTo(object obj)
        {
            var comparer = ((Cart)obj).product;
            return comparer.CompareTo(product);
        }
    }
}
