using AppSimuAuto.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppSimuAuto.Logic
{
    public class SimonLogic
    {
        private static Random _random = new Random();

        private static List<Button> _simonButtons = new List<Button>();

        public static List<Button> SequenceNeeded = new List<Button>();

        public static List<Button> CurrentSequence = new List<Button>();

        public static bool IsGameStarted = false;

        public static bool CanUserPress = false;

        public static int CurrentRound = 0;

        public static int CurrentScore = 0;

        public static Button CreateSimonButton()
        {
            var button = SimonButtonHelper.GenerateSimonButton((object sender, EventArgs e) => {
                if (!CanUserPress) return;
                if (sender is Button btn)
                {
                    CurrentSequence.Add(btn);
                    CurrentScore += 1 + CurrentRound;
                }
            });

            _simonButtons.Add(button);

            return button;
        }

        internal void StartGame()
        {
            if(IsGameStarted) return;

            IsGameStarted = true;
            CurrentScore = 0;
            CurrentRound = 0;

            Task.Factory.StartNew(async () => {
                while (IsGameStarted)
                {
                    var seq = GenerateNewSequence();

                    SequenceNeeded = seq;

                    PlaySequence(seq);
                    CanUserPress = true;

                    while (CheckSequences())
                    {
                        if (string.Join(", ", SequenceNeeded.Select(button => button.Id)) == string.Join(", ", CurrentSequence.Select(button => button.Id)))
                            break;
                        Thread.Sleep(10);
                    }

                    if (!CheckSequences())
                    {
                        IsGameStarted = false;
                    }

                    CanUserPress = false;
                    CurrentSequence.Clear();
                    CurrentRound++;
                    Thread.Sleep(500);
                }
            });
                
        }

        private List<Button> GenerateNewSequence()
        {
            SequenceNeeded.Clear();
            List<Button> buttons = new List<Button>();
            var numberOfButton = CurrentRound + 3;

            for(int i = 0; i < numberOfButton; i++)
            {
                buttons.Add(_simonButtons[_random.Next(0, _simonButtons.Count)]);
            }

            return buttons;
        }

        private void PlaySequence(List<Button> sequence)
        {
            foreach(var button in sequence)
            {
                var oldBg = button.Background;

                Application.Current.Dispatcher.BeginInvokeOnMainThread(() => { button.Background = button.Background = new SolidColorBrush(Color.FromHex("865DFF")); });
                Thread.Sleep(1000);
                Application.Current.Dispatcher.BeginInvokeOnMainThread(() => { button.Background = oldBg; });
                Thread.Sleep(100);
            }

        }

        private bool CheckSequences()
        {
            var result = true;

            for(int i = 0; i < CurrentSequence.Count; i++)
            {
                if (SequenceNeeded[i] != CurrentSequence[i])
                {
                    result = false;
                }

            }

            return result;
        }
    }
}
