using System;

namespace RedBlackTreeDemo;
public enum Color
{
    Red,
    Black
}

public class Program
{
    public static void Main()
    {
        RedBlackTree rbTree = new RedBlackTree();

        rbTree.Insert(10);
        rbTree.Insert(20);
        rbTree.Insert(30);
        rbTree.Insert(15);
        rbTree.Insert(25);

        Console.WriteLine("In-Order Traversal:");
        rbTree.InOrderTraversal(rbTree.GetRoot());

        Console.WriteLine("\nDeleting 20");
        rbTree.Delete(20);
        Console.WriteLine("In-Order Traversal:");
        rbTree.InOrderTraversal(rbTree.GetRoot());

        Console.WriteLine("\nSearching for 15");
        RBNode searchResult = rbTree.Search(rbTree.GetRoot(), 15);
        if (searchResult != null)
            Console.WriteLine($"Found: {searchResult.Value} ({searchResult.NodeColor})");
        else
            Console.WriteLine("Not found");

        Console.WriteLine("\nSearching for 35");
        RBNode searchResult2 = rbTree.Search(rbTree.GetRoot(), 35);
        if (searchResult2 != null)
            Console.WriteLine($"Found: {searchResult2.Value} ({searchResult2.NodeColor})");
        else
            Console.WriteLine("Not found");
    }
}