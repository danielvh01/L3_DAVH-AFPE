using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L3_DAVH_AFPE.Models.Data
{
    public sealed class Singleton
    {
        public DoubleLinkedList<string> options = new DoubleLinkedList<string>();
        private readonly static Singleton _instance = new Singleton();
        public DoubleLinkedList<Cart> orders;
        public DoubleLinkedList<PharmacyModel> inventory;
        //public Tree<Drug> guide;
        public bool fileUpload = false;
        public string tree = "";

        private Singleton()
        {
            orders = new DoubleLinkedList<Cart>();
            inventory = new DoubleLinkedList<PharmacyModel>();
            //guide = new Tree<Drug>();
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
