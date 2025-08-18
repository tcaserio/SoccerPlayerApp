using SoccerProject.ViewModels;
using SoccerProject.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.CommunityToolkit.Extensions;
using System.Collections.Generic;

namespace SoccerProject
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Clear Player List?", "Are you sure you want to clear the player list?", "Yes", "No");
            if (confirm)
                PlayerManager.Players.Clear();

        }

        private async void miSetTime_Clicked(object sender, EventArgs e)
        {
            //show popup
            var popup = new SetTime();
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            popup.Dismissed += (s, args) => tcs.SetResult(null);
            await Navigation.ShowPopupAsync(popup);

            //wait for user to close the popup
            await tcs.Task;
        }
    }
}
