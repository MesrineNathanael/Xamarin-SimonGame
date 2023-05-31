using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppSimuAuto.Helpers
{
    internal class SimonColorsHelper
    {
        private static Random _random = new Random();

        private static List<Color> _baseSimonColors = new List<Color> ()
        {
            Color.YellowGreen,
            Color.Green,
            Color.Blue,
            Color.Orange,
            Color.Orchid,
            Color.Purple,
            Color.Pink,
            Color.Red,
            Color.Lavender
        };

        public static Color GetSimonColor()
        {
            if(_baseSimonColors.Count == 0)
                return Color.Magenta;

            var randomIndex = _random.Next(0, _baseSimonColors.Count);
            var color = _baseSimonColors[randomIndex];
            
            _baseSimonColors.RemoveAt(randomIndex);

            return color;
        }

        public static Color GetSimonPressedColor(Brush baseColor)
        {
            return ((SolidColorBrush)baseColor).Color.AddLuminosity(10);
        }
    }
}
