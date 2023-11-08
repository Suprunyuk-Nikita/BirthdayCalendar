using Android.Content;
using Android.Widget;
using BirthdayCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthdayCalendar.Pages
{
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
        }
        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Background.SizeChanged += BackgroundSizeChanged;

            ResizeObjects();
        }

        private void BackgroundSizeChanged(object sender, EventArgs e)
        {
            Background.SizeChanged -= BackgroundSizeChanged;

            ResizeObjects();
        }

        private void ResizeObjects()
        {
            const double oldBackgroundWidth = 320;
            double backgroundWidth = Background.Width;
            double scaleFactor = backgroundWidth / oldBackgroundWidth;

            NavBar.Margin = new Thickness(0, 68d * scaleFactor / 1.61d, 0, 0);

            NavBtnBack.HeightRequest = 58d * scaleFactor;
            NavBtnBack.WidthRequest = 58d * scaleFactor;

            NavBtnAdd.HeightRequest = 58d * scaleFactor;
            NavBtnAdd.WidthRequest = 58d * scaleFactor;

            Title.FontSize = 26 * scaleFactor;
            Title.Margin = new Thickness(0, 22d * scaleFactor / 1.61d, 0, 0);

            InputPanel.Margin = new Thickness(0, 75d * scaleFactor, 0, 0);

            NameLabel.FontSize = 16d * scaleFactor;

            SurnameLabel.FontSize = 16d * scaleFactor;

            DateLabel.FontSize = 16d * scaleFactor;

            PhotoLabel.FontSize = 16d * scaleFactor;

            InputName.FontSize = 16d * scaleFactor;
            InputName.Margin = new Thickness(10d * scaleFactor, -4d * scaleFactor, 0, 0);

            InputSurname.FontSize = 16d * scaleFactor;
            InputSurname.Margin = new Thickness(10d * scaleFactor, -4d * scaleFactor, 0, 0);

            InputDate.Margin = new Thickness(10d * scaleFactor, -2d * scaleFactor, 0, 0);

            PhotoBtnLabel.FontSize = 12d * scaleFactor;
            PhotoBtnLabel.Margin = new Thickness(15d * scaleFactor, 5d * scaleFactor, 0, 0);

            PhotoBtn.HeightRequest = 20 * scaleFactor;
            PhotoBtn.WidthRequest = 100d * scaleFactor;
            PhotoBtn.Margin = new Thickness(10d * scaleFactor, 5d * scaleFactor, 0, 0);

            AddBtn.FontSize = 16d * scaleFactor;

        }

        private async void NavBtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsPage());
        }

        private async void PhotoBtn_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Warning", "In developing...", "Ok");
        }

        private async void AddBtn_Clicked(object sender, EventArgs e)
        {
            string name, surname, date, image;
            Person person = new Person();

            if (!InputName.Text.Trim().Contains(" ") && InputName.Text.Trim() != "")
            {
                name = InputName.Text.Trim();
            }
            else
            {
                await DisplayAlert("Error", "Invalid name entry", "Ok");
                return;
            }

            if (!InputSurname.Text.Trim().Contains(" ") && InputSurname.Text.Trim() != "")
            {
                surname = InputSurname.Text.Trim();
            }
            else
            {
                await DisplayAlert("Error", "Invalid name entry", "Ok");
                return;
            }

            DateTime inputDate = InputDate.Date;
            date = inputDate.ToString("dd.MM.yyyy");

            image = "";
            if (image == "")
            {
                image = "user.png";
            }

            person.Name = name;
            person.Surname = surname;
            person.Date = date;
            person.Image = image;
            
            
        }
    }
}