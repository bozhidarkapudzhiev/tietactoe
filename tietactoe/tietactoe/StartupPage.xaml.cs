using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tietactoe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartupPage : ContentPage
    {
        ObservableCollection<Highscore> highscores = new ObservableCollection<Highscore>();
        ObservableCollection<Highscore> Highscores { get { return highscores; } }
        const string url = "http://inekee.broeders.be/1920xamarin/webservice.aspx";
        public StartupPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(url);
            request.Method = "GET";
            var webResponse = request.GetResponse();
            string responseString;
            using (var stream= webResponse.GetResponseStream())
            {
                using(var reader= new StreamReader(stream))
                {
                    responseString = reader.ReadToEnd();
                }
            }
            dynamic results = JsonConvert.DeserializeObject<dynamic>(responseString);
          
        }
        private async void Button_Clicked( object sender, EventArgs e)
        {
            string player1Name = this.FindByName<Entry>("nameplayer1").Text;
            string player2Name = this.FindByName<Entry>("nameplayer2").Text;

            var gamePage = new MainPage(player1Name,player2Name);
            NavigationPage nav = new NavigationPage(gamePage);
            await Navigation.PushAsync(new MainPage(player2Name,player2Name));
        }

    }
}