using System;

namespace L3_DAVH_AFPE.Models
{
    public class PharmacyModel : IComparable
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string Production_Factory { get; set; }

        public double Price { get; set; }
        //public int Stock { get; set; }

        public int Quantity { get; set; }

        public void ChangeQuantity(int Q)
        {
            Quantity = Q;
        }

        public int CompareTo(object obj)
        {
            var comparer = ((PharmacyModel)obj).Name;
            return comparer.CompareTo(Name);
        }
    }

}
