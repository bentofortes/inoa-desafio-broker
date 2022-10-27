using System.Net;
using System.Net.Mail;
using System.Text.Json;

using EmailTypes;

namespace EmailHelper{
    public class Email{
        EmailType ?settings;

        public void readConfig(){
            if (!File.Exists("config.json")){
                Console.WriteLine("O arquivo config nao existe. Fechando o programa...");
                Environment.Exit(0);
            }

            string aux = File.ReadAllText("config.json");
            settings = JsonSerializer.Deserialize<EmailType>(aux);
        }

        public void readCredentials(){
            if (!File.Exists("credentials/aws_ses_credentials.csv")){
                Console.WriteLine("O arquivo das credenciais do email nao existe. Fechando o programa...");
                Environment.Exit(0);
            }

            string[] aux = File.ReadAllLines("credentials/aws_ses_credentials.csv");
            aux = aux[1].Split(",");

            if (settings != null){
                settings.smtpUsername = aux[1];
                settings.smtpPassword = aux[2];
            }
        }

        public void sendEmail(string assetName, float ?price, float sell, float buy){
            readConfig();
            readCredentials();

            if (
                settings == null ||
                settings.sender == null ||
                settings.recipient == null
            ) return;

            MailMessage email = new MailMessage();
            email.From = new MailAddress(settings.sender, "Desafio Inoa Broker");
            email.To.Add(new MailAddress(settings.recipient));

            if (price <= buy){
                email.Subject = $"Preco de {assetName.ToUpper()} abaixo ou igual ao valor de compra definido.";
                email.Body = @$"{DateTime.Now.ToString("G")}  -  No atual momento o preco do ativo {assetName.ToUpper()} eh {price}. O preco de compra definido foi de {buy}";
            }
            if (price >= sell){
                email.Subject = $"Preco de {assetName.ToUpper()} acima ou igual ao valor de venda definido.";
                email.Body = @$"{DateTime.Now.ToString("G")}  -  No atual momento o preco do ativo {assetName.ToUpper()} eh {price}. O preco de venda definido foi de {sell}";
            }


            using (var client = new System.Net.Mail.SmtpClient(settings.host, settings.port)){
                client.Credentials = new NetworkCredential(settings.smtpUsername, settings.smtpPassword);
                client.EnableSsl = true;

                try{
                    client.Send(email);
                    Console.WriteLine("Email enviado");
                    Console.WriteLine($"Pressione Enter para continuar monitorando o ativo {assetName.ToUpper()} ou Ctrl+C para sair.");
                    Console.ReadLine();
                }
                catch (Exception ex){
                    Console.WriteLine(ex);
                    Environment.Exit(0);
                }
            }
        }
    }
}