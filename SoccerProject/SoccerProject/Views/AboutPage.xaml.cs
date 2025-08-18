using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.CommunityToolkit.Extensions;
using System.Collections.Generic;
using System.Windows.Input;
using System.IO;

namespace SoccerProject.Views
{
    public partial class AboutPage : ContentPage
    {
        private const int SECONDS = 60;
        //maximum mins for the timer
        private const int DOUBLE_DIGITS = 10;

        //keeps track of game time left (45 mins = 2700s)
        private static int timeLeft;
        //keep track of player timer requirement  (half of the entire game time)
        private int playerTimeRequirement = timeLeft / 2;
        // Create a list of ListItem objects
        List<Player> listItems = new List<Player>();

        ListView listView;

        public AboutPage()
        {
            InitializeComponent();


            //--------------------------MANAGE GLOBAL AND CLASS VARIABLES--------------------------------
            //set the global half time to a default value. (2100 = 35 mins U-14)
            GlobalVariables.HalfTime = 2100;
            //set the time left be one half time
            timeLeft = GlobalVariables.HalfTime;
            //keeps track of counting down status
            GlobalVariables.isTimerRunning = false;
            //keeps track of game state (half 1 or 2)
            GlobalVariables.isSecondHalf = false;

            //store the values to be displayed
            int minutes = 0, seconds = 0;

            //minutes remaining is timeLeft div 60
            minutes = GlobalVariables.TimeLeft / SECONDS;
            //seconds is timeLeft mod 60 (remainder)
            seconds = GlobalVariables.TimeLeft % SECONDS;

            //--------------------------INITILIZE UI ELEMENTS--------------------------------
            //display the Game timer initially
            DisplayTime(timeLeft);

            listView = new ListView
            {
                // Set the ItemTemplate property to use the custom ViewCell
                ItemTemplate = new DataTemplate(typeof(ListItemViewCell))
            };

            // Add items to the ListView's ItemsSource property to display them in the UI
            listView.ItemsSource = PlayerManager.Players;
            //listView.BackgroundColor = Color.SkyBlue;
            
            //add the list view to the grid
            gridPlayerInfo.Children.Add(listView, 0, 1);
            //set the column span to 3
            Grid.SetColumnSpan(listView, 3);

            //--------------------------LOAD DATA--------------------------------
            //If any players exist, load them from the file
            LoadFileData();
            //and update the list view
            listView.ItemsSource = null;
            listView.ItemsSource = PlayerManager.Players;
            //make sure remove button is active if there are players loaded
            if (PlayerManager.Players.Count > 0)
            {
                lblRemovePlayer.IsEnabled = true;
            }

        }

        private async void btnTimer_Pressed(object sender, EventArgs e)
        {
            //if timer is not running
            if (GlobalVariables.isTimerRunning == false)
            {
                //set timer running to true
                GlobalVariables.isTimerRunning = true;
                //visually change the color to red for stoping the timer
                btnTimer.BackgroundColor = Color.FromHex("#e00d23");
                //change the timer color back to white
                lblGameTime.TextColor = Color.White;
                //change button text to stop game timer
                btnTimer.Text = "Stop Game Timer";
                //start timer task
                var task = CountDown();
                await task;

            }
            //if timer is running
            else
            {
                //change button text to start game timer
                btnTimer.Text = "Start Gamer Timer";
                //change the color back to blue
                btnTimer.BackgroundColor = Color.FromHex("#1d76db");
                //change label color to Red
                lblGameTime.TextColor = Color.FromHex("#e00d23");
                //set timer running to false
                GlobalVariables.isTimerRunning = false;
                
            }
        }

        private async Task CountDown()
        {
            while (timeLeft >= 0)
            {
                if (GlobalVariables.isTimerRunning == false)
                {
                    return;
                }
                //display time left as mm:ss
                DisplayTime(timeLeft);

                //start timer
                var task = StartTimer();
                //wait until the task is complete (1 second)
                await task;
                //decrement the time left
                timeLeft--;
                //update all the players on the field
                foreach (Player player in PlayerManager.Players)
                {
                    //make sure players aren't updated after time left has just decremented to 0
                    if (player.OnField == true && timeLeft != 0)
                    {
                        //increment the player's play time
                        player.TimeOnField++;
                        //update the player's percentage progress by giving it the entire length of the game
                        player.UpdateProgress(GlobalVariables.HalfTime * 2);
                    }
                }

            }

            //finished the half...
            //if 1st half finished
            if (GlobalVariables.isSecondHalf == false)
            {
                //move on to 2nd half
                GlobalVariables.isSecondHalf = true;
                //reset the timer
                timeLeft = GlobalVariables.HalfTime;
                //reset the displayed time
                DisplayTime(timeLeft);
                //update the button to start the second half
                btnTimer.Text = "Start 2nd Half";
                //update timer running state
                GlobalVariables.isTimerRunning = false;
                //change the button color
                btnTimer.BackgroundColor = Color.FromHex("#1d76db");
                //change the label displaying the half number
                lblGameHalf.Text = "Half 2";
            }
            else 
            {
                //game finished... do something?
                //disable button
                btnTimer.IsEnabled = false;
                //change the timer label color to red
                lblGameTime.TextColor = Color.FromHex("#e00d23");
                //update the button to say the game is over
                btnTimer.Text = "Game is Finished";
                return;
            }
        }

        /// <summary>
        /// Displays time in mm:ss format
        /// </summary>
        /// <param name="timeLeft"></param>
        private void DisplayTime(int timeLeft) 
        {
            //store the values to be displayed
            int minutes = 0, seconds = 0;

            //minutes remaining is timeLeft div 60
            minutes = timeLeft / SECONDS;
            //seconds is timeLeft mod 60 (remainder)
            seconds = timeLeft % SECONDS;

            if (minutes >= DOUBLE_DIGITS)
            {
                //mins >= 10 and seconds greater/equal than
                if (seconds >= DOUBLE_DIGITS)
                {
                    //report time normally
                    lblGameTime.Text = $"Game Time: {minutes}:{seconds}";
                }
                else 
                {
                    //report time with a 0 infront of seconds
                    lblGameTime.Text = $"Game Time: {minutes}:0{seconds}";
                }
            }

            //mins less than 10
            else
            {
                //mins <= 10 and seconds greater/equal than 10
                if (seconds >= DOUBLE_DIGITS)
                {
                    //report time with 0 infront of mins
                    lblGameTime.Text = $"Game Time: 0{minutes}:{seconds}";
                }
                //both mins and seconds less than 10
                else
                {
                    //report time with 0 infront of both mins and seconds
                    lblGameTime.Text = $"Game Time: 0{minutes}:0{seconds}";
                }
            }
        }

        /// <summary>
        /// start timer and delay to count seconds
        /// </summary>
        /// <returns></returns>
        private async Task StartTimer()
        {
            //wait 1000 miliseconds (1 second)
            await Task.Delay(1000);
        }

        private async void lblRemovePlayer_Pressed(object sender, EventArgs e)
        {
            //show popup
            var popup = new RemovePlayerPopup();
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            popup.Dismissed += (s, args) => tcs.SetResult(null);
            await Navigation.ShowPopupAsync(popup);

            //wait for user to close the popup
            await tcs.Task;

            //idk why this is necessary, but to update the list view the source needs to be set to null
            //then it reset to it's proper source.
            listView.ItemsSource = null;
            listView.ItemsSource = PlayerManager.Players;

            //if the list is empty, disable the remove button
            if (PlayerManager.Players.Count <= 0)
            { 
                lblRemovePlayer.IsEnabled = false;
            }
        }

        private async void lblAddPlayer_Pressed(object sender, EventArgs e)
        {
            //show popup
            var popup = new AddPlayerPopup();
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            popup.Dismissed += (s, args) => tcs.SetResult(null);
            await Navigation.ShowPopupAsync(popup);

            //wait for user to close the popup
            await tcs.Task;

            //idk why this is necessary, but to update the list view the source needs to be set to null
            //then it reset to it's proper source.
            listView.ItemsSource = null;
            listView.ItemsSource = PlayerManager.Players;

            //enable remove button if the list has anything in it
            if (PlayerManager.Players.Count > 0)
            {
                lblRemovePlayer.IsEnabled = true;
            }
        }

        private async void lblClearPlayers_Pressed(object sender, EventArgs e)
        {
            //warn user
            bool result = await DisplayAlert("Clear Players?", "Are you sure you would like to clear the list of players?", "Yes", "No");
            if (result)
            {
                PlayerManager.Players.Clear();
                //update the players
                listView.ItemsSource = null;
                listView.ItemsSource = PlayerManager.Players;
                //disable remove button
                lblRemovePlayer.IsEnabled= false;

                //clear the file
                string fileName = "playerList.txt";
                //Find my documents folder on device
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //create file path using file name
                string fullFilePath = Path.Combine(folderPath, fileName);

                if (File.Exists(fullFilePath))
                { 
                    File.Delete(fullFilePath);
                }
            }
        }

        private void ResetGame()
        {
            //reset the Game values
            timeLeft = GlobalVariables.HalfTime;
            GlobalVariables.isSecondHalf = false;
            GlobalVariables.isTimerRunning = false;
            //Update the UI
            DisplayTime(timeLeft);
            lblGameHalf.Text = "Half 1";
            btnTimer.IsEnabled = true ;
            btnTimer.Text = "START GAME TIMER";
            btnTimer.BackgroundColor = Color.FromHex("#1d76db");
            lblGameTime.TextColor = Color.White;
            //reset the player's values
            foreach (Player player in PlayerManager.Players)
            {
                player.OnField = false;
                player.TimeOnField = 0;
                player.Progress = 0;
            }
        }

        private async void btnReset_Pressed(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Reset Game?", "Are you sure you want to reset the game?", "Yes", "No");

            if (result)
            {
                ResetGame();        
            }

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            int oldGameHalf = GlobalVariables.HalfTime;
            //show popup
            var popup = new SetTime();
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            popup.Dismissed += (s, args) => tcs.SetResult(null);
            await Navigation.ShowPopupAsync(popup);

            //wait for user to close the popup
            await tcs.Task;

            //need to verify that the game time changed, or an unnecessary game reset will happen.
            if (oldGameHalf != GlobalVariables.HalfTime)
            {
                //reset the game with global variable changed to new time
                ResetGame();
            }


        }



        private void LoadFileData()
        {
            string fileName = "playerList.txt";
            //Find my documents folder on device
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //create file path using file name
            string fullFilePath = Path.Combine(folderPath, fileName);

            //if file exists, load the data into as list of player objects
            if (File.Exists(fullFilePath))
            { 
                //get all the lines 
                string[] lines = File.ReadAllLines(fullFilePath);
                //used to track the line numbers
                //int count = 0;
                //loop through each line
                foreach (string line in lines)
                {
                    //create new player object
                    Player player = new Player();
                    //number,name
                    string[] data = line.Split(',');

                    player.Number = data[0];
                    player.Name = data[1];
                    player.Progress = 0;
                    player.ProgressColor = Color.Red;
                    //count++;

                    //add the player to the list
                    PlayerManager.Players.Add(player);
                }
            }
        }
    }
}