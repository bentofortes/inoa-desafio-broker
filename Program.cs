using System.IO;
using System.Threading.Tasks;

using ApiHelper;

namespace Inoa{

    class DesafioBroker{

        private static async Task Main(string[] args){

            if (args.Length != 3){
                Console.WriteLine("\nUso correto:");
                Console.WriteLine("inoa-desafio-broker.exe <NOME_DO_ATIVO> <PRECO_LIMITE_INFERIOR> <PRECO_LIMITE_SUPERIOR>\n");
            }

            await Requests.test(args[0]);
        }
    }
}
