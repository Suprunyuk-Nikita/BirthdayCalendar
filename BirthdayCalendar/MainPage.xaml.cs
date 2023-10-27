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

            Date.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }
        private void BackgroundSizeChanged(object sender, EventArgs e)
        {
            const double oldBackgroundWidth = 320;
            double backgroundWidth = Background.Width;
            double scaleFactor = backgroundWidth / oldBackgroundWidth;

            Date.FontSize = 14d * scaleFactor;
            Date.Margin = new Thickness(0, 0, 0, 13 * scaleFactor);

            Title.FontSize = 26d * scaleFactor;
            Title.Margin = new Thickness(0, 0, 0, 65 * scaleFactor);

            NavBtnHome.HeightRequest = 58d * scaleFactor;
            NavBtnHome.WidthRequest = 58d * scaleFactor;

            NavBtnList.HeightRequest = 58d * scaleFactor;
            NavBtnList.WidthRequest = 58d * scaleFactor;

            test1.HeightRequest = 51d * scaleFactor;
            test1.WidthRequest = 51d * scaleFactor;

            test2.HeightRequest = 51d * scaleFactor;
            test2.WidthRequest = 51d * scaleFactor;

            test3.HeightRequest = 51d * scaleFactor;
            test3.WidthRequest = 51d * scaleFactor;

            test4.HeightRequest = 51d * scaleFactor;
            test4.WidthRequest = 51d * scaleFactor;

            Background.SizeChanged -= BackgroundSizeChanged;
        }
    }
}
