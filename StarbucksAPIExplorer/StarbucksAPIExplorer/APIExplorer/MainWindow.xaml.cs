using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Web;
using System.Web.Script.Serialization;

namespace APIExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const String BASEURI = "https://test.openapi.starbucks.com/";
        string CLIENTID = "r4cjhbnkm352qgexrmyze5pu";
        string CLIENTSECRET = "vVT2skNTJHT62KzZSBTAPuZy";
        string APIKEY = "r4cjhbnkm352qgexrmyze5pu";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        
        private void ExecuteRequest()
        {
            if (String.IsNullOrEmpty(Token.Text))
            {
                ResourceLocator.Text = "Click on Get Access Token First.";
                return;
            }
            if (String.IsNullOrEmpty(ResourceLocator.Text))
            {
                ResourceLocator.Text = "Specify the request here such as \"Location/v1/stores?latlng=47.580760,-122.334750\"";
                return;
            }

            string uri = BASEURI + ResourceLocator.Text + "&access_token=" + Token.Text + "&apikey=" + APIKEY;
            Response.Text = GetResponse(WebRequestMethods.Http.Get, uri, null);
            return;
        }

        private void GetAccessTokenClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Token.Text))
            {
                string uri = BASEURI + "v1/oauth/token?grant_type=client_credentials&client_id=" + CLIENTID + "&client_secret=" + CLIENTSECRET + "&scope=test_scope" + "&api_key=" + APIKEY;
                string body = "grant_type=client_credentials&client_id=" + CLIENTID + "&client_secret=" + CLIENTSECRET + "&scope=test_scope&api_key=" + APIKEY;
                string ret = GetResponse(WebRequestMethods.Http.Post, uri, body);
                var serializer = new JavaScriptSerializer();
                var oToken = serializer.Deserialize<token>(ret);
                Token.Text = oToken.access_token;
            }
        }

        private string GetResponse(string method, string uri, string body)
        {
            string ret = string.Empty;

            // request
            WebRequest request = WebRequest.Create(uri);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = method;
            if (method == WebRequestMethods.Http.Post)
            {
                request.ContentLength = body.Length;
                var encoding = new System.Text.ASCIIEncoding();
                var bytes = encoding.GetBytes(body);
                using (var stream = request.GetRequestStream())
                    stream.Write(bytes, 0, bytes.Length);
            }

            // response
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    ret = reader.ReadToEnd();
            }
            catch (Exception e)
            {
                ret = e.Message;
            }

            return ret;
        }

        private void SubmitClicked(object sender, RoutedEventArgs e)
        {
            ExecuteRequest();
        }        
    }
}
