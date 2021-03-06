using System;

namespace DataStructures
{
    public class AVLtree<T> where T : IComparable
    {
        #region Variables and instances
        public TreeNode<T> Root { get; set; }
        public TreeNode<T> Work { get; set; }
        public int lenght = 0;
        public AVLtree()
        {
            Root = null;
            Work = null;
        }
        #endregion

        #region Methods
        public TreeNode<T> Insert(T newvalue, TreeNode<T> pNode)
        {
            if (pNode == null)
            {
                lenght++;
                return new TreeNode<T>(newvalue);
            }
            if (newvalue.CompareTo(pNode.value) > 0)
            {
                pNode.left = Insert(newvalue, pNode.left);
            }
            else if (newvalue.CompareTo(pNode.value) < 0)
            {
                pNode.right = Insert(newvalue, pNode.right);
            }
            else
            {
                return pNode;
            }
            pNode.height = 1 + max(height(pNode.left), height(pNode.right));

            int balance = getBalance(pNode);

            //Left Left Case
            if (balance > 1 && newvalue.CompareTo(pNode.left.value) > 0)
                return rightRotate(pNode);

            // Right Right Case
            if (balance < -1 && newvalue.CompareTo(pNode.right.value) < 0)
                return leftRotate(pNode);

            // Left Right Case
            if (balance > 1 && newvalue.CompareTo(pNode.left.value) < 0)
            {
                pNode.left = leftRotate(pNode.left);
                return rightRotate(pNode);
            }

            // Right Left Case
            if (balance < -1 && newvalue.CompareTo(pNode.right.value) > 0)
            {
                pNode.right = rightRotate(pNode.right);
                return leftRotate(pNode);
            }
            return pNode;

        }
        public T Find(Func<T, int> comparer, TreeNode<T> node)
        {
            if (node != null)
            {
                if (comparer.Invoke(node.value) == 0)
                {
                    return node.value;
                }
                if (comparer.Invoke(node.value) > 0)
                {
                    return Find(comparer, node.left);
                }
                else
                {
                    return Find(comparer, node.right);
                }
            }

            return default;
        }
        public TreeNode<T> SearchParent(TreeNode<T> node, TreeNode<T> parent)
        {
            TreeNode<T> temp = null;
            if (node == null)
            {
                return null;
            }
            if (parent.left != null)
            {
                if (parent.left.value.CompareTo(node.value) == 0)
                {
                    return parent;
                }
            }
            if (parent.right != null)
            {
                if (parent.right.value.CompareTo(node.value) == 0)
                {
                    return parent;
                }
            }

            if (node.value.CompareTo(parent.value) > 0 && parent.left != null)
            {
                temp = SearchParent(node, parent.left);
            }
            if (node.value.CompareTo(parent.value) < 0 && parent.right != null)
            {
                temp = SearchParent(node, parent.right);
            }
            return temp;
        }

        public TreeNode<T> Delete(TreeNode<T> node, T value)
        {
            if (node == null)
            {
                return node;
            }
            if (value.CompareTo(node.value) > 0)
            {
                node.left = Delete(node.left, value);
            }
            else if (value.CompareTo(node.value) < 0)
            {
                node.right = Delete(node.right, value);
            }
            else
            {
                if (node.left == null)
                {
                    return node.right;
                }
                else if (node.right == null)
                {
                    return node.left;
                }
                else
                {
                    node.value = FindMinimum(node.right).value;
                    node.right = Delete(node.right, node.value);
                }
            }
            node.height = max(height(node.left), height(node.right)) + 1;

            int balance = getBalance(node);

            if (balance > 1 && getBalance(node.left) <= 0) //If node becomes with desbalance, it will be compared with the 4 possible cases of rotations.
                                                          //Left->Left
            {
                return rightRotate(node);
            }
            if (balance > 1 && getBalance(node.left) > 0) // Left->Right case
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }
            if (balance < -1 && getBalance(node.right) >= 0)// Right->Right Case
            {
                return leftRotate(node);
            }
            if (balance < -1 && getBalance(node.right) < 0)  // Right->Left Case
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }
            return node;
        }

        public TreeNode<T> FindMinimum(TreeNode<T> node)
        {
            if (node == null)
            {
                return default;
            }
            Work = node;
            TreeNode<T> minimum = Work;

            while (Work.left != null)
            {
                Work = Work.left;
                minimum = Work;
            }
            return minimum;

        }

        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        int height(TreeNode<T> N)
        {
            if (N == null)
                return 0;

            return N.height;
        }

        int getBalance(TreeNode<T> N)
        {
            if (N == null)
            {
                return 0;
            }

            return height(N.left) - height(N.right);
        }

        TreeNode<T> rightRotate(TreeNode<T> y)
        {
            TreeNode<T> x = y.left;
            TreeNode<T> z = x.right;

            x.right = y;
            y.left = z;

            y.height = max(height(y.left), height(y.right)) + 1;
            x.height = max(height(x.left), height(x.right)) + 1;

            return x;
        }

        TreeNode<T> leftRotate(TreeNode<T> x)
        {
            TreeNode<T> y = x.right;
            TreeNode<T> z = y.left;

            y.left = x;
            x.right = z;

            x.height = max(height(x.left), height(x.right)) + 1;
            y.height = max(height(y.left), height(y.right)) + 1;

            return y;
        }
        #endregion
    }
}
