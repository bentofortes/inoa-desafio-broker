using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiHelper
{
    public class RequestResult
    {
        public bool valid_key { get; set; }
        public string ?by { get; set; }
        // public string ?by { get; set; }
    }

    public class Result
    {

    }

    public class Requests
    {
        public static async Task test(string asset)
        {
            string key = File.ReadAllText("credentials/hg_key.txt");

            var client = new HttpClient();
            string teste = await client.GetStringAsync("https://api.hgbrasil.com/finance/stock_price?fields=valid_key&key=" + key + "&symbol=" + asset);
            Console.WriteLine(teste);
            teste = await client.GetStringAsync("https://api.hgbrasil.com/finance/stock_price?key=" + key + "&symbol=" + asset + "gsdfsdfd");
            Console.WriteLine(teste);
            teste = await client.GetStringAsync("https://api.hgbrasil.com/finance/stock_price?key=" + key + "&symbol=" + asset);
            Console.WriteLine(teste);

            RequestResult? result = JsonSerializer.Deserialize<RequestResult>(teste);
            Console.WriteLine(result?.GetType().GetProperties().Length);

        }
    }
}
