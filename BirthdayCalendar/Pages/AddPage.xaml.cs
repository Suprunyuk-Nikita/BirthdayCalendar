using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthdayCalendar.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();

            Background.SizeChanged += BackgroundSizeChanged;

            ResizeObjects();
        }

        private void BackgroundSizeChanged(object sender, EventArgs e)
        {
            Background.SizeChanged -= BackgroundSizeChanged;

            ResizeObjects();
        }

        public void ResizeObjects()
        {
            const double oldBackgroundWidth = 320;
            double backgroundWidth = Background.Width;
            double scaleFactor = backgroundWidth / oldBackgroundWidth;
            // ./1,61 margin top

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

        async private void NavBtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FriendsPage());
        }
    }
}