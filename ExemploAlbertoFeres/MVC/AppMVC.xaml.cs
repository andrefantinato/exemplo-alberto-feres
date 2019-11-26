using ExemploAlbertoFeres.MVC.Model;
using ExemploAlbertoFeres.MVC.View_Controller;
using System;
using System.IO;
using Xamarin.Forms;

namespace ExemploAlbertoFeres
{
    public partial class AppMVC : Application
    {
        //public static Database db;
        //public static Database Database
        //{
        //    get
        //    {
        //        if (db == null)
        //        {
        //            db = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pessoas.db3"));
        //        }
        //        return db;
        //    }
        //}

        public AppMVC()
        {
            InitializeComponent();
            MainPage = new PessoaPage();
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
