using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Zpr.Fer.Hr.Lumen.Pages;

namespace Zpr.Fer.Hr.Lumen
{
    public partial class App : Application
    {
        private static LumenDatabase _database;
        public static LumenDatabase Database
        {
            get
            {
                if (_database == null)
                    _database = new LumenDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("lumenDb.db3"));
                return _database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
