using ExemploAlbertoFeres.MVVM.View;
using Xamarin.Forms;

namespace ExemploAlbertoFeres
{
    public partial class AppMVVM : Application
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

        public AppMVVM()
        {
            InitializeComponent();
            MainPage = new PessoaView();
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
