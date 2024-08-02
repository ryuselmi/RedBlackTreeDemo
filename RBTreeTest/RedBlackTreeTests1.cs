using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedBlackTreeDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBTreeTest
{
    [TestClass]
    public class RedBlackTreeTests1
    {
        [TestMethod()]
        public void Insert_ShouldAddNodesCorrectly()
        {
            RedBlackTree tree = new RedBlackTree();
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(30);

            Assert.IsNotNull(tree.Search(tree.GetRoot(), 10), "Node 10 should be present in the tree.");
            Assert.IsNotNull(tree.Search(tree.GetRoot(), 20), "Node 20 should be present in the tree.");
            Assert.IsNotNull(tree.Search(tree.GetRoot(), 30), "Node 30 should be present in the tree.");
        }
    }
}
