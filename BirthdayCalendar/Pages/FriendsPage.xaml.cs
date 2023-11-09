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
                StackLayout friendStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 0,
                    Margin = new Thickness(0, 0, 0, 20d * scaleFactor)
                };

                Frame friendIconMask = new Frame
                {
                    HeightRequest = 40d * scaleFactor,
                    WidthRequest = 40d * scaleFactor,
                    VerticalOptions = LayoutOptions.Center,
                    CornerRadius = Convert.ToInt32(40d * scaleFactor),
                    Padding = new Thickness(0),
                    IsClippedToBounds = true
                };

                Image friendIcon = new Image
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Source = person.Image
                };

                Label friendInfo = new Label
                {
                    FontSize = 16d * scaleFactor,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Center,
                    FontFamily = "Comfortaa",
                    TextColor = Color.FromHex("#EFF2F6"),
                    Padding = new Thickness(17d * scaleFactor, -2d * scaleFactor, 0, 0),
                    Text = person.Name + "\n" + person.Date
                };

                Grid friendBtnBox = new Grid
                {
                    HeightRequest = 34d * scaleFactor,
                    WidthRequest = 34d * scaleFactor,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                Button friendBtn = new Button
                {
                    HeightRequest = 34d * scaleFactor,
                    WidthRequest = 34d * scaleFactor,
                    BackgroundColor = Color.FromHex("#0000"),
                    TextColor = Color.FromHex("#0000"),
                    Text = person.ID.ToString()
                };
                friendBtn.Clicked += FriendBtn_Clicked;

                Image friendBtnIcon = new Image
                {
                    HeightRequest = 34d * scaleFactor,
                    WidthRequest = 34d * scaleFactor,
                    Source = "person_button.png"
                };

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
            //await DisplayAlert("friend ID", $"{(sender as Button).Text}", "Ok");
            //string btnText = (sender as Button).Text;
            //int id = Convert.ToInt32(btnText);
            //Person person = await App.PersonsDB.GetPersonAsync(id);
            //await App.PersonsDB.DeletePersonAsync(person);

            //int score = await App.PersonsDB.GetCountAsync();
            //contactsScore.Text = score.ToString();

            //friendsList.Children.Clear();
            //ShowDB(GetScaleFactor());

            string btnText = (sender as Button).Text;
            int id = Convert.ToInt32(btnText);
            await Navigation.PushAsync(new AddPage(id));
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