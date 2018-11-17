using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YellowMelon.ViewModel;


namespace YellowMelon.View
{
    public sealed partial class ArtistViewController : UserControl
    {
        public event EventHandler Close;
        ArtistViewModel artistViewModel = new ArtistViewModel();
        public ArtistViewController()
        {
            this.InitializeComponent();
        }
        internal void InitData(int artistIdx)
        {
            DataContext = artistViewModel.GetArtist(artistIdx);
            lvArtistMusic.ItemsSource = artistViewModel.GetMusics(artistIdx);
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close?.Invoke(this, null);
        }
    }
}
