using ExemploAlbertoFeres.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExemploAlbertoFeres.MVVM.ViewModel
{
    public class PessoaViewModel : BaseViewModel
    {
        private Database db;

        public ICommand SalvarPessoaCommand
        {
            get;
            set;
        }
        public ObservableCollection<Pessoa> Pessoas
        {
            get;
            set;
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set
            {
                SetProperty(ref _nome, value);
            }
        }

        private string _telefone;
        public string Telefone
        {
            get { return _telefone; }
            set
            {
                SetProperty(ref _telefone, value);
            }
        }

        public PessoaViewModel()
        {
            this.db = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pessoas.db3"));
            this.Pessoas = new ObservableCollection<Pessoa>();
            this.SalvarPessoaCommand = new Command(this.SalvarPessoa);
        }

        async private void SalvarPessoa()
        {
            Pessoa p = new Pessoa
            {
                Nome = Nome,
                Telefone = Telefone
            };
            await this.db.SavePessoaAsync(p);
            await this.LoadAsync();
        }

        public override async Task LoadAsync()
        {
            var pessoas = await this.db.GetPessoaAsync();

            Pessoas.Clear();
            if (pessoas == null || pessoas.Count == 0)
            {
                await DisplayAlert("Ops!", "Nenhum resultado encontrado.", "Ok");
                return;
            }

            foreach (var p in pessoas)
            {
                Pessoas.Add(p);
            }
        }

    }
}
