using System.Threading;

using ApiHelper;

namespace Inoa{

    class DesafioBroker{

        private static async Task Main(string[] args){

            if (args.Length != 3){
                Console.WriteLine("\nUso correto:");
                Console.WriteLine("inoa-desafio-broker.exe <NOME_DO_ATIVO> <PRECO_LIMITE_INFERIOR> <PRECO_LIMITE_SUPERIOR>\n");
                Environment.Exit(0);
            }
            try
            {
                float minPrice = float.Parse(args[1]);
                float maxPrice = float.Parse(args[2]);
            }
            catch
            {
                Console.WriteLine("\nUso correto:");
                Console.WriteLine("inoa-desafio-broker.exe <NOME_DO_ATIVO> <PRECO_LIMITE_INFERIOR> <PRECO_LIMITE_SUPERIOR>\n");
                Console.WriteLine("Os valores de preco devem conter apenas numeros e \".\" ou \",\"\n");
                Environment.Exit(0);
            }

            await Requests.getPrice(args[0]);
            while(true)
            {
                Thread.Sleep(5000);
                await Requests.getPrice(args[0]);
            }
        }
    }
}