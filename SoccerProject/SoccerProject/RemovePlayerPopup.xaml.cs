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
    public partial class RemovePlayerPopup : Popup
    {
        string folderPath = "";
        string fileName = "playerList.txt";
        string fullFilePath = "";

        public RemovePlayerPopup()
        {
            InitializeComponent();

            List<string> nameAndNumbers = new List<string>();

            //create a list of player names and numbers to display in the picker
            foreach (Player player in PlayerManager.Players)
            {
                string playerNameAndNumbers = $"{player.Number} {player.Name}";
                nameAndNumbers.Add(playerNameAndNumbers);
            }

            pckRemovePlayer.ItemsSource = nameAndNumbers;

            //only enable if something is selected in the picker
            if (pckRemovePlayer.SelectedIndex != -1)
            { 
                btnRemovePlayer.IsEnabled = true;
            }
        }

        private void btnRemovePlayer_Pressed(object sender, EventArgs e)
        {
            //remove player from the list of players
            PlayerManager.Players.RemoveAt(pckRemovePlayer.SelectedIndex);

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
            //overwrite the file with updated player list - not efficient, but easier to implement
            File.WriteAllText(fullFilePath, fileString);

            Dismiss(true);
        }

        private void pckRemovePlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pckRemovePlayer.SelectedIndex != -1)
            {
                btnRemovePlayer.IsEnabled = true;
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