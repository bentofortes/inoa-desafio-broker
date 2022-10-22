using System.Text.Json;

using ResponseTypes;

namespace ApiHelper
{
    public class Requests
    {
        public static async Task getPrice(string assetName)
        {
            var client = new HttpClient();
            RequestResponse ?response;
            
            try
            {
                HttpResponseMessage request = await client.GetAsync($"https://query1.finance.yahoo.com/v11/finance/quoteSummary/{assetName}.sa?modules=price");
                string responseString = await request.Content.ReadAsStringAsync();

                response = JsonSerializer.Deserialize<RequestResponse>(responseString);
                if (response?.quoteSummary?.error?.code == "Not Found")
                {
                    Console.WriteLine("Erro. Ativo nao encontrado\n");
                    Environment.Exit(0);
                }

                Price ?price = response?.quoteSummary?.result?[0].price;
                Console.WriteLine("{0} {1, 20} {2, 20}",
                    DateTime.Now.ToString("G"),
                    price?.symbol,
                    price?.currencySymbol + " " + price?.regularMarketPrice?.raw);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
