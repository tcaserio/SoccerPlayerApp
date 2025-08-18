using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoccerProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetTime : Popup
    {
        public SetTime()
        {
            InitializeComponent();
            //display the current time in minutes
            entHalfDuration.Placeholder = $"enter half duration in minutes";
        }

        private void entHalfDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entHalfDuration.Text != "")
            {
                btnClose.IsEnabled = true;
                
            }
            else
            {
                btnClose.IsEnabled = false;
            }
        }

        private async void btnClose_Pressed(object sender, EventArgs e)
        {
            int seconds = 0;
            int minutes = 0;
            //if can parse to int
            if (int.TryParse(entHalfDuration.Text, out minutes))
            {
                if (minutes < 100 && minutes > 0)
                {
                    //convert seconds to mins
                    seconds = minutes * 60;
                    GlobalVariables.HalfTime = seconds;
                    GlobalVariables.TimeLeft = GlobalVariables.HalfTime;
                    //Navigation.PushModalAsync();
                    Dismiss(true);

                }
                else
                {
                    lblError.Text = "Please enter a number between 1 and 99";
                }

            }
        }
    }
}