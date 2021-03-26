using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L3_DAVH_AFPE.Models.Data
{
    public class Drug : IComparable
    {
        public int numberline { get; set; }
        public string name { get; set; }


        public int CompareTo(object obj)
        {
            var comparer = ((Drug)obj).name;
            return comparer.CompareTo(name);
        }
    }
}
