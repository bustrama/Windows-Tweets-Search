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

        public void auth()
        {
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/x-www-form-urlencoded;charset=UTF-8";

            NameValueCollection auth = new NameValueCollection();
            auth["Header"] = "POST /oauth2/token HTTP/1.1";
            auth["userAgent"] = "Windows Tweets Search v1.0";
            auth["autorization"] = "Basic eHZ6MWV2R ... o4OERSZHlPZw==";
            auth["contentLength"] = "29";
            auth["acceptEncoding"] = "gzip";

            wc.UploadValues("https://api.twitter"+".com/oauth2/token", "POST", auth);
        }

        private void refreshMe(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
