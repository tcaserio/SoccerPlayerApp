using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoccerProject
{
    public class Player : INotifyPropertyChanged
    {
        private string number;
        private string name;
        private bool onField;
        private int timeOnField;
        private double _progress;
        private Color _progressColor;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Number
        {
            get { return number; }
            set
            {
                if (number != value)
                {
                    number = value;
                    OnPropertyChanged(nameof(Number));
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress)); // Notify binding system
                UpdateProgressColor(); // Update color based on new progress value
            }
        }

        private void UpdateProgressColor()
        {
            if (Progress <= 25)
            {
                ProgressColor = Color.Red;
            }
            else if (Progress < 50)
            {
                ProgressColor = Color.Yellow;
            }
            else
            {
                ProgressColor = Color.Green;
            }
        }
        public Color ProgressColor
        {
            get => _progressColor;
            set
            {
                if (_progressColor != value) // Only update if value is different
                {
                    _progressColor = value;
                    OnPropertyChanged(nameof(ProgressColor)); // Notify binding system
                }
            }
        }

        public bool OnField
        {
            get { return onField; }
            set
            {
                if (onField != value)
                {
                    onField = value;
                    OnPropertyChanged(nameof(OnField));
                }
            }
        }

        public int TimeOnField
        {
            get { return timeOnField; }
            set { timeOnField = value; }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateProgress(int gameTime)
        { 
            //calculate the progress percentage
            Progress = (Convert.ToDouble(TimeOnField) / Convert.ToDouble(gameTime)) * 100;
        }
    }
}
