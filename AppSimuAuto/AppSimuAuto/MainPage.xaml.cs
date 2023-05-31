using AppSimuAuto.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppSimuAuto
{
    public partial class MainPage : ContentPage
    {
        public SimonLogic SimonLogic { get; set; }

        public MainPage()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            SimonLogic = new SimonLogic();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ButtonGrid.Children.Add(SimonLogic.CreateSimonButton(), j, i);
                }
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    HandleLogicInfo();
                    Thread.Sleep(10);
                }
            });
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            SimonLogic.StartGame();
        }

        private void HandleLogicInfo()
        {
            Application.Current.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                if (SimonLogic.IsGameStarted)
                {
                    StartButton.IsEnabled = false;
                    CurrentScoreLabel.Text = $"Current score : {SimonLogic.CurrentScore}";
                }
                else
                {
                    StartButton.IsEnabled = true;
                    LastScoreLabel.Text = $"Last score : {SimonLogic.CurrentScore}";
                }

                if (!SimonLogic.IsGameStarted && SimonLogic.CurrentScore != 0)
                {
                    StateLabel.Text = $"Kinda bad score tbh";
                }
                else if(!SimonLogic.IsGameStarted && SimonLogic.CurrentScore == 0)
                {
                    StateLabel.Text = $"Waiting to start...";
                }
                else if(SimonLogic.IsGameStarted)
                {
                    StateLabel.Text = $"Started ! focus pls";
                }
                
            });
        }
    }
}
