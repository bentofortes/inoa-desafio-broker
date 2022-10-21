using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.Json;

namespace EmailHelper
{
    public class EmailType{
        public string ?sender { get; set; }
        public string ?recipient { get; set; }
        public string ?subject { get; set; }
        public string ?body { get; set; }

        public int port { get; set; }
        public string ?host { get; set; }
        public string ?smtpUsername { get; set; }
        public string ?smtpPassword { get; set; }

        // public void writeBody(string assetName)
        static public bool nullCheck(EmailType email)
        {
            foreach(PropertyInfo p in email.GetType().GetProperties())
            {
                Console.WriteLine(p.GetValue(email));
                if (p.GetValue(email) == null)
                {
                    Console.WriteLine(p.Name + " is null");
                    return false;
                }
            }
            return true;
        }
    }

    public class Email
    {
        EmailType ?settings;

        public void readConfig(){
            if (!File.Exists("credentials/config.json")) return;

            string aux = File.ReadAllText("credentials/config.json");
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

        public void sendEmail()
        {
            Console.WriteLine("Email sent1!");
            readConfig();
            readCredentials();
            if (settings == null) return;
            // if (!EmailType.nullCheck(settings)) return;

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