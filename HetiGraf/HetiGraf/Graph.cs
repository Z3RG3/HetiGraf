namespace HetiGraf;

public delegate void GraphEventHandler<T>(object source, GraphEventArgs<T> geargs); //1 delegalt
public class Graph<T>
{
        List<T> nodes = new List<T>(); // gráf csúcsai
        List<List<T>> neighboursList = new List<List<T>>();
        private Action<string> externalProcessor; // külső feldolgozó metódus referencia

        public void SetExternalProcessor(Action<string> processor)
        {
            externalProcessor = processor;
        }
        
        public void TraverseGraph()
        {
            foreach (var csucs in nodes)
            {
                ProcessItem(csucs);
            }
        }
        private void ProcessItem(T item)
        {
            if (externalProcessor != null)
            {
                //külső feldolgozó metódus meghivasa a jelenlegi elemre
                externalProcessor(item.ToString());
            }
        }
        
        public void AddNode(T csucs)
        {
            nodes.Add(csucs);
            neighboursList.Add(new List<T>());
        }
      
        public event GraphEventHandler<T> EdgeAdded; // 2 event
        protected virtual void OnEdgeAdded(T from, T to)
        {
            EdgeAdded?.Invoke(this, new GraphEventArgs<T>(from, to));
        }
        public void AddEdge(T from, T to)
        {
            int indexFrom = nodes.IndexOf(from);
            int indexTo = nodes.IndexOf(to);

            // irányítatlan
            neighboursList[indexFrom].Add(to);
            neighboursList[indexTo].Add(from);
            
            OnEdgeAdded(from, to); //3 event.?Invoke (meghivas)
        }
        
        public bool HasEdge(T from, T to)
        {
            int indexFrom = nodes.IndexOf(from);
            int indexTo = nodes.IndexOf(to);

            return neighboursList[indexFrom].Contains(nodes[indexTo]);
        }

        public List<T> Neighbors(T csucs)
        {
            int index = nodes.IndexOf(csucs);
            return neighboursList[index];
        }
        public event EventHandler<BFSCompletedEventArgs<T>> BFSCompleted;
        public void BFS(T start, T target)
        {
            Queue<Tuple<T, List<T>>> queue = new Queue<Tuple<T, List<T>>>();
            List<T> visited = new List<T>();

            queue.Enqueue(new Tuple<T, List<T>>(start, new List<T> { start }));

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                T currentNode = current.Item1;
                List<T> currentPath = current.Item2;

                //kiváltjuk az eseményt
                OnBFSCompleted(currentNode, currentPath, target);

                if (currentNode.Equals(target))
                {
                    return;
                }

                foreach (T neighbor in Neighbors(currentNode))
                {
                    if (!visited.Contains(neighbor))
                    {
                        var newPath = new List<T>(currentPath) { neighbor };
                        queue.Enqueue(new Tuple<T, List<T>>(neighbor, newPath));
                        visited.Add(neighbor);
                    }
                }
            }

            // Ha nincs kapcsolat
            OnBFSCompleted(default, null, target);
        }

        protected virtual void OnBFSCompleted(T currentNode, List<T> currentPath, T target)
        {
            BFSCompleted?.Invoke(this, new BFSCompletedEventArgs<T>(currentNode, currentPath, target));
        }
        public void DFS(T csucs) // mélységi (Depth First Search)
        {
            List<T> visited = new List<T>();
            DFSRecursive(csucs, ref visited);
        }

        private void DFSRecursive(T k, ref List<T> visited)
        {
            visited.Add(k);
            Console.Write(k.ToString() + ", ");
            foreach (T x in Neighbors(k))
            {
                if (!visited.Contains(x))
                {
                    DFSRecursive(x, ref visited);
                }
            }
        }
        
}
public class BFSCompletedEventArgs<T> : EventArgs
{
    public T CurrentNode { get; }
    public List<T> CurrentPath { get; }
    public T Target { get; }

    public BFSCompletedEventArgs(T currentNode, List<T> currentPath, T target)
    {
        CurrentNode = currentNode;
        CurrentPath = currentPath;
        Target = target;
    }
}

public class GraphEventArgs<T> : EventArgs 
{
    public T NodeA { get; }
    public T NodeB { get; }

    public GraphEventArgs(T nodeA, T nodeB)
    {
        NodeA = nodeA;
        NodeB = nodeB;
    }
}
