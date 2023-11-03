using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BirthdayCalendar
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Background.SizeChanged += BackgroundSizeChanged;

            ResizeObjects();

            Date.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void BackgroundSizeChanged(object sender, EventArgs e)
        {
            ResizeObjects();
            Background.SizeChanged -= BackgroundSizeChanged;
        }

        public void ResizeObjects()
        {
            const double oldBackgroundWidth = 320;
            double backgroundWidth = Background.Width;
            double scaleFactor = backgroundWidth / oldBackgroundWidth;

            Date.FontSize = 14d * scaleFactor;
            Date.Margin = new Thickness(0, 0, 0, 13d * scaleFactor);

            Title.FontSize = 26d * scaleFactor;
            Title.Margin = new Thickness(0, 0, 0, 65d * scaleFactor);

            NavBtnHome.HeightRequest = 58d * scaleFactor;
            NavBtnHome.WidthRequest = 58d * scaleFactor;

            NavBtnList.HeightRequest = 58d * scaleFactor;
            NavBtnList.WidthRequest = 58d * scaleFactor;
        }

        private async void NavBtnList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactsList());
        }
    }
}
