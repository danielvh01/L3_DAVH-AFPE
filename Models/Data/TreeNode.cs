using System;
using System.Collections.Generic;
using System.Text;

namespace L3_DAVH_AFPE.Models.Data
{
    public class TreeNode<T> where T : IComparable
    {
        
        public TreeNode<T> parent { get; set; }
        public TreeNode<T> left { get; set; }
        public TreeNode<T> right { get; set; }
        public T value { get; set; }

        public int height;

        public TreeNode(T newvalue)
        {
            value = newvalue;
            left = null;
            right = null;
            parent = null;
            height = 1;
        }




    }
}
