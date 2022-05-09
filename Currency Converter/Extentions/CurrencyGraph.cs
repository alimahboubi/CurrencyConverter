using Currency_Converter.Models;

namespace Currency_Converter.Extentions
{
    public class CurrencyGraph
    {
        public CurrencyGraph()
        {

        }
        public CurrencyGraph(IEnumerable<Rate> conversionRates)
        {
            foreach (var conversionRate in conversionRates)
            {
                AddVertex(conversionRate.From, conversionRate.To);
            }

            foreach (var edge in conversionRates)
                AddEdge(edge);
        }
        public Dictionary<string, Dictionary<string, double>> AdjacencyList { get; } = new Dictionary<string, Dictionary<string, double>>();

        private void AddVertex(string vertex1, string vertex2)
        {
            if (!AdjacencyList.ContainsKey(vertex1))
                AdjacencyList[vertex1.Trim().ToUpper()] = new Dictionary<string, double>();
            if (!AdjacencyList.ContainsKey(vertex2))
                AdjacencyList[vertex2.Trim().ToUpper()] = new Dictionary<string, double>();
        }

        private void AddEdge(Rate edge)
        {
            if (AdjacencyList.ContainsKey(edge.From) && AdjacencyList.ContainsKey(edge.To))
            {
                AdjacencyList[edge.From.Trim().ToUpper()].Add(edge.To.Trim().ToUpper(), edge.Value);
                AdjacencyList[edge.To.Trim().ToUpper()].Add(edge.From.Trim().ToUpper(), 1 / edge.Value);
            }
        }

    
    }
}
