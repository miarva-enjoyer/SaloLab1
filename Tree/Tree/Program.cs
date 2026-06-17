public class Tree
{
    public int valueTree { get; set; }

    public List<Tree> children { get; set; }

    public Tree(int value)
    {
        valueTree = value;
        children = new List<Tree>();
    }

    public void render()
    {
        Console.WriteLine($"Value Tree: {valueTree} \n");

        foreach (var child in children)
        {
            child.render();
        }
    }
}

class Application
{
    static void Main(string[] args)
    {
        Tree tree = new Tree(1);

        Tree firstPar = new Tree(2);
        Tree secondPar = new Tree(3);

        Tree firstChild = new Tree(100);
        Tree secondChild = new Tree(200);
        Tree thirdChild = new Tree(300);

        tree.children.Add(firstPar);
        tree.children.Add(secondPar);

        firstPar.children.Add(firstChild);
        firstPar.children.Add(secondChild);

        secondPar.children.Add(thirdChild);

        Console.WriteLine("Tree Structure: \n");
        tree.render();

    }
}