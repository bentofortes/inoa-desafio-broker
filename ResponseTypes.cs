namespace ResponseTypes{

    public class RequestResponse{
        public QuoteSummary ?quoteSummary { get; set; }
    }

    public class QuoteSummary{
        public Result[] ?result { get; set; }
        public Error ?error { get; set; }
    }

    public class Result{
        public Price ?price { get; set; }
    }

    public class Error{
        public string ?code { get; set; }
        public string ?description { get; set; }
    }

    public class Price{
        public string ?currencySymbol { get; set; }
        public string ?longName { get; set; }
        public string ?symbol { get; set; }
        public float ?buyPrice { get; set; }
        public float ?sellPrice { get; set; }
        public RegularMarketPrice ?regularMarketPrice { get; set; }
    }

    public class RegularMarketPrice{
        public float raw { get; set; }
    }
}