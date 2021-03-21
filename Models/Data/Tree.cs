﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L3_DAVH_AFPE.Models.Data
{
    //public class Tree
    //{
    //    TreeNode<T> root;

        
    //    int height(TreeNode<T> N)
    //    {
    //        if (N == null)
    //            return 0;

    //        return N.height;
    //    }

        
    //    int max(int a, int b)
    //    {
    //        return (a > b) ? a : b;
    //    }
        
    //    TreeNode<T> rightRotate(TreeNode<T> y)
    //    {
    //        TreeNode<T> x = y.left;
    //        TreeNode<T> T2 = x.right;
            
    //        x.right = y;
    //        y.left = T2;            
    //        y.height = max(height(y.left),
    //                    height(y.right)) + 1;
    //        x.height = max(height(x.left),
    //                    height(x.right)) + 1;
            
    //        return x;
    //    }

    //    TreeNode<T> leftRotate(TreeNode<T> x)
    //    {
    //        TreeNode<T> y = x.right;
    //        TreeNode<T> T2 = y.left;

            
    //        y.left = x;
    //        x.right = T2;
           
    //        x.height = max(height(x.left),
    //                    height(x.right)) + 1;
    //        y.height = max(height(y.left),
    //                    height(y.right)) + 1;
            
    //        return y;
    //    }
        
    //    int getBalance(TreeNode<T> N)
    //    {
    //        if (N == null)
    //            return 0;

    //        return height(N.left) - height(N.right);
    //    }

    //    TreeNode<T> insert(TreeNode<T> node, int key)
    //    {
            
    //        if (node == null)
    //            return (new Node(key));

    //        if (key < node.key)
    //            node.left = insert(node.left, key);
    //        else if (key > node.key)
    //            node.right = insert(node.right, key);
    //        else 
    //            return node;

    //        /* 2. Update height of this ancestor node */
    //        node.height = 1 + max(height(node.left),
    //                            height(node.right));

    //        /* 3. Get the balance factor of this ancestor 
    //            node to check whether this node became 
    //            unbalanced */
    //        int balance = getBalance(node);

    //        // If this node becomes unbalanced, then there 
    //        // are 4 cases Left Left Case 
    //        if (balance > 1 && key < node.left.key)
    //            return rightRotate(node);

    //        // Right Right Case 
    //        if (balance < -1 && key > node.right.key)
    //            return leftRotate(node);

    //        // Left Right Case 
    //        if (balance > 1 && key > node.left.key)
    //        {
    //            node.left = leftRotate(node.left);
    //            return rightRotate(node);
    //        }

    //        // Right Left Case 
    //        if (balance < -1 && key < node.right.key)
    //        {
    //            node.right = rightRotate(node.right);
    //            return leftRotate(node);
    //        }

    //        /* return the (unchanged) node pointer */
    //        return node;
    //    }
    //}
}
