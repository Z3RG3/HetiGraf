// See https://aka.ms/new-console-template for more information
namespace HetiGraf;
internal class Program
{
    public static void Main(string[] args)
    {
        Graph<int> graph = new Graph<int>();

        graph.AddNode(1);
        graph.AddNode(2);
        graph.AddNode(3);

        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);

        // Teszteljük a külső feldolgozást
        graph.SetExternalProcessor(Console.WriteLine);

        // Vagy használjuk a saját feldolgozó metódust (File-hez fűzés)
        graph.SetExternalProcessor(AppendToFile);

        // Bejárjuk a gráfot és alkalmazzuk a kiválasztott feldolgozó metódust
        graph.TraverseGraph();
    }
    static void AppendToFile(string item)
    {
        // Ebben a példában csak a konzolra írjuk ki, de itt lehetne tényleges fájlba fűzés
        Console.WriteLine("Append to file: " + item);
    }
    
}