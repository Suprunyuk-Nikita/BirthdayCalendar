using BirthdayCalendar.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace BirthdayCalendar.Pages
{
    public partial class MainPage : ContentPage
    {
        private double oldSclollPos = 0;
        private bool system = false;
        List<Person> birthdaysToday = new List<Person>();
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Background.SizeChanged += BackgroundSizeChanged;

            ResizeObjects(GetScaleFactor());
            ShowBirthdayToday(GetScaleFactor());

            Date.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private double GetScaleFactor()
        {
            const double oldBackgroundWidth = 320;
            double backgroundWidth = Background.Width;
            double scaleFactor = backgroundWidth / oldBackgroundWidth;
            return scaleFactor;
        }

        private void BackgroundSizeChanged(object sender, EventArgs e)
        {
            Background.SizeChanged -= BackgroundSizeChanged;

            ResizeObjects(GetScaleFactor());
            ShowBirthdayToday(GetScaleFactor());
        }

        private void ResizeObjects(double scaleFactor)
        {
            Date.FontSize = 14d * scaleFactor;
            Date.Margin = new Thickness(0, 0, 0, 13d * scaleFactor / 1.61d);

            Title.FontSize = 26d * scaleFactor;
            Title.Margin = new Thickness(0, 0, 0, 65d * scaleFactor / 1.61d);

            birthdayList.HeightRequest = 115d * scaleFactor;
            birthdayList.Margin = new Thickness(0, 0, 0, 10d * scaleFactor / 1.61d);

            friendName.FontSize = 16d * scaleFactor;
            friendYearOld.FontSize = 16d * scaleFactor;

            friendInfo.Margin = new Thickness(0, 0, 0, 59 * scaleFactor / 1.61d);

            upcomingBirthdaysLabel.FontSize = 18d * scaleFactor;

            NavBtnHome.HeightRequest = 58d * scaleFactor;
            NavBtnHome.WidthRequest = 58d * scaleFactor;

            NavBtnList.HeightRequest = 58d * scaleFactor;
            NavBtnList.WidthRequest = 58d * scaleFactor;
        }

        private async void ShowBirthdayToday(double scaleFactor)
        {
            try
            {
                List<Person> personsList = new List<Person>(await App.PersonsDB.GetCountAsync());
                personsList = await App.PersonsDB.GetPersonsAsync();

                DateTime dateNow = DateTime.ParseExact(Date.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                birthdaysToday.Clear();
                foreach (Person person in personsList)
                {
                    DateTime personBirthday = DateTime.ParseExact(person.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                    if (personBirthday.Day == dateNow.Day && personBirthday.Month == dateNow.Month)
                    {
                        birthdaysToday.Add(person);
                    }
                }

                if (birthdaysToday.Count > 0)
                {
                    ShowPersonInfo(birthdaysToday);
                    birthdayList.Padding = new Thickness(148d * scaleFactor / 1.61d, 0, 148d * scaleFactor / 1.61d, 0);
                    birthdayList.Children.Clear();
                    foreach (Person person in birthdaysToday)
                    {
                        Frame friendIconMask = new Frame
                        {
                            HeightRequest = 114d * scaleFactor,
                            WidthRequest = 114d * scaleFactor,
                            VerticalOptions = LayoutOptions.Center,
                            CornerRadius = Convert.ToInt32(144d * scaleFactor),
                            Margin = new Thickness(0, 0, 10d * scaleFactor / 1.61d, 0),
                            Padding = new Thickness(0),
                            IsClippedToBounds = true
                        };

                        Image friendIcon = new Image
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Source = person.Image
                        };

                        friendIconMask.Content = friendIcon;
                        birthdayList.Children.Add(friendIconMask);
                    }
                }
                else
                {
                    Label birthdayTodayLabel = new Label
                    {
                        FontSize = 18d * scaleFactor,
                        FontFamily = "Comfortaa",
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("#EFF2F6"),
                        Text = "There are no birthdays today :("
                    };

                    birthdayList.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    birthdayList.Children.Clear();
                    birthdayList.Children.Add(birthdayTodayLabel);
                }
            }
            catch (ArgumentNullException) { ShowBirthdayToday(GetScaleFactor()); }

        }

        private async void BirthdayListScroll_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (sender is ScrollView scrollView && !system)
            {
                system = true;
                int x = (int)Math.Abs(e.ScrollX);
                if (oldSclollPos > x)
                {
                    // Прокрутка вправо
                    await scrollView.ScrollToAsync(x - (x % 155), 0, true);
                    oldSclollPos = x - (x % 155);
                }
                else
                {
                    // Прокрутка влево
                    await scrollView.ScrollToAsync(x + (155 - (x % 155)), 0, true);
                    oldSclollPos = x + (155 - (x % 155));
                }
                system = false;
                ShowPersonInfo(birthdaysToday);
            }
        }

        private async void ShowPersonInfo(List<Person> list)
        {
            try
            {
                int id = (int)oldSclollPos / 155;
                friendName.Text = list[id].Name;
                friendYearOld.Text = (DateTime.Now.Year - DateTime.ParseExact(list[id].Date, "dd.MM.yyyy", CultureInfo.InvariantCulture).Year).ToString();
            }
            catch (IndexOutOfRangeException) { }
            catch (ArgumentOutOfRangeException) { };
        }
        private async void NavBtnList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsPage());
        }
    }
}
