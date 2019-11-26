using ExemploAlbertoFeres.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExemploAlbertoFeres.MVVM.View
{
	public partial class PessoaView : ContentPage
	{
		public PessoaView ()
		{
			InitializeComponent ();
            BindingContext = new PessoaViewModel();
		}
	}
}