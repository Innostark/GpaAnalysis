using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Text;

namespace TMD.Web.PayPal
{
    public class NVPAPICaller
    {
        private string pendpointurl = "https://api-3t.paypal.com/nvp";
        private const string CVV2 = "CVV2";

        private string returnURL = "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["SiteURL"] + "/" + "Account/Thankyou";
        private string cancelURL = "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["SiteURL"] + "/" + "Home/Index";
        private string ipnURL = "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["SiteURL"] + "/" + "Account/PayPalIPN";

        //Flag that determines the PayPal environment (live or sandbox)
        private const bool bSandbox = true;

        private const string SIGNATURE = "SIGNATURE";
        private const string PWD = "PWD";
        private const string ACCT = "ACCT";

        //Replace <API_USERNAME> with your API Username
        //Replace <API_PASSWORD> with your API Password
        //Replace <API_SIGNATURE> with your Signature
        //public string APIUsername = "testinnostark1_api1.yahoo.com";
        //private string APIPassword = "ZBFQV7LHJZT9BFPM";
        //private string APISignature = "AFcWxV21C7fd0v3bYYYRCpSSRl31A.AD2K.OHlo3JnVrmtC9mVDxFqiP"; 

        public string APIUsername = "testinnostark2_api1.gmail.com";
        private string APIPassword = "Z4TFF8LH7AZFK7DU";
        private string APISignature = "AFcWxV21C7fd0v3bYYYRCpSSRl31AdyeZRdjd2epR7.DVwL4W8N7TfO2"; 

        
        private string Subject = "";
        private string BNCode = "PP-ECWizard";

        //HttpWebRequest Timeout specified in milliseconds 
        private const int Timeout = 10000;
        private static readonly string[] SECURED_NVPS = new string[] { ACCT, CVV2, SIGNATURE, PWD };


        /// <summary>
        /// Sets the API Credentials
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="Pwd"></param>
        /// <param name="Signature"></param>
        /// <returns></returns>
        public void SetCredentials(string Userid, string Pwd, string Signature)
        {
            APIUsername = Userid;
            APIPassword = Pwd;
            APISignature = Signature;
        }

        public bool ExpressCheckout(string name, string description, string price, string quantity, string currency,string email, ref string token, ref string retMsg)
        {

            string host = "www.paypal.com";
            if (bSandbox)
            {
                pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
                host = "www.sandbox.paypal.com";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "SetExpressCheckout";
            encoder["RETURNURL"] = returnURL;
            encoder["CANCELURL"] = cancelURL;
            encoder["NOTIFYURL"] = ipnURL;
            encoder["SOLUTIONTYPE"] = "Sole";
            encoder["PAYMENTREQUEST_0_CUSTOM"] = email;
            encoder["LANDINGPAGE"] = "Billing";
            encoder["USERSELECTEDFUNDINGSOURCE"] = "CreditCard";


            double dblQuantity = Convert.ToDouble(quantity);
            double dblPrice = Convert.ToDouble(price);
            double totalPrice = dblQuantity * dblPrice;

            encoder["L_PAYMENTREQUEST_0_NAME0"] = name;
            encoder["L_PAYMENTREQUEST_0_DESC0"] = description;
            encoder["L_PAYMENTREQUEST_0_AMT0"] = price;
            encoder["L_PAYMENTREQUEST_0_QTY0"] = quantity;

            encoder["PAYMENTREQUEST_0_AMT"] = totalPrice.ToString();
            encoder["PAYMENTREQUEST_0_ITEMAMT"] = totalPrice.ToString();
            encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "SALE";
            encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = currency;

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                token = decoder["TOKEN"];

                string ECURL = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token + "&useraction=COMMIT";

                retMsg = ECURL;
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }

        /// <summary>
        /// GetShippingDetails: The method that calls SetExpressCheckout API, invoked from the 
        /// Billing Page EC placement
        /// </summary>
        /// <param name="token"></param>
        /// <param ref name="retMsg"></param>
        /// <returns></returns>
        public bool GetDetails(string token, ref NVPCodec decoder, ref string retMsg)
        {

            if (bSandbox)
            {
                pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "GetExpressCheckoutDetails";
            encoder["TOKEN"] = token;

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }


        /// <summary>
        /// ConfirmPayment: The method that calls SetExpressCheckout API, invoked from the 
        /// Billing Page EC placement
        /// </summary>
        /// <param name="token"></param>
        /// <param ref name="retMsg"></param>
        /// <returns></returns>
        public bool ConfirmPayment(string finalPaymentAmount, string token, string PayerId, string currency, ref NVPCodec decoder, ref string retMsg)
        {
            if (bSandbox)
            {
                pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "DoExpressCheckoutPayment";
            encoder["TOKEN"] = token;
            encoder["PAYMENTACTION"] = "Sale";
            encoder["PAYERID"] = PayerId;
            encoder["AMT"] = finalPaymentAmount;
            encoder["CURRENCYCODE"] = currency;

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }


        /// <summary>
        /// HttpCall: The main method that is used for all API calls
        /// </summary>
        /// <param name="NvpRequest"></param>
        /// <returns></returns>
        public string HttpCall(string NvpRequest) //CallNvpServer
        {
            string url = pendpointurl;

            //To Add the credentials from the profile
            string strPost = NvpRequest + "&" + buildCredentialsNVPString();
            strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode(BNCode);

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Timeout = Timeout;
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;

            try
            {
                using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
                {
                    myWriter.Write(strPost);
                }
            }
            catch (Exception e)
            {
              //do some error handling
            }

            //Retrieve the Response returned from the NVP API call to PayPal
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            string result;
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Credentials added to the NVP string
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private string buildCredentialsNVPString()
        {
            NVPCodec codec = new NVPCodec();

            if (!IsEmpty(APIUsername))
                codec["USER"] = APIUsername;

            if (!IsEmpty(APIPassword))
                codec["PWD"] = APIPassword;

            if (!IsEmpty(APISignature))
                codec["SIGNATURE"] = APISignature;

            if (!IsEmpty(Subject))
                codec["SUBJECT"] = Subject;

            codec["VERSION"] = "84.0";

            return codec.Encode();
        }

        /// <summary>
        /// Returns if a string is empty or null
        /// </summary>
        /// <param name="s">the string</param>
        /// <returns>true if the string is not null and is not empty or just whitespace</returns>
        public static bool IsEmpty(string s)
        {
            return s == null || s.Trim() == string.Empty;
        }
    }


    public sealed class NVPCodec : NameValueCollection
    {
        private const string AMPERSAND = "&";
        private const string EQUALS = "=";
        private static readonly char[] AMPERSAND_CHAR_ARRAY = AMPERSAND.ToCharArray();
        private static readonly char[] EQUALS_CHAR_ARRAY = EQUALS.ToCharArray();

        /// <summary>
        /// Returns the built NVP string of all name/value pairs in the Hashtable
        /// </summary>
        /// <returns></returns>
        public string Encode()
        {
            StringBuilder sb = new StringBuilder();
            bool firstPair = true;
            foreach (string kv in AllKeys)
            {
                string name = HttpUtility.UrlEncode(kv);
                string value = HttpUtility.UrlEncode(this[kv]);

                if (!firstPair)
                {
                    sb.Append(AMPERSAND);
                }
                sb.Append(name).Append(EQUALS).Append(value);
                firstPair = false;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Decoding the string
        /// </summary>
        /// <param name="nvpstring"></param>
        public void Decode(string nvpstring)
        {
            Clear();
            foreach (string nvp in nvpstring.Split(AMPERSAND_CHAR_ARRAY))
            {
                string[] tokens = nvp.Split(EQUALS_CHAR_ARRAY);
                if (tokens.Length >= 2)
                {
                    string name = HttpUtility.UrlDecode(tokens[0]);
                    string value = HttpUtility.UrlDecode(tokens[1]);
                    Add(name, value);
                }
            }
        }


        #region Array methods
        public void Add(string name, string value, int index)
        {
            this.Add(GetArrayName(index, name), value);
        }

        public void Remove(string arrayName, int index)
        {
            this.Remove(GetArrayName(index, arrayName));
        }

        /// <summary>
        /// 
        /// </summary>
        public string this[string name, int index]
        {
            get
            {
                return this[GetArrayName(index, name)];
            }
            set
            {
                this[GetArrayName(index, name)] = value;
            }
        }

        private static string GetArrayName(int index, string name)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", "index can not be negative : " + index);
            }
            return name + index;
        }
        #endregion
    }
}