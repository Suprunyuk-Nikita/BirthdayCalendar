using BirthdayCalendar.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BirthdayCalendar.Pages
{
    public partial class AddPage : ContentPage
    {
        int id = 0;
        public AddPage()
        {
            InitializeComponent();
        }
        public AddPage(int ID)
        {
            InitializeComponent();

            id = ID;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Background.SizeChanged += BackgroundSizeChanged;

            ChangePageContent(GetScaleFactor());

            ResizeObjects(GetScaleFactor());
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

        private async void ChangePageContent(double scaleFactor)
        {
            if (id > 0)
            {
                Person person = new Person();
                person = await App.PersonsDB.GetPersonAsync(id);

                InputName.Text = person.Name.Split(' ')[0];
                InputSurname.Text = person.Name.Split(' ')[1];
                InputDate.Date = DateTime.ParseExact(person.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                

                Button changeBtn = new Button
                {
                    Text = "change",
                    TextColor = Color.FromHex("#EFF2F6"),
                    BackgroundColor = Color.FromHex("#145535"),
                    FontAttributes = FontAttributes.Bold,
                    FontFamily = "Comfortaa",
                    Padding = new Thickness(0, -2, 0, 2),
                    Margin = new Thickness(0, 0, 30d * scaleFactor, 0)
                };

                changeBtn.Clicked += ChangeBtn_Clicked;

                Button deleteBtn = new Button
                {
                    Text = "delete",
                    TextColor = Color.FromHex("#EFF2F6"),
                    BackgroundColor = Color.FromHex("#145535"),
                    FontAttributes = FontAttributes.Bold,
                    FontFamily = "Comfortaa",
                    Padding = new Thickness(0, -2, 0, 2)
                };

                deleteBtn.Clicked += DeleteBtn_Clicked;

                bottomPanel.Children.Clear();
                bottomPanel.Children.Add(changeBtn);
                bottomPanel.Children.Add(deleteBtn);
            }
        }

        private async void ChangeBtn_Clicked(object sender, EventArgs e)
        {
            string name, surname, date, image;
            Person person = new Person();

            if (InputName.Text == null)
            {
                await DisplayAlert("Error", "Invalid name entry", "Ok");
                return;
            }

            if (!InputName.Text.Trim().Contains(" ") & InputName.Text.Trim() != "")
            {
                name = InputName.Text.Trim();
            }
            else
            {
                await DisplayAlert("Error", "Invalid name entry", "Ok");
                return;
            }

            if (InputSurname.Text == null)
            {
                await DisplayAlert("Error", "Invalid surname entry", "Ok");
                return;
            }

            if (!InputSurname.Text.Trim().Contains(" ") & InputSurname.Text.Trim() != "")
            {
                surname = InputSurname.Text.Trim();
            }
            else
            {
                await DisplayAlert("Error", "Invalid surname entry", "Ok");
                return;
            }

            DateTime inputDate = InputDate.Date;
            date = inputDate.ToString("dd.MM.yyyy");

            image = "";
            if (image == "")
            {
                image = "user.png";
            }

            person.ID = id;
            person.Name = name + " " + surname;
            person.Date = date;
            person.Image = image;

            await App.PersonsDB.SavePersonAsync(person);

            await Navigation.PushAsync(new FriendsPage());
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            Person person = await App.PersonsDB.GetPersonAsync(id);
            await App.PersonsDB.DeletePersonAsync(person);

            await Navigation.PushAsync(new FriendsPage());
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

            if (InputName.Text == null)
            {
                await DisplayAlert("Error", "Invalid name entry", "Ok");
                return;
            }

            if (!InputName.Text.Trim().Contains(" ") & InputName.Text.Trim() != "")
            {
                name = InputName.Text.Trim();
            }
            else
            {
                await DisplayAlert("Error", "Invalid name entry", "Ok");
                return;
            }

            if (InputSurname.Text == null)
            {
                await DisplayAlert("Error", "Invalid surname entry", "Ok");
                return;
            }

            if (!InputSurname.Text.Trim().Contains(" ") & InputSurname.Text.Trim() != "")
            {
                surname = InputSurname.Text.Trim();
            }
            else
            {
                await DisplayAlert("Error", "Invalid surname entry", "Ok");
                return;
            }

            DateTime inputDate = InputDate.Date;
            date = inputDate.ToString("dd.MM.yyyy");

            image = "";
            if (image == "")
            {
                image = "user.png";
            }

            person.Name = name + " " + surname;
            person.Date = date;
            person.Image = image;

            await App.PersonsDB.SavePersonAsync(person);

            await Navigation.PushAsync(new FriendsPage());
        }
    }
}