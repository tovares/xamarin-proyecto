using Newtonsoft.Json;
using ShopTimeline.Models;
using ShopTimeline.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShopTimeline.Services
{
    public class FakeService
    {

        #region variables

        public string _nombre;
        public string _name;
        public string _email;
        public string _city;

        public UsersViewModel.RootObject _client = new UsersViewModel.RootObject();
        public MoviesViewModel.RootObject _movie = new MoviesViewModel.RootObject();
        
        private string _tk = "";

        #endregion
        
        public FakeService()
        {

            Task.Run(async () => { await GetData(); }).Wait();

        }


        public List<Card> GetAllCards()
        {
            
            return new List<Card>
            {
                new Card
                {

                    Bank = "CORIS BANK",
                    FirstSignColor = Color.FromHex("#D32F2F"),
                    LastSignColor = Color.FromHex("#FFA000"),
                    CardNumber = "5789 XXXX XXXX 6789",
                    ExpirationDate = "15/20",
                    Owner = "CARDINE DOE",
                    BackgroundGradientStartColor = Color.FromHex("#4C273C"),
                    BackgroundGradientEndColor = Color.FromHex("#A62C60"),
                    Name = _name,
                    Email = _email,
                    City =_city 


                }
            };

            

        }


        #region Procedmientos


        private void GetToken()
        {

            Token tk = new Token();

            tk.clientId = "d3airywbm0RRD3PhQlB24pzBEA55u24v";
            tk.clientSecret = "JvaCSlswlFVsINTc7MAz2--niJYZnSXnBlQjxkib2-rNKubkn4j-GY75eeeYYDBX";
            tk.contentType = "application/json";
            tk.audience = "https://dev-safy830z.auth0.com/api/v2/";
            tk.grantType = "client_credentials";
            tk.authUri = "https://dev-safy830z.auth0.com/oauth/token";

            tk.GetToken();

            if (tk.isSuccess)
            {
                _tk = tk.token;
            }

        }



        private void BindData(UsersViewModel.RootObject _mClient)
        {
                 
            _name = "Name : " + _mClient.results[0].name.first;
            _email = "Email : " + _mClient.results[0].email;
            _city = "City : " + _mClient.results[0].location.city;

        }


        //private async void GetData()
        private async Task<bool> GetData()
        {

            GetToken();

            using (var httpClientHandler = new HttpClientHandler())
            {

                // httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                string Url_API = "https://randomuser.me/api/";

                string Bearer_TK = _tk;

                var http = new HttpClient();
                http.DefaultRequestHeaders.Add("authorization", ("Bearer " + Bearer_TK));

                var json = await http.GetStringAsync(Url_API);

                _client = JsonConvert.DeserializeObject<UsersViewModel.RootObject>(json);

                BindData(_client);

            }


            //var lstact = new List<MoviesViewModel.ActorPrincipal> { 
            //    new MoviesViewModel.ActorPrincipal {Id = "01", Edad = "50", Nombre = "Nombre 1er Actor"},
            //    new MoviesViewModel.ActorPrincipal {Id = "02", Edad = "25", Nombre = "Nombre 2do Actor"}
            //};
            
            //var car = new List<MoviesViewModel.Caracteristica> { 
            //    new MoviesViewModel.Caracteristica { IdCaracteristica = "01", ActorPrincipal = lstact, Ano = "2000", Nombre = "Caracteristica 1" },
            //    new MoviesViewModel.Caracteristica { IdCaracteristica = "02", ActorPrincipal = lstact, Ano = "5000", Nombre = "Caracteristica 2" }
            //};
            

            //var _result = new List<MoviesViewModel.Result> {
            //    new MoviesViewModel.Result { Id ="00", Tipo="DVC", Genero="Drama" , Caracteristica = car },
            //    new MoviesViewModel.Result { Id ="01", Tipo="DVC", Genero="Drama" , Caracteristica = car },
            //    new MoviesViewModel.Result { Id ="02", Tipo="DVC", Genero="Drama" , Caracteristica = car },
            //};

            //_movie.results = _result;

            //var _movieJson = JsonConvert.SerializeObject(_movie);


            return true;

        }




        #endregion



    }
}
