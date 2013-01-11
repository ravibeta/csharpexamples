using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;

namespace AWSwithWSDL
{
    public class SigningExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(SigningBehavior); }
        }


        protected override object CreateBehavior()
        {
            return new SigningBehavior(AccessKeyId, SecretKey);
        }

        [ConfigurationProperty("accessKeyId", IsRequired = true)]
        public string AccessKeyId
        {
            get { return (string)base["accessKeyId"]; }
            set { base["accessKeyId"] = value; }
        }

        [ConfigurationProperty("secretKey", IsRequired = true)]
        public string SecretKey
        {
            get { return (string)base["secretKey"]; }
            set { base["secretKey"] = value; }
        }
    }

    public class SigningBehavior : IEndpointBehavior
    {
		private string	_accessKeyId	= "";
		private string	_secretKey	= "";

        public SigningBehavior()
        {
            this._accessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
            this._secretKey = ConfigurationManager.AppSettings["secretKey"];
        }

        public SigningBehavior(string accessKeyId, string secretKey)
        {
			this._accessKeyId	= accessKeyId;
			this._secretKey		= secretKey;
		}

		public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime clientRuntime) {
            clientRuntime.MessageInspectors.Add(new SigningMessageInspector(_accessKeyId, _secretKey));
		}

		public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher) { return; }
		public void Validate(ServiceEndpoint serviceEndpoint) { return; }
		public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters) { return; }
    }

    public class SigningMessageInspector : IClientMessageInspector
    {
		private string	_accessKeyId	= "";
		private string	_secretKey	= "";

        public SigningMessageInspector(string accessKeyId, string secretKey)
        {
			this._accessKeyId	= accessKeyId;
			this._secretKey		= secretKey;
		}

		public object BeforeSendRequest(ref Message request, IClientChannel channel) {
			// prepare the data to sign
			string		operation		= Regex.Match(request.Headers.Action, "[^/]+$").ToString();
			DateTime	now				= DateTime.UtcNow;
			string		timestamp		= now.ToString("yyyy-MM-ddTHH:mm:ssZ");
			string		signMe			= operation + timestamp;
			byte[]		bytesToSign		= Encoding.UTF8.GetBytes(signMe);

			// sign the data
			byte[]		secretKeyBytes	= Encoding.UTF8.GetBytes(_secretKey);
			HMAC		hmacSha256		= new HMACSHA256(secretKeyBytes);
			byte[]		hashBytes		= hmacSha256.ComputeHash(bytesToSign);

    		string		signature		= Convert.ToBase64String(hashBytes);

			// add the signature information to the request headers
			request.Headers.Add(new AmazonHeader("AWSAccessKeyId", _accessKeyId));
			request.Headers.Add(new AmazonHeader("Timestamp", timestamp));
			request.Headers.Add(new AmazonHeader("Signature", signature));
            request.Headers.Add(new AmazonHeader("Service", "AWSECommerceService"));
            request.Headers.Add(new AmazonHeader("Operation", "ItemLookup"));

			return null;
		}

		public void AfterReceiveReply(ref Message reply, object correlationState) { }
    }

    public class AmazonHeader : MessageHeader
    {
        private string name;
        private string value;

        public AmazonHeader(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public override string Name { get { return name; } }
        public override string Namespace { get { return ConfigurationManager.AppSettings["amazonSecurityNamespace"]; } }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter xmlDictionaryWriter, MessageVersion messageVersion)
        {
            xmlDictionaryWriter.WriteString(value);
        }
    }
 
}
