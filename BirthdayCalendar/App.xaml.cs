using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BirthdayCalendar.Pages;
using BirthdayCalendar.Data;
using System.IO;

namespace BirthdayCalendar
{
    public partial class App : Application
    {
        static PersonsDB personsDB;

        public static PersonsDB PersonsDB
        {
            get
            {
                if (personsDB == null)
                {
                    personsDB = new PersonsDB(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "PersonsDatabase.db3"));
                }
                return personsDB;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
