using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Background.SizeChanged += BackgroundSizeChanged;

            ResizeObjects();

            contactsScore.Text = "0";
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

            NavBtnHome.HeightRequest = 58d * scaleFactor;
            NavBtnHome.WidthRequest = 58d * scaleFactor;

            NavBtnList.HeightRequest = 58d * scaleFactor;
            NavBtnList.WidthRequest = 58d * scaleFactor;
        }

        async private void NavBtnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        async private void AddContactBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPage());
        }
    }
}