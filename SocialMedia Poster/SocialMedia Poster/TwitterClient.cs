using System;
using System.Net;
using System.IO;
using System.Web;
using Twitterizer;
using Twitterizer.Core;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
namespace SocialMediaPoster
{
    public class TwitterClient
    {


        public string ConsumerKey = "rQ42mTpJ0YsoqkgFoCErV29n5";
        public string ConsumerSecret = "xWhVvt4DGNCV80W5my5JRNkkLc7dUxwAFYrjOdYiuW3L6Ohaz6";
        public string AccessToken = "2600414179-hTvSTtF7gK3LnvlEd61GS3cjOueas9gGlLP8Ly7";
        public string AccessTokenSecret = "6OukddCMytWOEP8fdL6zixU5yIYm8f4Kax1bRS2bT6mWM";



        public string Username { get; set; }
        public string Password { get; set; }
        public Exception Error { get; set; }



        public TwitterClient(string userName, string password)
        {
            this.Username = userName;
            this.Password = password;
        }
        public void Checkit()
        {
            OAuthTokens TOKEN = new OAuthTokens();
            TOKEN.AccessToken = AccessToken;
            TOKEN.AccessTokenSecret = AccessTokenSecret;
            TOKEN.ConsumerKey = ConsumerKey;
            TOKEN.ConsumerSecret = ConsumerSecret;

            TwitterResponse<TwitterStatus> response = TwitterStatus.Update(
                        TOKEN,
                        "Testing!! It works (hopefully).");

            if (response.Result == RequestResult.Success)
            {
                throw new Exception(response.Result.ToString());
            }
            else
            {
                throw new Exception(response.Result.ToString());
            }
        }
        public void SendReply()
        {
            string status = "Abbas Naqviiia  asdasd asdas a  aa as d da a sds";
            string postBody = "status=" + Uri.EscapeDataString(status);
            string oauth_consumer_key = ConsumerKey;
            string oauth_consumerSecret = ConsumerSecret;
            string oauth_signature_method = "HMAC-SHA1";
            string oauth_version = "1.0";
            string oauth_token = AccessToken;
            string oauth_token_secret = AccessTokenSecret;
            string oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds + 305).ToString();

            SortedDictionary<string, string> basestringParameters = new SortedDictionary<string, string>();
            //include the "in_reply_to_status_id" parameter if you need to reply to particular tweet.
            //basestringParameters.Add("in_reply_to_status_id",
            //  "status id of the post to which we are going to reply");
            basestringParameters.Add("status", Uri.EscapeDataString(status));
            basestringParameters.Add("oauth_version", oauth_version);
            basestringParameters.Add("oauth_consumer_key", oauth_consumer_key);
            basestringParameters.Add("oauth_nonce", oauth_nonce);
            basestringParameters.Add("oauth_signature_method", oauth_signature_method);
            basestringParameters.Add("oauth_timestamp", oauth_timestamp);
            basestringParameters.Add("oauth_token", oauth_token);

            //Build the signature string
            string baseString = String.Empty;
            baseString += "POST" + "&";
            baseString += Uri.EscapeDataString("https://api.twitter.com/1.1/statuses/update.json&include_entities=true") + "&";
            foreach (KeyValuePair<string, string> entry in basestringParameters)
            {
                baseString += Uri.EscapeDataString(entry.Key + "=" + entry.Value + "&");
            }

            //GS - Remove the trailing ambersand char, remember 
            //it's been urlEncoded so you have to remove the 
            //last 3 chars - %26
            baseString = baseString.Substring(0, baseString.Length - 3);

            //Build the signing key    
            string signingKey = Uri.EscapeDataString(oauth_consumerSecret) +
              "&" + Uri.EscapeDataString(oauth_token_secret);

            //Sign the request
            HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(signingKey));
            string signatureString = Convert.ToBase64String(hasher.ComputeHash(new ASCIIEncoding().GetBytes(baseString)));

            //Tell Twitter we don't do the 100 continue thing
            ServicePointManager.Expect100Continue = false;

       //     HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(
          //   @"https://api.twitter.com/1.1/statuses/update.json?in_reply_to_status_id=status id");
                          HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(
                 @"https://api.twitter.com/1.1/statuses/update.json?include_entities=true&status=" + Uri.EscapeDataString(status));

            string authorizationHeaderParams = String.Empty;
            authorizationHeaderParams += "OAuth ";
            authorizationHeaderParams += "oauth_consumer_key=" + "\"" +
            Uri.EscapeDataString(oauth_consumer_key) + "\", ";
            authorizationHeaderParams += "oauth_nonce=" + "\"" +
              Uri.EscapeDataString(oauth_nonce) + "\",";
            authorizationHeaderParams += "oauth_signature_method=" +
              "\"" + Uri.EscapeDataString(oauth_signature_method) + "\", ";
            authorizationHeaderParams += "oauth_timestamp=" + "\"" +
              Uri.EscapeDataString(oauth_timestamp) + "\", ";
          
            authorizationHeaderParams += "oauth_token=" + "\"" +
              Uri.EscapeDataString(oauth_token) + "\", ";
            authorizationHeaderParams += "oauth_signature=" + "\"" +
              Uri.EscapeDataString(signatureString) + "\", ";
            authorizationHeaderParams += "oauth_version=" + "\"" +
              Uri.EscapeDataString(oauth_version) + "\"";


            webRequest.Accept = "*/*";
            webRequest.Headers.Add("Authorization", authorizationHeaderParams);
            webRequest.KeepAlive = false;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            
            
            Stream stream = webRequest.GetRequestStream();
            byte[] bodyBytes = new ASCIIEncoding().GetBytes(postBody);
            stream.Write(bodyBytes, 0, bodyBytes.Length);
            stream.Flush();
            stream.Close();

            //Allow us a reasonable timeout in case Twitter's busy
            webRequest.Timeout = 3 * 60 * 1000;
            try
            {
                //         webRequest.Proxy = new WebProxy("enter proxy details/address");
                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                Stream dataStream = webResponse.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}