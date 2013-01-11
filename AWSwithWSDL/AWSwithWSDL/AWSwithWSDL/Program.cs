using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using AWSwithWSDL.AWSService;
using System.ServiceModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

namespace AWSwithWSDL
{
    public class Program
    {
        private const string MY_AWS_ACCESS_KEY_ID = "your_access_key_id";
        private const string MY_AWS_SECRET_KEY = "your_secret_key";
        private const string DESTINATION = "ecs.amazonaws.com";

        private const string URL = "https://webservices.amazon.com/AWSECommerceService/2011-08-01";
        private const string ITEM_ID = "0545010225";

        public static void Main()
        {
            using (var client = new AWSECommerceServicePortTypeClient("AWSECommerceServicePort"))
            {

                ItemLookupRequest req = new ItemLookupRequest();
                req.ItemId = new string[] { "0545010225" };

                var itemLookup = new ItemLookup();
                itemLookup.AssociateTag = "ws";
                itemLookup.AWSAccessKeyId = "your_access_key_id";
                itemLookup.Request = new ItemLookupRequest[] { req };
                var resp = client.ItemLookup(itemLookup);
                foreach (var item in resp.Items[0].Item)
                {
                    Console.WriteLine(item.ItemAttributes.Title);
                }
                Console.WriteLine("done...enter any key to continue>");
                Console.ReadLine();
            }

            System.Console.WriteLine("Hit Enter to end");
            System.Console.ReadLine();
        }
    }
}
