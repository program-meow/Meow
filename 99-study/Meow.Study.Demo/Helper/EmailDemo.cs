using System;
using System.Windows.Forms;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
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
            textBox1.Text = "";
        }

        /// <summary>
        /// SMTP
        /// </summary>
        private void button1_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Text += "SMTP：\r\n";
            textBox1.Text += "开始发送邮件...\r\n";
            try
            {
                SmtpSend();
            }
            catch (Exception exception)
            {
                textBox1.Text += exception.Message;
            }
        }

        /// <summary>
        /// POP3
        /// </summary>
        private void button2_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Text += "POP3：\r\n";
            textBox1.Text += "开始发送邮件...\r\n";
            try
            {
                Pop3Send();
            }
            catch (Exception exception)
            {
                textBox1.Text += exception.Message;
            }
        }

        /// <summary>
        /// IMAP
        /// </summary>
        private void button3_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Text += "IMAP：\r\n";
            textBox1.Text += "开始发送邮件...\r\n";
            try
            {
                ImapSend();
            }
            catch (Exception exception)
            {
                textBox1.Text += exception.Message;
            }
        }

        /// <summary>
        /// SMTP发送
        /// </summary>
        private void SmtpSend()
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
                client.Connect("smtp.163.com", 25, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("name", "password");

                client.Send(message);
                client.Disconnect(true);
            }

            textBox1.Text += "发送成功...";
        }

        /// <summary>
        /// POP3发送
        /// </summary>
        private void Pop3Send()
        {
            using (var client = new Pop3Client())
            {
                client.Connect("pop.friends.com", 110, false);

                client.Authenticate("joey", "password");

                for (int i = 0; i < client.Count; i++)
                {
                    var message = client.GetMessage(i);
                    textBox1.Text += string.Format("Subject: {0}\r\n", message.Subject);
                }

                client.Disconnect(true);
            }

            textBox1.Text += "发送成功...";
        }

        /// <summary>
        /// IMAP发送
        /// </summary>
        private void ImapSend()
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.friends.com", 993, true);

                client.Authenticate("joey", "password");

                // The Inbox folder is always available on all IMAP servers...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                textBox1.Text += string.Format("Total messages: {0}\r\n", inbox.Count);
                textBox1.Text += string.Format("Recent messages: {0}\r\n", inbox.Recent);

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i);
                    textBox1.Text += string.Format("Subject: {0}\r\n", message.Subject);
                }

                client.Disconnect(true);
            }

            textBox1.Text += "发送成功...";
        }
    }
}
