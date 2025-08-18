using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms.Core;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SoccerProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class ListItemViewCell : ViewCell
    {
        public ListItemViewCell()
        {
            // Create labels and controls for each element of the ListItem
            var idLabel = new Label 
            { 
                TextColor = Color.Black,
                FontSize = 18,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(10),
            };
            var nameLabel = new Label
            {
                TextColor = Color.Black,
                FontSize = 18,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0, 0, 0, 0)

            };
            var progressLabel = new Label
            {
                TextColor = Color.Black,
                FontSize = 18,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 75, 0),
            };
            var toggleSwitch = new Switch
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(10,0,40,0)
            };


            // Bind the labels and controls to the corresponding properties of the ListItem
            idLabel.SetBinding(Label.TextProperty, new Binding("Number"));
            nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
            progressLabel.SetBinding(Label.TextProperty, (new Binding("Progress", stringFormat: "{0:0}%")));
            progressLabel.SetBinding(Label.TextColorProperty, new Binding("ProgressColor", converter: new ColorConverter()));
            toggleSwitch.SetBinding(Switch.IsToggledProperty, new Binding("OnField"));


            // Add the labels and controls to a horizontal StackLayout
            var layout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                idLabel,
                nameLabel,
                progressLabel,
                toggleSwitch
            }
            };

            // Set the ViewCell's View property to the StackLayout
            View = layout;
        }

    }

}
