using System.Windows.Forms;
using MailKit.Net.Smtp;
using MimeKit;

namespace Meow.Study.Demo.Helper
{
    /// <summary>
    /// EmailDemo
    /// </summary>
    public partial class EmailDemo : Form
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public EmailDemo()
        {
            InitializeComponent();
        }

        private void EmailDemo_Load(object sender, System.EventArgs e)
        {

        }


        private void Test()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", "joey@friends.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.friends.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("joey", "password");

                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}
