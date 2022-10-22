using System.Reflection;

namespace EmailTypes
{
    public class EmailType
    {
        public string ?sender { get; set; }
        public string ?recipient { get; set; }
        // public string ?subject { get; set; }
        // public string ?body { get; set; }

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
                    return false;
                }
            }
            return true;
        }
    }
}