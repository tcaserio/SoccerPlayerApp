using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoccerProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlayerPopup : Popup
    {
        string folderPath = "";
        string fileName = "playerList.txt";
        string fullFilePath = "";
        public AddPlayerPopup()
        {
            InitializeComponent();
        }

        private void btnAddPlayer_Pressed(object sender, EventArgs e)
        {
            //get the player number from the entry
            string playerNum = '#' + entNumber.Text;
            //get the player name from the entry
            string playerName = entName.Text;
            //add the player manager to the list of players in the Player Manager
            PlayerManager.Players.Add(new Player
            {
                Number = playerNum,
                Name  = playerName,
                Progress = 0,
                OnField = false,
                TimeOnField = 0
            });

            //update the path
            GetFilePath();
            //create a string of all players and numbers to be written to file
            string fileString = "";
            //add the last used game time
            //fileString += GlobalVariables.HalfTime + "\n";

            foreach (Player player in PlayerManager.Players)
            {
                //add each player to the file string
                fileString += $"{player.Number},{player.Name}\n";
            }
            //overwrite the file with the player list - not efficient, but easier to implement
            File.WriteAllText(fullFilePath, fileString);

            //int count = PlayerManager.Players.Count;
            Dismiss(true);
        }

        private void entNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entNumber.Text != null && entName.Text != null)
            {
                btnAddPlayer.IsEnabled = true;
            }
            if (entNumber.Text == "" || entName.Text == "")
            {
                btnAddPlayer.IsEnabled = false;
            }
        }

        //name
        private void entNumber_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (entNumber.Text != null && entName.Text != null)
            {
                btnAddPlayer.IsEnabled = true;
            }
            if (entNumber.Text == "" || entName.Text == "")
            {
                btnAddPlayer.IsEnabled = false;
            }
        }

        /// <summary>
        /// find and store the file path as string
        /// </summary>
        private void GetFilePath()
        {
            //Find my documents folder on device
            folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //create file path using file name
            fullFilePath = Path.Combine(folderPath, fileName);
        }
    }
}