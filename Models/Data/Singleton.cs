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
        public Tree<Drug> guide;
        public bool fileUpload = false;
        public string tree = "";
        private Singleton()
        {
            orders = new DoubleLinkedList<Cart>();
            inventory = new DoubleLinkedList<PharmacyModel>();
            guide = new Tree<Drug>();
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
        public string getPrice(string product)
        {
            return "$" + Instance.inventory.Get(Instance.guide.Find(new Drug { name = product, numberline = 0 }, guide.Root).value.numberline).Price;
        }

        public void Traverse(TreeNode<Drug> node)
        {
            if (node == null)
            {
                return;
            }
            if (node.right != null)
            {
                Traverse(node.right);
            }
            options.InsertAtEnd(node.value.name + " - " + getPrice(node.value.name));
            if (node.left != null)
            {
                Traverse(node.left);
            }
        }

        public void Resuply()
        {
            for (int i = 0; i < Models.Data.Singleton.Instance.inventory.Length; i++)
            {
                PharmacyModel item = Models.Data.Singleton.Instance.inventory.Get(i);
                if (item.Quantity == 0)
                {
                    Random r = new Random();
                    item.Quantity = r.Next(1, 15);
                    Singleton.Instance.guide.Insert(new Drug { name = item.Name, numberline = i }, Singleton.Instance.guide.Root);
                }
            }
        }

        public string PrintTree(TreeNode<Drug> node)
        {
            if (node == null)
            {
                return "";
            }
            if (node.right != null)
            {
                PrintTree(node.right);
            }
            tree += node.value.name + " Numberline: " + node.value.numberline + "\r\n";

            if (node.left != null)
            {
                PrintTree(node.left);
            }
            return tree;
        }

    }
}