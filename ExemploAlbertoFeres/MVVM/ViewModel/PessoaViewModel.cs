using ExemploAlbertoFeres.MVVM.Model;
using Plugin.Messaging;
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

        public ICommand ShowPessoaCommand
        {
            get;
            set;
        }

        private Pessoa _selectedItem;
        public Pessoa SelectedPessoa
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                //OnPropertyChanged("SelectedPessoa");
                ShowPessoaCommand.Execute(_selectedItem);
                //SelectedPessoa = null;
            }
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

        private string _image;
        public string ImageBase64
        {
            get { return _image; }
            set
            {
                SetProperty(ref _image, value);
            }
        }

        public PessoaViewModel()
        {
            this.db = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pessoas.db3"));
            this.Pessoas = new ObservableCollection<Pessoa>();
            this.SalvarPessoaCommand = new Command(this.SalvarPessoa);
            this.ShowPessoaCommand = new Command(this.ShowPessoa);
            this.LoadAsync();
        }

        async private void ShowPessoa()
        {
            string result = await AppMVVM.Current.MainPage.DisplayActionSheet("o que deseja fazer?", "Deletar", "Telefonar");
            if (result == null)
                result = "";

            if(result.Equals("Deletar"))
            {
                await this.db.DeletePessoaAsync(this.SelectedPessoa);
                await this.LoadAsync();
            }
            else if(result.Equals("Telefonar"))
            {
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall)
                    phoneDialer.MakePhoneCall(this.SelectedPessoa.Telefone);
            }

            this.SelectedPessoa = null;
        }

        async private void SalvarPessoa()
        {
            Pessoa p = new Pessoa
            {
                Nome = Nome,
                Telefone = Telefone,
                ImageBase64 = ImageBase64
            };
            await this.db.SavePessoaAsync(p);
            await this.LoadAsync();

            this.Nome = "";
            this.Telefone = "";
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
