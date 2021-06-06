using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tietactoe
{
    public partial class MainPage : ContentPage
    {
        int MovesPlayer1 = -2;
        int MovesPlayer2 = -2;
        int ScorePlayer = 4;
        String Player1Name = "";
        String Player2Name = "";

        HttpClient client = new HttpClient();
        string url = "http://inekee.broeders.be/1920xamarin/webservice.aspx?name=";
        bool player1 = true;
        bool gewonnen = false;
        int[,] array = new int[,]
        {
            {0,0,0 },
            {0,0,0 },
            {0,0,0 },
        };
        public MainPage( string player1name, string player2name)
        {
            InitializeComponent();
            this.Player1Name = player1name;
            this.Player2Name = player2name;
        }
        public void Button_Clicked( object sender,EventArgs e)
        {
            Button b = ((Button)sender);
            string name = b.StyleId;
            string x = name.Substring(1, 1);
            string y = name.Substring(2, 1);
            b.IsEnabled = false;
            if (player1 == true)
            {
                array[int.Parse(x), int.Parse(y)] = 1;
                check();
                MovesPlayer1 += 1;
                player1 = false;
            }
            else
            {
                array[int.Parse(x), int.Parse(y)] = 1;
                b.BackgroundColor = Color.Green;
                check();
                MovesPlayer2 += 1;
                player1 = true;
            }
        }

        public void check()
        {
            int getal = 0;

            if(player1== true)
            {
                getal = 1;
            }
            else
            {
                getal = 2;
            }
       if(b00.IsEnabled==false && b01.IsEnabled==false && b02.IsEnabled==false && b10.IsEnabled==false && b11.IsEnabled==false && b12.IsEnabled==false && b20.IsEnabled==false && b21.IsEnabled==false && b22.IsEnabled == false)
            {
                DisplayAlert("Tie", "Niemad heeft gewoonnen", "ok");
                gewonnen = true;
            }
            if (array[0, 0] == getal && array[0, 1] == getal && array[2, 0] == getal)
            {
                DisplayAlert("Gewonnen", "Player" + getal.ToString() + "heeft gewonnen", "ok");
                gewonnen = true;
            }
            if (array[2,0] == getal && array[2, 1] == getal && array[2, 2] == getal)
            {
                DisplayAlert("Gewonnen", "Player" + getal.ToString() + "heeft gewonnen", "ok");
                gewonnen = true;
            }
            if (array[0, 0] == getal && array[1, 1] == getal && array[2, 2] == getal)
            {
                DisplayAlert("Gewonnen", "Player" + getal.ToString() + "heeft gewonnen", "ok");
                gewonnen = true;
            }
            if (array[2, 0] == getal && array[1, 0] == getal && array[0, 2] == getal)
            {
                DisplayAlert("Gewonnen", "Player" + getal.ToString() + "heeft gewonnen", "ok");
                gewonnen = true;
            }
            if (gewonnen == true)
            {
                if (getal == 1)
                {
                    if (MovesPlayer1 >= 0)
                    {
                        ScorePlayer -= MovesPlayer1;
                        url += Player1Name + "&score=" + ScorePlayer.ToString();
                    }
                }
                if (getal == 2)
                {
                    if (MovesPlayer2 >= 0)
                    {
                        ScorePlayer -= MovesPlayer2;
                        url += Player2Name + "&score=" + ScorePlayer.ToString();
                    }
                }
                HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(url);
                request.Method = "GET";
                var webResponse = request.GetResponse();
                Navigation.PushAsync(new StartupPage());

            }
        }
    }
}
