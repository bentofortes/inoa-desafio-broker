using ApiHelper;


namespace Inoa{

    class DesafioBroker{

        private async static Task Main(string[] args){

            if (args.Length != 3){
                Console.WriteLine("\nUso correto:");
                Console.WriteLine("inoa-desafio-broker.exe <NOME_DO_ATIVO> <PRECO_VENDA> <PRECO_COMPRA>\n");
                Environment.Exit(0);
            }

            try{
                float.Parse(args[1]);
                float.Parse(args[2]);
            }
            catch{
                Console.WriteLine("\nUso correto:");
                Console.WriteLine("inoa-desafio-broker.exe <NOME_DO_ATIVO> <PRECO_VENDA> <PRECO_COMPRA>\n");
                Console.WriteLine("Os valores de preco devem conter apenas numeros e \".\" ou \",\"\n");
                Environment.Exit(0);
            }

            Requests worker = new Requests();
            if (worker == null) return;
            Console.WriteLine();

            await worker.getPrice(args[0], float.Parse(args[1]), float.Parse(args[2]));
            while(true){
                Thread.Sleep(5000); // atualiza de 5 em 5 segundos o preco do ativo
                await worker.getPrice(args[0], float.Parse(args[1]), float.Parse(args[2]));
            }
        }
    }
}