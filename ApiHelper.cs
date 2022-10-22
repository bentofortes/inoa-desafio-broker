using System.Text.Json;

using EmailHelper;
using ResponseTypes;


namespace ApiHelper{

    public class Requests{

        public float ?currentPrice;
        public Price ?price;

        public async Task getPrice(string assetName, float sellPrice, float buyPrice){
            var client = new HttpClient();
            RequestResponse ?response;
            
            try{
                HttpResponseMessage request = await client.GetAsync($"https://query1.finance.yahoo.com/v11/finance/quoteSummary/{assetName}.sa?modules=price");
                string responseString = await request.Content.ReadAsStringAsync();

                response = JsonSerializer.Deserialize<RequestResponse>(responseString);
                if (response?.quoteSummary?.error?.code == "Not Found"){
                    Console.WriteLine("Erro. Ativo nao encontrado\n");
                    Environment.Exit(0);
                }

                currentPrice = response?.quoteSummary?.result?[0].price?.regularMarketPrice?.raw;
                price = response?.quoteSummary?.result?[0].price;

                Console.WriteLine("{0} {1, 20} {2, 20}",
                    DateTime.Now.ToString("G"),
                    price?.symbol,
                    price?.currencySymbol + " " + currentPrice);
                
                if (currentPrice <= buyPrice || currentPrice >= sellPrice){
                    Email email = new Email();
                    Console.WriteLine("Enviando Email...");
                    email.sendEmail(assetName, currentPrice, sellPrice, buyPrice);
                }
            }
            catch (Exception ex){
                Console.WriteLine(ex);
            }
        }
    }
}
