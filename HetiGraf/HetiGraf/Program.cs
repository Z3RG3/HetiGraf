// See https://aka.ms/new-console-template for more information
namespace HetiGraf;
internal class Program
{
    public static void Main(string[] args)
    {
        Graph<Person> graph = new Graph<Person>();
        
        Person stew = new Person("Stew");
        Person joseph = new Person("Joseph");
        Person marge = new Person("Marge");
        Person gerald = new Person("Gerald");
        Person zack = new Person("Zack");
        Person peter = new Person("Peter");
        Person janet = new Person("Janet");
        
        graph.AddNode(stew);
        graph.AddNode(joseph);
        graph.AddNode(marge);
        graph.AddNode(gerald);
        graph.AddNode(zack);
        graph.AddNode(peter);
        graph.AddNode(janet);
        
            
            graph.EdgeAdded += Graph_EdgeAdded; //4 +felirat, 5 trigger eventek <
        graph.AddEdge(stew, joseph); 
        graph.AddEdge(stew, marge); 
        graph.AddEdge(joseph, marge); 
        graph.AddEdge(joseph, gerald); 
        graph.AddEdge(joseph, zack); 
        graph.AddEdge(gerald, zack); 
        graph.AddEdge(zack, peter); 
        graph.AddEdge(peter, janet); 
        
        graph.SetExternalProcessor(AppendToFile);
        graph.TraverseGraph();

            graph.BFSCompleted += Graph_BFSCompleted;
        Console.WriteLine("BFS eredmény:");
        graph.BFS(janet, gerald);
        
    }
    static void Graph_BFSCompleted(object sender, BFSCompletedEventArgs<Person> e)
    {
        if (e.CurrentNode != null)
        {
            Console.Write(e.CurrentNode.ToString() + ", ");
        }

        if (e.CurrentNode != null && e.CurrentNode.Equals(e.Target))
        {
            Console.WriteLine("\nIsmeretség foka: " + (e.CurrentPath.Count - 1));
            Console.WriteLine("Ismeretség útvonal: " + string.Join(" -> ", e.CurrentPath));
        }
        else
        {
            Console.WriteLine("Nincs kapcsolat a két személy között.");
        }
    }
    static void Graph_EdgeAdded(object source, GraphEventArgs<Person> args) // event kezelo
    {
        Console.WriteLine($"Edge added between {args.NodeA} and {args.NodeB}");
    }
   public static void AppendToFile(string item)
    {
        File.AppendAllText("output.txt", item + Environment.NewLine);
        Console.WriteLine("Append to file: " + item);
    }
    
}