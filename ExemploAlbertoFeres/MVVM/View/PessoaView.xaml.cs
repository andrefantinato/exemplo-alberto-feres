using ExemploAlbertoFeres.MVVM.ViewModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExemploAlbertoFeres.MVVM.View
{
	public partial class PessoaView : ContentPage
	{
        private PessoaViewModel _viewmodel => BindingContext as PessoaViewModel;
        public PessoaView ()
		{
			InitializeComponent ();
            BindingContext = new PessoaViewModel();
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void EscolherFoto(object sender, EventArgs e)
        {
            MediaFile file;

            var result = await DisplayActionSheet("Enviar", "Cancelar", "", "Câmera", "Biblioteca");
            if (result == null)
                result = "";

            if (result.Equals("Câmera"))
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    PhotoSize = PhotoSize.Small
                });

                if (file == null)
                    return;

                ImageSelected.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
                ImageSelected.IsVisible = true;


                byte[] data = ReadFully(file.GetStream());
                var dataString = System.Convert.ToBase64String(data);
                _viewmodel.ImageBase64 = dataString;

            }
            else if (result.Equals("Biblioteca"))
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Ops", "Galeria de fotos não suportada.", "OK");
                    return;
                }

                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 500
                });

                if (file == null)
                    return;

                ImageSelected.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
                ImageSelected.IsVisible = true;

                byte[] data = ReadFully(file.GetStream());
                string dataString = System.Convert.ToBase64String(data);
                _viewmodel.ImageBase64 = dataString;
            }

        }

        public static byte[] ReadFully(System.IO.Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}