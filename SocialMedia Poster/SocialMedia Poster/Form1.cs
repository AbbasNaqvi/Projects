using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Facebook;

namespace SocialMediaPoster
{
    using System;
    using System.Windows.Forms;
    using Facebook;
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;
    using System.IO;
    using System.Dynamic;
    using System.Net.Mail;

    public partial class Form1 : Form
    {
        #region  MailAttrributes
        private string EmailUserName = "abbas.naqvi@dynamologic.com";
        private string EmailPassword = "Disneyjob";
        List<string> UploadingImages = new List<string>();

        private string FacebookTO = "retry359tinder@m.facebook.com";
        private string TwitterTO = "Tweet@twittermail.com";
        #endregion
    
        #region LoginData
        //     private string userName = "AbbasNaqvi123";
        private string pageid = null;
        bool IsPage = true;
        private string userName = "SocialMediaPoster12";
        private const string AppId = "549177888517555";
        private string FBAppSecret = "4445227995b6afe09f1688b2e9b112be";
        string FBRedirectUrl = "http://abbasnaqvi512.tumblr.com";
        private const string ExtendedPermissions = "user_about_me,publish_stream,offline_access,publish_actions";
        private readonly Uri LoginUrl;
        private string accesstoken = null;
        public FacebookOAuthResult FacebookOAuthResult { get; private set; }
        FacebookClient Fbclient;
        #endregion

        #region PostData
        string Caption = "Caption is amazing1";
        string Description = "Description is amazing1";
        string photourl = "https://developers.soundcloud.com/assets/posts/no-spam-6729407a32d458c3464348f88ab4da50.png";

        #endregion



        public Form1()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(AppId)==false)
            {
                Fbclient = new FacebookClient();
                IDictionary<string, object> _loginParameters = new Dictionary<string, object>();
                _loginParameters["response_type"] = "token";
                _loginParameters["display"] = "popup";
                _loginParameters[" redirect_uri"] = FBRedirectUrl;
                if (!string.IsNullOrEmpty(ExtendedPermissions))
                {
                    _loginParameters["scope"] = ExtendedPermissions;
                }
                Fbclient.AppId = AppId;
                Fbclient.AppSecret = FBAppSecret;
                LoginUrl = Fbclient.GetLoginUrl(_loginParameters);
                
            }
        }
        #region Working Code

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
            webBrowser1.Navigate(LoginUrl.AbsoluteUri);
        }
        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            FacebookOAuthResult result;

            if (Fbclient.TryParseOAuthCallbackUrl(e.Url, out result))
            {
                // The url is the result of OAuth 2.0 authentication
                if (result.IsSuccess)
                {
                    accesstoken = result.AccessToken;
                    dynamic name = Fbclient.Get(userName);
                    richTextBox1.Text += "\n\n" + (string)name["username"] + "\n\n";
                    pageid = (string)name["username"];
                    FacebookMessanger messenger = new FacebookMessanger();
                    try
                    {
                        messenger.ShareOnFB(Caption, Description, photourl, accesstoken, userName, IsPage);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    var errorDescription = result.ErrorDescription;
                    var errorReason = result.ErrorReason;
                }
            }
            else
            {
                // The url is NOT the result of OAuth 2.0 authentication.
            }
        }

        #endregion
        /*
         * This method Sends Mail
         * 
         */
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                FacebookMessanger messanger = new FacebookMessanger();
                messanger.SendEmail(EmailUserName, EmailPassword, FacebookTO, EmailUserName, Caption, Description, null);
            }
            catch (Exception ex)
            {
                richTextBox1.Text += "\n\n"+ex.Message+"\n\n";
            }
        }

        private void btnEmailTweet_Click(object sender, EventArgs e)
        {
            try
            {
                FacebookMessanger messanger = new FacebookMessanger();
                if (Description.Length < 130)
                {
                    messanger.SendEmail(EmailUserName, EmailPassword, TwitterTO, EmailUserName, Caption, Description, null);
                }
                else { 
                    messanger.SendEmail(EmailUserName, EmailPassword, "post@tweetymail.com", EmailUserName, Caption, Description, null);
                
                }
            }
            catch (Exception ex)
            {
                richTextBox1.Text += "\n\n" + ex.Message + "\n\n";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TwitterClient client = new TwitterClient("Abbas5564","Disney7");
            //client.SendMessage("aaaaaaaaaaaassssssssss");
            try
            {
                client.Checkit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                client.SendReply();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        #region healthycode

        //private string GetAccessToken()
        //{
        //    var fb = new FacebookClient();
        //    dynamic result = fb.Get("oauth/access_token", new
        //    {
        //        client_id = FBAppId,
        //        client_secret = FBAppSecret,
        //        redirect_uri = FBRedirectUrl,
        //        code = code
        //    });

        //    return result;
        //}
        //private void DisplayAppropriateMessage(FacebookOAuthResult facebookOAuthResult)
        //{
        //    if (facebookOAuthResult != null)
        //    {
        //        if (facebookOAuthResult.IsSuccess)
        //        {
        //            _accessToken = facebookOAuthResult.AccessToken;
        //            var fb = new FacebookClient(facebookOAuthResult.AccessToken);

        //            dynamic result = fb.Get("/me");
        //            var name = result.name;

        //            // for .net 3.5
        //            //var result = (IDictionary<string, object>)fb.Get("/me");
        //            //var name = (string)result["name"];

        //            MessageBox.Show("Hi " + name);
        //            btnLogout.Visible = true;
        //        }
        //        else
        //        {
        //            MessageBox.Show(facebookOAuthResult.ErrorDescription);
        //        }
        //    }
        //}
        //        private string GetString(string SubjectString, string pattern)
        //        {
        //            string Result = null;
        //            try
        //            {
        //                Match m = Regex.Matches(SubjectString, pattern)[0];
        //                Result = m.Groups["data"].Value;
        //            }
        //            catch (Exception e)
        //            {
        //                Result = null;
        //            }
        //            return Result;
        //        }
        //        private void button2_Click(object sender, EventArgs e)
        //        {
        //            string lsd = null;
        //            string lgnrnd = null;
        //            Int64 lgnjs = 0;

        //            string FirstUrl = "https://www.facebook.com/v2.1/dialog/oauth?redirect_uri=" + FBRedirectUrl + "&display=popup&response_type=code&client_id=" + FBAppId + "&ret=login";
        //            string SecondUrl = null;
        //            #region First Request
        //            using (CookieAwareWebClient FirstClient = new CookieAwareWebClient())
        //            {
        //                FirstClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/6.0; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729; InfoPath.2; LCJB)");
        //                string FirstDocument = FirstClient.DownloadString(FirstUrl);
        //                SecondUrl = GetString(FirstDocument, "url=(?<data>.*?)\"").Replace("amp;", "");
        //            }
        //            #endregion
        //            #region Second Request

        //            using (CookieAwareWebClient SecondClient = new CookieAwareWebClient())
        //            {

        //                SecondClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/6.0; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729; InfoPath.2; LCJB)");
        //                SecondClient.Headers.Add("Accept-Language", "en-US");
        //                SecondClient.Headers.Add("Cache-Control", "no-cache");
        //                SecondClient.Headers.Add("Accept", "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*");
        //                SecondClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

        //                string response = SecondClient.DownloadString(SecondUrl);
        //                lsd = GetString(response, "name=\"lsd\"(.*?)value=\"(?<data>.*?)\"");
        //                lgnrnd = GetString(response, "name=\"lgnrnd\"(.*?)value=\"(?<data>.*?)\"");
        //                string temp = GetString(response, "serverTime\":(?<data>.*?)}");
        //                string replaced = Regex.Replace(
        //                         temp,
        //                         @"(?<=\d+?)0+(?=\D|$)",
        //                         String.Empty);
        //                //temp = Regex.Matches(temp, "(?<data>.*?)(0*)", RegexOptions.RightToLeft)[0].Groups["data"].Value;
        //                lgnjs = Convert.ToInt64(replaced);
        //                lgnjs += 5;

        //            }
        //            #endregion
        //            HttpWebRequest requestLogin = (HttpWebRequest)HttpWebRequest.Create(@"https://www.facebook.com/login.php?login_attempt=1&next=https%3A%2F%2Fwww.facebook.com%2Fv2.1%2Fdialog%2Foauth%3Fredirect_uri%3Dhttp%253A%252F%252Fabbasnaqvi512.tumblr.com%252F%26display%3Dpopup%26response_type%3Dcode%26client_id%3D549177888517555%26ret%3Dlogin&popup=1
        //");
        //            requestLogin.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/6.0; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729; InfoPath.2; LCJB)";
        //            requestLogin.Headers[HttpRequestHeader.AcceptLanguage] = "en-US";
        //            requestLogin.ContentType = "application/x-www-form-urlencoded";
        //            requestLogin.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
        //            requestLogin.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";
        //            requestLogin.Method = "POST";

        //            string qsstamp = "W1tbMywxNSwyNCwzMCwzNyw0OCw2NSw3OCw5NiwxMTIsMTEzLDE1MiwxNTQsMTU2LDE3MywyMjksMjM0LDI3MywyODEsMjkwLDMyMywzNDEsMzY5LDM3OCwzODMsMzkxLDM5NCw0NTksNDY0LDQ3NSw0ODgsNTM0LDUzNSw1NDAsNTU2LDU3Nyw2MDQsNjA5LDYxNSw2MTYsNjI1LDc5OF1dLCJBWmxINHlKVjRiRlFta2JfeEVhUVliallZdkRHdDNIN3Y0RVN0aU9rOXQ5WUkxVlBRMUNYaUZ0YUpyV3NXV01MX2JCSXRiUDBZTUY1WDBzbFRVcDQwSUpXUF9ZRjJVMDh3VktQSEJZYWN4LXdEcUFXSy1NTE14d0QwNVJOcFE3UmxFWVR6VHI3Nk8xei1WTlE1cnZNNW1xUjNkWG9ldEFGZzBDN3lQazF6MlJYZzJHUkZlWUpLU1pSMDVyMDBfc05PNHVQUTBxY2ZTcnoxT1BuWFY5c3lzLXEtOEp1MC1VUzZxaDZqRzAtY3BfY1d3Il0=";


        //            string email = "iamabbas512@hotmail.com";
        //            string pass = "Disney81";

        //            StringBuilder postData = new StringBuilder();
        //            postData.Append("lsd=" + WebUtility.HtmlEncode(lsd) + "&");
        //            postData.Append("api_key=" + WebUtility.HtmlEncode(FBAppId) + "&");
        //            postData.Append("display=" + WebUtility.HtmlEncode("popup") + "&");
        //            postData.Append("enable_profile_selector=" + WebUtility.HtmlEncode("") + "&");
        //            postData.Append("legacy_return=" + WebUtility.HtmlEncode("1") + "&");
        //            postData.Append("profile_selector_ids=" + WebUtility.HtmlEncode("") + "&");
        //            postData.Append("skip_api_login=" + WebUtility.HtmlEncode("1") + "&");
        //            postData.Append("signed_next=" + WebUtility.HtmlEncode("1") + "&");
        //            postData.Append("trynum=" + WebUtility.HtmlEncode("1") + "&");
        //            postData.Append("timezone=" + WebUtility.HtmlEncode("-300") + "&");
        //            postData.Append("lgnrnd=" + WebUtility.HtmlEncode(lgnrnd) + "&");
        //            postData.Append("lgnjs=" + WebUtility.HtmlEncode(lgnjs.ToString()) + "&");
        //            postData.Append("email=" + WebUtility.HtmlEncode(email) + "&");
        //            postData.Append("pass=" + WebUtility.HtmlEncode(pass) + "&");
        //            postData.Append("qsstamp=" + WebUtility.HtmlEncode(qsstamp));
        //            ASCIIEncoding ascii = new ASCIIEncoding();
        //            byte[] postBytes = ascii.GetBytes(postData.ToString());

        //            Stream postStream = requestLogin.GetRequestStream();
        //            postStream.Write(postBytes, 0, postBytes.Length);
        //            postStream.Flush();
        //            postStream.Close();
        //            HttpWebResponse responseLogin = (HttpWebResponse)requestLogin.GetResponse();

        //            //    webBrowser1.Navigate("http://facebook.com/index.php");

        //            string redirUrl = responseLogin.Headers["Location"];
        //            richTextBox1.Text = redirUrl;


        //        }

        #endregion

        #region unused Buttons

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    webBrowser1.Navigate(" https://www.facebook.com/v2.1/dialog/oauth?redirect_uri=http%3A%2F%2Fabbasnaqvi512.tumblr.com%2F&display=popup&response_type=code&client_id=549177888517555&ret=login");
        //}
        
        
        
        
        
        //private void btnLogout_Click(object sender, EventArgs e)
        //{
        //    var webBrowser = new WebBrowser();
        //    var fb = new FacebookClient();
        //    var logouUrl = fb.GetLogoutUrl(new { access_token = _accessToken, next = "https://www.facebook.com/connect/login_success.html" });
        //    webBrowser.Navigate(logouUrl);
        //    btnLogout.Visible = false;
        //}

        //private void btnFacebookLogin_Click_1(object sender, EventArgs e)
        //{
        //    var fbLoginDialog = new FacebookLoginDialog(FBAppId, ExtendedPermissions);
        //    fbLoginDialog.ShowDialog();

        //    DisplayAppropriateMessage(fbLoginDialog.FacebookOAuthResult);
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string dialog_url = " https://www.facebook.com/dialog/oauth?client_id=549177888517555&display=popup&response_type=code&redirect_uri=http://abbasnaqvi512.tumblr.com/";
        //    WebClient c = new WebClient();
        //    string document = c.DownloadString(dialog_url);
        //    webBrowser1.Navigate(dialog_url);
        //    richTextBox1.Text = document;

        //}












        #endregion

        #region Depreciated
        //Takes Out the Login From Code Manually..Replaced By New Method
        //private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Url.OriginalString.Contains("assets") && e.Url.OriginalString.Contains("code"))
        //        {
        //            code = Regex.Matches(e.Url.OriginalString, "assets.tumblr.com.*?code%3D(?<data>.*?)&")[0].Groups["data"].Value;
        //            richTextBox1.Text += "\n\n" + code + "\n\n";
        //            if (String.IsNullOrEmpty(code) == false)
        //            {
        //                webBrowser1.Stop();
        //                webBrowser1.Dispose();
        //                _accessToken = GetAccessToken();
        //                ShareOnFB();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        #endregion
    }
}