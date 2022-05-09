using Currency_Converter.Models;

namespace Currency_Converter.Extentions
{
    public class BFSAlgorithm
    {
        public Func<string, IEnumerable<Rate>> ShortestPathFunction(CurrencyGraph graph, string start)
        {
            var previous = new List<Rate>();

            var queue = new Queue<string>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in graph.AdjacencyList[vertex])
                {
                    if (previous.Any(q => q.From == neighbor.Key))
                        continue;

                    previous.Add(new Rate(from: neighbor.Key, to: vertex, value: neighbor.Value));
                    queue.Enqueue(neighbor.Key);
                }
            }

            Func<string, IEnumerable<Rate>> shortestPath = v =>
            {
                var path = new List<Rate>();

                var current = v;
                while (!current.Equals(start))
                {
                    var item = previous.FirstOrDefault(q=>q.From==current);
                    if (item == null)
                        break;
                    path.Add(new Rate(from:item.To, to:item.From,value:item.Value));
                    current = item.To;
                };

                path.Reverse();

                return path;
            };

            return shortestPath;
        }
    }
}
