using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pesapal.APIHelper;
using System.Web;
using iPrint;

namespace ePayment
{
    public class PesaPal
    {
                                     
        private String ConsumerKey;
        private String ConsumerSecret;
        private String _pesapalPostUri;
        private String _pesapalQueryPaymentStatusUri;
        private String StationNumber = "UG00101";        
        private decimal Cost;

        private String pesapal_transaction_tracking_id;
        private String pesapal_merchant_reference;
        private String firstName;
        private String lastName;
        private String phoneNumber;

        public PesaPal(bool realAccount)
        {
            if (realAccount)
            {
                ConsumerKey = "ll2VYsSDOpj/qJlIxpurfFgsyJ5f9fOL";
                ConsumerSecret = "k2VJMqZQfL218aHwC1qEE+WIOag=";
                _pesapalPostUri = "https://www.pesapal.com/API/PostPesapalDirectOrderV4";
                _pesapalQueryPaymentStatusUri = "https://www.pesapal.com/api/querypaymentstatus";
            }
            else
            {
                ConsumerKey = "JV7CI78QzuTa0m/zCKKsOcc1BzvLeygt";
                ConsumerSecret = "PrnOeqr0Qh0bZ8/vI4LjTRIlrsI=";
                _pesapalPostUri = "http://demo.pesapal.com/API/PostPesapalDirectOrderV4";
                _pesapalQueryPaymentStatusUri = "http://demo.pesapal.com/api/querypaymentstatus";
            }

        }


        public decimal TotalCost
        {
            get { return Cost; }
            set { Cost = value; }
        }

        public String ePay_transaction_tracking_id
        {
            get { return pesapal_transaction_tracking_id; }
            set { pesapal_transaction_tracking_id = value; }
        }

        public String ePay_merchant_reference
        {
            get { return pesapal_merchant_reference; }
            set { pesapal_merchant_reference = value; }
        }

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public String PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string GetPesapalUrl()
        {
            Uri pesapalPostUri = new Uri(_pesapalPostUri);
                        
            Uri pesapalCallBackUri = new Uri("https://www.google.com/");
            // Setup builder
            IBuilder builder = new APIPostParametersBuilderV2()
                                .ConsumerKey(ConsumerKey)
                                .ConsumerSecret(ConsumerSecret)
                                .OAuthVersion(EOAuthVersion.VERSION1)
                                .SignatureMethod(ESignatureMethod.HMACSHA1)
                                .SimplePostHttpMethod(EHttpMethod.GET)
                                .SimplePostBaseUri(pesapalPostUri)
                                .OAuthCallBackUri(pesapalCallBackUri);
            // Initialize API helper
            APIHelper<IBuilder> helper = new APIHelper<IBuilder>(builder);
            // Populate line items
            var lineItems = new List<LineItem> { };
            // For each item purchased, add a lineItem.
            // For example, if the user purchased 3 of Item A, add a line item as follows:

            var lineItem = new LineItem
            {
                Particulars = "Photo Printing",
                UniqueId = StationNumber,
                Quantity = 1,
                UnitCost = Cost
            };
            lineItem.SubTotal = (lineItem.Quantity * lineItem.UnitCost);
            lineItems.Add(lineItem);
            // Do the same for additional items purchased ...
            // Compose the order
            PesapalDirectOrderInfo webOrder = new PesapalDirectOrderInfo()
            {
                Amount = (lineItems.Sum(x => x.SubTotal)).ToString(),
                Description = "Photo Printing",
                Type = "MERCHANT",
                Reference = GenerateReference(),
                Email = "zsquresh@gmail.com",
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                LineItems = lineItems
            };
            // Post the order to PesaPal, which upon successful verification,
            // will return the string containing the url to load in the iframe
            return helper.PostGetPesapalDirectOrderUrl(webOrder);
        }

        private String GenerateReference()
        {
            String Reference;
            Reference = StationNumber + DateTime.Now.ToString("HHmmssddMMyy");
            return Reference;
        }
        public String UpdateIpnTransactionStatus(string pesapal_tracking_id, string reference)
        {
            Uri pesapalQueryPaymentStatusUri = new Uri(_pesapalQueryPaymentStatusUri);
            
            // Setup builder
            IBuilder builder = new APIPostParametersBuilder()
                                .ConsumerKey(ConsumerKey)
                                .ConsumerSecret(ConsumerSecret)
                                .OAuthVersion(EOAuthVersion.VERSION1)
                                .SignatureMethod(ESignatureMethod.HMACSHA1)
                                .SimplePostHttpMethod(EHttpMethod.GET)
                                .SimplePostBaseUri(pesapalQueryPaymentStatusUri);

            // Initialize API helper           
            APIHelper<IBuilder> helper = new APIHelper<IBuilder>(builder);
            try
            {
                // query pesapal for status >> format of the result is
                //pesapal_response_data=<PENDING|COMPLETED|FAILED|INVALID>
                string result = helper.PostGetQueryPaymentStatus(pesapal_tracking_id, reference);
                string[] resultParts = result.Split(new char[] { '=' });
                string paymentStatus = resultParts[1]; /* Possible values: PENDING, COMPLETED, FAILED or INVALID*/

                return paymentStatus;
            }
            catch (Exception ex)
            {
                clsGlobalFunctions.ErrorLog(ex); //Log Error
                return "Error";
            }
        }

        public void extraxtPesaPalResponse(String pesaPalResponse) //CheckPaymentStatus
        {

            String param1 = "?pesapal_transaction_tracking_id=";
            String param2 = "&pesapal_merchant_reference=";

            int pos_Param1 = pesaPalResponse.IndexOf(param1) + param1.Length;
            int end_Param1 = pesaPalResponse.IndexOf(param2);

            int pos_Param2 = end_Param1 + param2.Length;
            int end_Param2 = pesaPalResponse.Length;

            pesapal_transaction_tracking_id = pesaPalResponse.Substring(pos_Param1, end_Param1 - pos_Param1);

            pesapal_merchant_reference = pesaPalResponse.Substring(pos_Param2, end_Param2 - pos_Param2);

            //StoreTransactionRef();
           // timTransactionStatus.Enabled = true;
        }

     /*   private void StoreTransactionRef()
        {
            //textBox4.Text = pesapal_transaction_tracking_id + "]  [" + pesapal_merchant_reference;
        }*/

        public String QueryPaymentStatusByMerchant(string reference)
        {
            Uri pesapalQueryPaymentStatusUri = new Uri("_pesapalQueryPaymentStatusUri");

            // Setup builder
            IBuilder builder = new APIPostParametersBuilder()
                                .ConsumerKey(ConsumerKey)
                                .ConsumerSecret(ConsumerSecret)
                                .OAuthVersion(EOAuthVersion.VERSION1)
                                .SignatureMethod(ESignatureMethod.HMACSHA1)
                                .SimplePostHttpMethod(EHttpMethod.GET)
                                .SimplePostBaseUri(pesapalQueryPaymentStatusUri);

            // Initialize API helper
            APIHelper<IBuilder> helper = new APIHelper<IBuilder>(builder);

            try
            {
                // query pesapal for status >> format of the result is
                //pesapal_response_data=<PENDING|COMPLETED|FAILED|INVALID>
                string result = helper.PostGetQueryPaymentStatus(reference);
                string[] resultParts = result.Split(new char[] { '=' });
                string paymentStatus = resultParts[1]; /* Possible values: PENDING, COMPLETED, FAILED or INVALID*/
                return result;
            }
            catch (Exception)
            {
                // Handle error
                return "ERROR";
            }
        }
    }

}
