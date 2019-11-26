using ExemploAlbertoFeres.MVC.Model;
using System;
using System.IO;
using Xamarin.Forms;

namespace ExemploAlbertoFeres.MVC.View_Controller
{
    /*
     * Para efeito didático o codebehind funciona como Controller
     */

    public partial class PessoaPage : ContentPage
	{
        Database db;

        public PessoaPage ()
		{
			InitializeComponent ();
            db = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pessoas.db3"));
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nomeEntry.Text) && !string.IsNullOrWhiteSpace(telefoneEntry.Text))
            {
                await db.SavePessoaAsync(new Pessoa
                {
                    Nome = nomeEntry.Text,
                    Telefone = telefoneEntry.Text
                });

                nomeEntry.Text = telefoneEntry.Text = string.Empty;
                listView.ItemsSource = await db.GetPessoaAsync();
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await db.GetPessoaAsync();
        }
    }
}