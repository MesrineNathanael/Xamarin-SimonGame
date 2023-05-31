using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppSimuAuto.Helpers
{
    internal class SimonButtonHelper
    {
        public static Button GenerateSimonButton(EventHandler handle)
        {
            var button = new Button();

            button.Clicked += handle;

            //button.Background = SimonColorsHelper.GetSimonColor();
            button.Background = new SolidColorBrush(Color.FromHex("FFA3FD"));
            button.BorderWidth = 0;
            button.HorizontalOptions = LayoutOptions.Fill;
            button.VerticalOptions = LayoutOptions.Fill;

            return button;
        }
    }
}
