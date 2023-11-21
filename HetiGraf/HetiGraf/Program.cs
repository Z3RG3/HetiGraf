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

        graph.AddEdge(stew, joseph); 
        graph.AddEdge(stew, marge); 
        graph.AddEdge(joseph, marge); 
        graph.AddEdge(joseph, gerald); 
        graph.AddEdge(joseph, zack); 
        graph.AddEdge(gerald, zack); 
        graph.AddEdge(zack, peter); 
        graph.AddEdge(peter, janet); 

        graph.SetExternalProcessor(Console.WriteLine);
        graph.TraverseGraph();
        
        graph.SetExternalProcessor(AppendToFile);
        graph.TraverseGraph();
    }
   public static void AppendToFile(string item)
    {
        File.AppendAllText("output.txt", item + Environment.NewLine);
        Console.WriteLine("Append to file: " + item);
    }
    
}