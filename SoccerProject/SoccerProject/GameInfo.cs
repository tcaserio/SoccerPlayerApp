using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoccerProject
{
    public class GameInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _halfTime;
        private int _timeLeft;
        private bool _isTimerRunning;
        private bool _isSecondHalf;

        public int HalfTime
        {
            get { return _halfTime; }
            set 
            {
                if (_halfTime != value)
                {
                    _halfTime = value;
                    OnPropertyChanged(nameof(HalfTime));
                }
            }
        }
        public int timeLeft
        {
            get { return _timeLeft; }
            set
            {
                if (_timeLeft != value)
                {
                    _timeLeft = value;
                    OnPropertyChanged(nameof(timeLeft));
                }
            }
        }

        public bool isTimerRunning
        {
            get { return _isTimerRunning; }
            set
            {
                if (_isTimerRunning != value)
                {
                    _isTimerRunning = value;
                    OnPropertyChanged(nameof(isTimerRunning));
                }
            }
        }

        public bool isSecondHalf
        {
            get { return _isSecondHalf; }
            set
            {
                if (_isSecondHalf != value)
                {
                    _isSecondHalf = value;
                    OnPropertyChanged(nameof(HalfTime));
                }
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
