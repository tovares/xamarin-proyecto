using Newtonsoft.Json;
using ShopTimeline.ViewModels;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopTimeline.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersView : ContentPage
    {


        #region variables

        private UsersViewModel viewModel;

        public UsersViewModel.RootObject _client = new UsersViewModel.RootObject();

        #endregion

        public UsersView()
        {
            InitializeComponent();

            BindingContext = viewModel = new UsersViewModel();
            NavigationPage.SetHasNavigationBar(this, false);

        }


        #region Eventos

        private async void FlexButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ShopView());
        }

        #endregion

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

                GetData(tk.token);
            }
            
        }


        private void BindData( UsersViewModel.RootObject _mClient)
        {

            //entryName_First.Text = _mClient.results[0].name.first;
            //entryEmail_Email.Text = _mClient.results[0].email;
            //entryLocation_City.Text = _mClient.results[0].location.city;

        }


        private async void GetData(string _tk )
        {

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


        }




        #endregion


    }
}