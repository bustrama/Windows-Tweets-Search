using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowTweet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }

        public void auth()
        {
            WebClient wc = new WebClient();
            wc.Headers["Host"] = "api.twitter.com";
            wc.Headers["Content-Type"] = "application/x-www-form-urlencoded;charset=UTF-8";
            wc.Headers["User-Agent"] = "Windows Tweets Search v1.0";
            wc.Headers["Content-Length"] = "29";

            NameValueCollection auth = new NameValueCollection();
            auth["Authorization"] = "Basic " + Base64Encode("F2Ll18JPHROTclBPabbQ:T55syYJQlFYzbDLc2LjKdr3TQJNalvYv9OSINWIBk0");
            auth["grant_type"] = "client_credentials";

            wc.UploadValues("https://api.twitter.com/oauth2/token", "POST", auth);
        }

        private void refreshMe(object sender, RoutedEventArgs e)
        {
            auth();
        }
    }
}
