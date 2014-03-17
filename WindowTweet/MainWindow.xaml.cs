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
using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using System.Web;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WindowTweet
{
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

        public static string auth()
        {
            WebClient wcAuth = new WebClient();
            wcAuth.Headers["Host"] = "api.twitter.com";
            wcAuth.Headers["User-Agent"] = "Windows Search Tweets";
            wcAuth.Headers["Authorization"] = "Basic " + Base64Encode("F2Ll18JPHROTclBPabbQ:T55syYJQlFYzbDLc2LjKdr3TQJNalvYv9OSINWIBk0");

            NameValueCollection auth = new NameValueCollection();
            auth["grant_type"] = "client_credentials";

            string accessToken = System.Text.Encoding.Default.GetString(wcAuth.UploadValues("https://api.twitter.com/oauth2/token", "POST", auth));
            accessToken = (new JavaScriptSerializer().DeserializeObject(accessToken) as Dictionary<string, object>)["access_token"].ToString();
            return accessToken;
        }

        public static IEnumerable<Tweet> search(string word)
        {
            WebClient wc = new WebClient();
            wc.Headers["Authorization"] = "Bearer " + auth();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string tweets = wc.DownloadString("https://api.twitter.com/1.1/search/tweets.json?q=" + word + "&f=realtime");
            object[] tweetslist = (jss.DeserializeObject(tweets) as Dictionary<string, object>)["statuses"] as object[];
            foreach (var item in tweetslist)
            {
                var tweet = item as Dictionary<string, object>;
                yield return new Tweet() { userTweet = tweet["text"].ToString(), userImage = (tweet["user"] as Dictionary<string, object>)["profile_image_url"].ToString() };
            }
        }

        private void refreshMe(object sender, RoutedEventArgs e)
        {
            string word = searchEngine.Text;
            tweetsBox.DataContext = search(word);
        }

        private void search(object sender, RoutedEventArgs e)
        {
            string word = searchEngine.Text;
            foreach (var tweet in search(word))
                if (!tweets.Items.Contains(tweet))
                    tweets.Items.Insert(0,tweet);
        }
    }
}

public class Tweet
{
    public string userTweet { get; set; }
    public string userImage { get; set; }
}