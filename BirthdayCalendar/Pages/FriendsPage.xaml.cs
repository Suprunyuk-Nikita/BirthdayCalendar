using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BirthdayCalendar.Models;

namespace BirthdayCalendar.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsPage : ContentPage
    {
        public FriendsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Background.SizeChanged += BackgroundSizeChanged;

            ResizeObjects(GetScaleFactor());

            int score = await App.PersonsDB.GetCountAsync();
            contactsScore.Text = score.ToString();

            ShowDB(GetScaleFactor());
        }

        private void BackgroundSizeChanged(object sender, EventArgs e)
        {

            Background.SizeChanged -= BackgroundSizeChanged;

            ResizeObjects(GetScaleFactor());
        }

        private double GetScaleFactor()
        {
            const double oldBackgroundWidth = 320;
            double backgroundWidth = Background.Width;
            double scaleFactor = backgroundWidth / oldBackgroundWidth;
            return scaleFactor;
        }

        private void ResizeObjects(double scaleFactor)
        {
            searchPanel.HeightRequest = 30d * scaleFactor;
            searchPanel.WidthRequest = 187d * scaleFactor;

            searchIcon.HeightRequest = 16d * scaleFactor;
            searchIcon.WidthRequest = 16d * scaleFactor;
            searchIcon.Margin = new Thickness(9d * scaleFactor, 7d * scaleFactor, 7d * scaleFactor, 7d * scaleFactor);

            searchInput.HeightRequest = 16d * scaleFactor;
            searchInput.WidthRequest = 148d * scaleFactor;
            searchInput.FontSize = 10d * scaleFactor;

            addContactIcon.HeightRequest = 24d * scaleFactor;
            addContactIcon.WidthRequest = 24d * scaleFactor;

            addContactBtn.HeightRequest = 24d * scaleFactor;
            addContactBtn.WidthRequest = 24d * scaleFactor;
            addContactBtn.Margin = new Thickness(0, 3d * scaleFactor);

            contactsScore.FontSize = 16d * scaleFactor;

            friendsBox.Margin = new Thickness(0, 10d * scaleFactor);

            NavBtnHome.HeightRequest = 58d * scaleFactor;
            NavBtnHome.WidthRequest = 58d * scaleFactor;

            NavBtnList.HeightRequest = 58d * scaleFactor;
            NavBtnList.WidthRequest = 58d * scaleFactor;
        }

        private async void ShowDB(double scaleFactor)
        {
            List<Person> personsList = new List<Person>(await App.PersonsDB.GetCountAsync());
            personsList = await App.PersonsDB.GetPersonsAsync();

            foreach (Person person in personsList)
            {
                StackLayout friendStackLayout = new StackLayout();
                friendStackLayout.Orientation = StackOrientation.Horizontal;
                friendStackLayout.Spacing = 0;
                friendStackLayout.Margin = new Thickness(0, 0, 0, 20d * scaleFactor);

                Frame friendIconMask = new Frame();
                friendIconMask.HeightRequest = 40d * scaleFactor;
                friendIconMask.WidthRequest = 40d * scaleFactor;
                friendIconMask.VerticalOptions = LayoutOptions.Center;
                friendIconMask.CornerRadius = Convert.ToInt32(40d * scaleFactor);
                friendIconMask.Padding = new Thickness(0);
                friendIconMask.IsClippedToBounds = true;

                Image friendIcon = new Image();
                friendIcon.HorizontalOptions = LayoutOptions.Center;
                friendIcon.VerticalOptions = LayoutOptions.Center;
                friendIcon.Source = person.Image;

                Label friendInfo = new Label();
                friendInfo.FontSize = 16d * scaleFactor;
                friendInfo.FontAttributes = FontAttributes.Bold;
                friendInfo.VerticalOptions = LayoutOptions.Center;
                friendInfo.FontFamily = "Comfortaa";
                friendInfo.TextColor = Color.FromHex("#EFF2F6");
                friendInfo.Padding = new Thickness(17d * scaleFactor, -2d * scaleFactor, 0, 0);
                friendInfo.Text = person.Name + "\n" + person.Date;

                Grid friendBtnBox = new Grid();
                friendBtnBox.HeightRequest = 34d * scaleFactor;
                friendBtnBox.WidthRequest = 34d * scaleFactor;
                friendBtnBox.VerticalOptions = LayoutOptions.Center;
                friendBtnBox.HorizontalOptions = LayoutOptions.EndAndExpand;

                Button friendBtn = new Button();
                friendBtn.HeightRequest = 34d * scaleFactor;
                friendBtn.WidthRequest = 34d * scaleFactor;
                friendBtn.BackgroundColor = Color.FromHex("#0000");
                friendBtn.TextColor = Color.FromHex("#0000");
                friendBtn.Clicked += FriendBtn_Clicked;
                friendBtn.Text = person.ID.ToString();

                Image friendBtnIcon = new Image();
                friendBtnIcon.HeightRequest = 34d * scaleFactor;
                friendBtnIcon.WidthRequest = 34d * scaleFactor;
                friendBtnIcon.Source = "person_button.png";

                friendIconMask.Content = friendIcon;
                friendStackLayout.Children.Add(friendIconMask);
                friendStackLayout.Children.Add(friendInfo);

                friendBtnBox.Children.Add(friendBtnIcon);
                friendBtnBox.Children.Add(friendBtn);

                friendStackLayout.Children.Add(friendBtnBox);

                friendsList.Children.Add(friendStackLayout);
            }
        }

        private async void FriendBtn_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("friend ID", $"{(sender as Button).Text}", "Ok");
        }
        private async void NavBtnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void AddContactBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }
    }
}