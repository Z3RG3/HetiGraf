namespace HetiGraf;

public class Graph<T>
{
        List<T> tartalom = new List<T>(); // gráf csúcsai
        List<List<T>> szomszedsagiLista = new List<List<T>>();
        private Action<string> externalProcessor; // külső feldolgozó metódus referencia

        public void SetExternalProcessor(Action<string> processor)
        {
            externalProcessor = processor;
        }
        
        public void TraverseGraph()
        {
            foreach (var csucs in tartalom)
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
            tartalom.Add(csucs);
            szomszedsagiLista.Add(new List<T>());
        }

        public void AddEdge(T from, T to)
        {
            int indexFrom = tartalom.IndexOf(from);
            int indexTo = tartalom.IndexOf(to);

            // irányítatlan
            szomszedsagiLista[indexFrom].Add(to);
            szomszedsagiLista[indexTo].Add(from);
        }

        public bool HasEdge(T from, T to)
        {
            int indexFrom = tartalom.IndexOf(from);
            int indexTo = tartalom.IndexOf(to);

            return szomszedsagiLista[indexFrom].Contains(tartalom[indexTo]);
        }

        public List<T> Neighbors(T csucs)
        {
            int index = tartalom.IndexOf(csucs);
            return szomszedsagiLista[index];
        }

        public void SzelessegiBejaras(T start)
        {
            Queue<T> S = new Queue<T>();
            List<T> F = new List<T>();

            S.Enqueue(start); // gráf csúcsai
            F.Add(start); // feldolgozott/feldolgozandó elemek

            T k;
            while (S.Count != 0)
            {
                k = S.Dequeue();
                Console.Write(k.ToString() + ", ");
                foreach (T x in Neighbors(k))
                {
                    if (!F.Contains(x))
                    {
                        S.Enqueue(x);
                        F.Add(x);
                    }
                }
            }
        }

        public void MelysegiBejaras(T csucs)
        {
            List<T> F = new List<T>();
            MelysegiBejarasRek(csucs, ref F);
        }

        private void MelysegiBejarasRek(T k, ref List<T> F)
        {
            F.Add(k);
            Console.Write(k.ToString() + ", ");
            foreach (T x in Neighbors(k))
            {
                if (!F.Contains(x))
                {
                    MelysegiBejarasRek(x, ref F);
                }
            }
        }
}
