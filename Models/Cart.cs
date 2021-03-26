using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace L3_DAVH_AFPE.Models
{
    public class Cart : IComparable
    {
        #region GETS / SETS
        public int ID { get; set; }
        [Required]
        public string clientName { get; set; }
        [Required]
        public string NIT { get; set; }
        [Required]
        public string address { get; set; }        
        public double amount { get; set; }
        public string product { get; set; }


        #endregion

        #region Method
        public int CompareTo(object obj)
        {
            var comparer = ((Cart)obj).product;
            return comparer.CompareTo(product);
        }
        #endregion
    }
}
