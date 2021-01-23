using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace PL
{
    /// <summary>
    /// Interaction logic for ContactUsPage.xaml
    /// </summary>
    public partial class ContactUsPage : Page
    {
        public ContactUsPage()
        {
            InitializeComponent();
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            int flag = 1;

            if (txtYourMail.Text.Trim().Length == 0)
            {
                flag = 0;
                txtbTo.Text = "Required";
                txtYourMail.Focus();
            }
            else if (!Regex.IsMatch(txtYourMail.Text, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
            {
                flag = 0;
                txtbTo.Text = "Invalid";
                txtYourMail.Focus();
            }
            else
            {
                flag = 1;
            }
            if (txtSubject.Text.Trim().Length == 0)
            {
                flag = 0;
                txtbSubject.Text = "Required";
                txtSubject.Focus();
            }
            if (txtContent.Text.Trim().Length == 0)
            {
                flag = 0;
                txtbContent.Text = "Required";
                txtContent.Focus();
            }
            if (flag == 1)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("netivimcompany@gmail.com");
                mail.From = new MailAddress(txtYourMail.Text);
                mail.Subject = txtSubject.Text;
                mail.Body = txtContent.Text;
                mail.IsBodyHtml = true;

                MailMessage answer = new MailMessage();
                answer.To.Add(txtYourMail.Text);
                answer.From = new MailAddress("netivimcompany@gmail.com", "חברת נתיבים");
                answer.Subject = "קבלת פנייה";
                answer.Body = "שלום רב! הודעתך התקבלה בהצלחה ותועבר לטיפול. תודה על פנייתך ויום טוב!";
                answer.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("netivimcompany@gmail.com", "100project");
                smtp.EnableSsl = true;
                txtbTo.Text = "";
                txtbSubject.Text = "";
                txtbContent.Text = "";
                try
                {
                    smtp.Send(mail);
                    MessageBox.Show("Your message has been successfully sent!", "Contact Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtYourMail.Text = "";
                    txtSubject.Text = "";
                    txtContent.Text = "";
                    smtp.Send(answer);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtYourMail.Text = "";
            txtSubject.Text = "";
            txtContent.Text = "";
            txtYourMail.Focus();

            txtbTo.Text = "";
            txtbSubject.Text = "";
            txtbContent.Text = "";
        }
    }
}
