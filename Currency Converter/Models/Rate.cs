namespace Currency_Converter.Models
{
    public class Rate
    {
        public Rate(string from, string to, double value)
        {
            From = from;
            To = to;
            Value = value;
        }

        public string From { get; set; }
        public string To { get; set; }
        public double Value { get; set; }
    }
}
