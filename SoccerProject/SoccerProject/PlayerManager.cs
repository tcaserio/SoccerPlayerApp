using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoccerProject
{
    public static class PlayerManager
    {
        private static List<Player> players;

        private static List<string> playerNames;

        private static GameInfo _gameInfo;

        public static event EventHandler PlayersChanged;
        public static event EventHandler gameInfoChanged;

        static PlayerManager()
        { 
            players = new List<Player>();
        }

        public static List<Player> Players
        {
            get { return players; }
            set
            {
                if (players != value)
                {
                    players = value;
                    PlayersChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static GameInfo gameInfo
        {
            get { return _gameInfo; }
            set 
            {
                if (_gameInfo != value)
                { 
                    _gameInfo = value;
                    gameInfoChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

    }
}
