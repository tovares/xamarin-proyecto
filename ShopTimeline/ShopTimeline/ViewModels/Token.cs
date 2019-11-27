using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ShopTimeline.ViewModels
{
    public class Token
    {

        #region Propiedades

        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string authUri { get; set; }
        public string contentType { get; set; }
        public string audience { get; set; }
        public string grantType { get; set; }
        public string token { get; set; }
        public string tokenType { get; set; }
        public bool isSuccess { get; set; }


        #endregion

        #region Procedimientos


        public async void GetToken()
        {

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };


                var httpClient = new HttpClient();

                JObject oJsonObject = new JObject();
                oJsonObject.Add("client_id", clientId);
                oJsonObject.Add("client_secret", clientSecret);
                oJsonObject.Add("audience", audience);
                oJsonObject.Add("grant_type", grantType);

                HttpResponseMessage response = httpClient.PostAsync(authUri, new StringContent(oJsonObject.ToString(), Encoding.UTF8, contentType)).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    string res = await response.Content.ReadAsStringAsync();
                    var jresult = JsonConvert.DeserializeObject<AccessToken>(res.ToString());

                    token = jresult.access_token.ToString();
                    tokenType = jresult.token_type.ToString();
                    isSuccess = true;
                }
                else
                {
                    token = "";
                    tokenType = "";
                    isSuccess = false;
                }


            }


        }


        #endregion

    }
}
