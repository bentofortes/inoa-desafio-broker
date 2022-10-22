using System.Net;
using System.Net.Mail;
using System.Text.Json;

using EmailTypes;

namespace EmailHelper
{
    public class Email
    {
        EmailType ?settings;

        public void readConfig(){
            if (!File.Exists("config.json")) return;

            string aux = File.ReadAllText("config.json");
            settings = JsonSerializer.Deserialize<EmailType>(aux);
        }

        public void readCredentials(){
            if (!File.Exists("credentials/aws_ses_credentials.csv")) return;

            string[] aux = File.ReadAllLines("credentials/aws_ses_credentials.csv");
            aux = aux[1].Split(",");

            if (settings != null){
                settings.smtpUsername = aux[1];
                settings.smtpPassword = aux[2];
            }
        }

        public void sendEmail(string assetName, int price, int max, int min)
        {
            readConfig();
            readCredentials();

            if (
                // settings.subject == null ||
                // settings.body == null ||
                settings == null ||
                settings.sender == null ||
                settings.recipient == null
            ) return;

            MailMessage email = new MailMessage();
            email.From = new MailAddress(settings.sender, "Desafio Inoa");
            email.To.Add(new MailAddress(settings.recipient));
            email.Subject = "gsdfgsfgsdfsdfdsdfsdf";
            email.Body = "AAAAAAAAAAAAAAAAAAAA";


            using (var client = new System.Net.Mail.SmtpClient(settings.host, settings.port))
            {
                client.Credentials = new NetworkCredential(settings.smtpUsername, settings.smtpPassword);
                client.EnableSsl = true;
                client.Send(email);
                Console.WriteLine("Email sent!");
            }
        }
    }
}