namespace EmailTypes{

    public class EmailType{
        public string ?sender { get; set; }
        public string ?recipient { get; set; }

        public int port { get; set; }
        public string ?host { get; set; }
        public string ?smtpUsername { get; set; }
        public string ?smtpPassword { get; set; }
    }
}