using DataStructures;
using System;

namespace L3_DAVH_AFPE.Models.Data
{

    public sealed class Singleton
    {
        public double total;
        public string sd;
        private readonly static Singleton _instance = new Singleton();
        public DoubleLinkedList<PharmacyModel> orders;
        public DoubleLinkedList<PharmacyModel> inventory;
        public AVLtree<Drug> guide;
        public bool fileUpload = false;
        public string tree = "";
        public int contCarts = 0;
        public Cart cart;
        private Singleton()
        {
            orders = new DoubleLinkedList<PharmacyModel>();
            inventory = new DoubleLinkedList<PharmacyModel>();
            guide = new AVLtree<Drug>();
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
            return "$" + Instance.inventory.Get(Instance.guide.Find(x => x.name.CompareTo(product), Singleton.Instance.guide.Root).numberline).Price;
        }

        public double totalre()
        {
            total = 0;
            for (int i = 0; i < orders.Length; i++)
            {
                var x = orders.Get(i);
                total += x.Price * x.Quantity;
            }
            return Math.Round(total,2);
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

        public string PreOrder(TreeNode<Drug> node)
        {
            if (node == null)
            {
                return "";
            }
            tree += node.value.name + "| Numberline: " + node.value.numberline + "\r\n";
            if (node.left != null)
            {
                PreOrder(node.left);
            }
            if (node.right != null)
            {
                PreOrder(node.right);
            }
            return tree;
        }
        public string InOrder(TreeNode<Drug> node)
        {
            if (node == null)
            {
                return "";
            }
            if (node.left != null)
            {
                InOrder(node.left);
            }
            tree += node.value.name + "| Numberline: " + node.value.numberline + "\r\n";
            if (node.right != null)
            {
                InOrder(node.right);
            }
            return tree;
        }
        public string PostOrder(TreeNode<Drug> node)
        {
            if (node == null)
            {
                return "";
            }
            if (node.left != null)
            {
                InOrder(node.left);
            }
            if (node.right != null)
            {
                InOrder(node.right);
            }
            tree += node.value.name + "| Numberline: " + node.value.numberline + "\r\n";
            return tree;
        }

    }
}