using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Web.Configuration;

namespace FinalG3FoodOrderingSystem.Models
{
    public class PDTHolder
    {
        public double GrossTotal { set; get; }
        public int InvoiceNumber { set; get; }
        public string PaymentStatus { set; get; }
        public string PayerFirstName { set; get; }
        public string PayerLastName { set; get; }
        public double PaymentFee { set; get; }
        public string BusinessEmail { set; get; }
        public string PayerEmail { set; get; }
        public string TxToken { set; get; }
        public string ReceiverEmail { set; get; }
        public string ItemName { set; get; }
        public string Currency { set; get; }
        public string TransactionId { set; get; }
        public string SubscriberId { set; get; }
        public string Custom { set; get; }

        private static string authToken, txToken, query, strResponse;

        public static PDTHolder Success(string tx)
        {
            authToken = WebConfigurationManager.AppSettings["PDTToken"];
            txToken = tx;
            query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);
            string url = WebConfigurationManager.AppSettings["PayPalSubmitUrl"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();

            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
            if (strResponse.StartsWith("SUCCESS"))
                return PDTHolder.Parse(strResponse);
            return null;
        }
        private static PDTHolder Parse(string postData)
        {
            string sKey, sValue;
            PDTHolder ph = new PDTHolder();
            try
            {
                String[] StringArray = postData.Split('\n');
                int i;
                for (i = 1; i < StringArray.Length - 1; i++)
                {
                    String[] StringArray1 = StringArray[i].Split('=');
                    sKey = StringArray1[0];
                    sValue = HttpUtility.UrlDecode(StringArray1[1]);
                    switch (sKey)
                    {
                        case "mc_gross":
                            ph.GrossTotal = Convert.ToDouble(sValue);
                            break;
                        case "invoice":
                            ph.InvoiceNumber = Convert.ToInt32(sValue);
                            break;
                        case "payment_status":
                            ph.PaymentStatus = Convert.ToString(sValue);
                            break;
                        case "first_name":
                            ph.PayerFirstName = Convert.ToString(sValue);
                            break;
                        case "lastname":
                            ph.PayerLastName = Convert.ToString(sValue);
                            break;
                        case "mc_fee":
                            ph.PaymentFee = Convert.ToDouble(sValue);
                            break;
                        case "business":
                            ph.BusinessEmail = Convert.ToString(sValue);
                            break;
                        case "payer_email":
                            ph.PayerEmail = Convert.ToString(sValue);
                            break;
                        case "tx token":
                            ph.TxToken = Convert.ToString(sValue);
                            break;
                        case "receiver_email":
                            ph.ReceiverEmail = Convert.ToString(sValue);
                            break;
                        case "item_name":
                            ph.ItemName = Convert.ToString(sValue);
                            break;
                        case "mc_currency":
                            ph.Currency = Convert.ToString(sValue);
                            break;
                        case "txn_id":
                            ph.TransactionId = Convert.ToString(sValue);
                            break;
                        case "custom":
                            ph.Custom = Convert.ToString(sValue);
                            break;
                        case "subscr_id":
                            ph.SubscriberId = Convert.ToString(sValue);
                            break;
                    }

                }
                return ph;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

    
