using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTreeDemo
{
    public class RedBlackTree
    {
        private RBNode root;

        public RedBlackTree()
        {
            root = null;
        }

        public void Insert(int value)
        {
            RBNode newNode = new RBNode(value);
            if (root == null)
            {
                root = newNode;
                root.NodeColor = Color.Black;
            }
            else
            {
                RBNode parent = null;
                RBNode current = root;
                while (current != null)
                {
                    parent = current;
                    if (value < current.Value)
                        current = current.Left;
                    else
                        current = current.Right;
                }

                newNode.Parent = parent;
                if (value < parent.Value)
                    parent.Left = newNode;
                else
                    parent.Right = newNode;

                FixInsert(newNode);
            }
        }

        private void FixInsert(RBNode node)
        {
            while (node != root && node.Parent.NodeColor == Color.Red)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    RBNode uncle = node.Parent.Parent.Right;
                    if (uncle != null && uncle.NodeColor == Color.Red)
                    {
                        node.Parent.NodeColor = Color.Black;
                        uncle.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }
                        node.Parent.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        RightRotate(node.Parent.Parent);
                    }
                }
                else
                {
                    RBNode uncle = node.Parent.Parent.Left;
                    if (uncle != null && uncle.NodeColor == Color.Red)
                    {
                        node.Parent.NodeColor = Color.Black;
                        uncle.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        node.Parent.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        LeftRotate(node.Parent.Parent);
                    }
                }
            }
            root.NodeColor = Color.Black;
        }

        private void LeftRotate(RBNode node)
        {
            RBNode temp = node.Right;
            node.Right = temp.Left;
            if (temp.Left != null)
                temp.Left.Parent = node;

            temp.Parent = node.Parent;
            if (node.Parent == null)
                root = temp;
            else if (node == node.Parent.Left)
                node.Parent.Left = temp;
            else
                node.Parent.Right = temp;

            temp.Left = node;
            node.Parent = temp;
        }

        private void RightRotate(RBNode node)
        {
            RBNode temp = node.Left;
            node.Left = temp.Right;
            if (temp.Right != null)
                temp.Right.Parent = node;

            temp.Parent = node.Parent;
            if (node.Parent == null)
                root = temp;
            else if (node == node.Parent.Right)
                node.Parent.Right = temp;
            else
                node.Parent.Left = temp;

            temp.Right = node;
            node.Parent = temp;
        }

        public void Delete(int value)
        {
            RBNode nodeToDelete = Search(root, value);
            if (nodeToDelete == null)
                return;

            RBNode y = nodeToDelete;
            Color originalColor = y.NodeColor;
            RBNode x;

            if (nodeToDelete.Left == null)
            {
                x = nodeToDelete.Right;
                Transplant(nodeToDelete, nodeToDelete.Right);
            }
            else if (nodeToDelete.Right == null)
            {
                x = nodeToDelete.Left;
                Transplant(nodeToDelete, nodeToDelete.Left);
            }
            else
            {
                y = Minimum(nodeToDelete.Right);
                originalColor = y.NodeColor;
                x = y.Right;
                if (y.Parent == nodeToDelete)
                {
                    if (x != null)
                        x.Parent = y;
                }
                else
                {
                    Transplant(y, y.Right);
                    y.Right = nodeToDelete.Right;
                    y.Right.Parent = y;
                }

                Transplant(nodeToDelete, y);
                y.Left = nodeToDelete.Left;
                y.Left.Parent = y;
                y.NodeColor = nodeToDelete.NodeColor;
            }

            if (originalColor == Color.Black)
                FixDelete(x);
        }

        private void Transplant(RBNode target, RBNode with)
        {
            if (target.Parent == null)
                root = with;
            else if (target == target.Parent.Left)
                target.Parent.Left = with;
            else
                target.Parent.Right = with;

            if (with != null)
                with.Parent = target.Parent;
        }

        private void FixDelete(RBNode node)
        {
            while (node != root && GetColor(node) == Color.Black)
            {
                if (node == node.Parent.Left)
                {
                    RBNode sibling = node.Parent.Right;
                    if (GetColor(sibling) == Color.Red)
                    {
                        sibling.NodeColor = Color.Black;
                        node.Parent.NodeColor = Color.Red;
                        LeftRotate(node.Parent);
                        sibling = node.Parent.Right;
                    }

                    if (GetColor(sibling.Left) == Color.Black && GetColor(sibling.Right) == Color.Black)
                    {
                        sibling.NodeColor = Color.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (GetColor(sibling.Right) == Color.Black)
                        {
                            sibling.Left.NodeColor = Color.Black;
                            sibling.NodeColor = Color.Red;
                            RightRotate(sibling);
                            sibling = node.Parent.Right;
                        }

                        sibling.NodeColor = node.Parent.NodeColor;
                        node.Parent.NodeColor = Color.Black;
                        sibling.Right.NodeColor = Color.Black;
                        LeftRotate(node.Parent);
                        node = root;
                    }
                }
                else
                {
                    RBNode sibling = node.Parent.Left;
                    if (GetColor(sibling) == Color.Red)
                    {
                        sibling.NodeColor = Color.Black;
                        node.Parent.NodeColor = Color.Red;
                        RightRotate(node.Parent);
                        sibling = node.Parent.Left;
                    }

                    if (GetColor(sibling.Right) == Color.Black && GetColor(sibling.Left) == Color.Black)
                    {
                        sibling.NodeColor = Color.Red;
                        node = node.Parent;
                    }
                    else
                    {
                        if (GetColor(sibling.Left) == Color.Black)
                        {
                            sibling.Right.NodeColor = Color.Black;
                            sibling.NodeColor = Color.Red;
                            LeftRotate(sibling);
                            sibling = node.Parent.Left;
                        }

                        sibling.NodeColor = node.Parent.NodeColor;
                        node.Parent.NodeColor = Color.Black;
                        sibling.Left.NodeColor = Color.Black;
                        RightRotate(node.Parent);
                        node = root;
                    }
                }
            }
            if (node != null)
                node.NodeColor = Color.Black;
        }

        private Color GetColor(RBNode node)
        {
            if (node == null)
                return Color.Black;
            return node.NodeColor;
        }

        public RBNode Search(RBNode node, int value)
        {
            while (node != null && value != node.Value)
            {
                if (value < node.Value)
                    node = node.Left;
                else
                    node = node.Right;
            }
            return node;
        }

        private RBNode Minimum(RBNode node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        public void InOrderTraversal(RBNode node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.WriteLine($"{node.Value} ({node.NodeColor})");
                InOrderTraversal(node.Right);
            }
        }

        public RBNode GetRoot()
        {
            return root;
        }
    }
}
