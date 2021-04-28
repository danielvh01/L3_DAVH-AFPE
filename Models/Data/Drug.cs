using System;

namespace L3_DAVH_AFPE.Models.Data
{
    public class Drug : IComparable
    {
        #region GETS/SETS
        public int numberline { get; set; }
        public string name { get; set; }
        #endregion

        #region Method
        public int CompareTo(object obj)
        {
            var comparer = ((Drug)obj).name;
            return comparer.CompareTo(name);
        }
        #endregion
    }
}
