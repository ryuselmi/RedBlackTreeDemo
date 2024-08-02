using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTreeDemo
{
    public class RBNode
    {
        public int Value;
        public Color NodeColor;
        public RBNode Left;
        public RBNode Right;
        public RBNode Parent;

        public RBNode(int value)
        {
            Value = value;
            NodeColor = Color.Red;
            Left = null;
            Right = null;
            Parent = null;
        }
    }
}
