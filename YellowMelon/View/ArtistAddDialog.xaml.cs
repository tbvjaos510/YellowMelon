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
using YellowMelon.Model;
using DataAccessLibrary;
using Dapper;
using Windows.UI.Popups;

namespace YellowMelon.View
{
    public sealed partial class ArtistAddDialog : ContentDialog
    {
        private Artist artist = new Artist();

        public Artist result;
        public ArtistAddDialog()
        {
            this.InitializeComponent();
            this.DataContext = artist;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (artist.NickName == "")
            {
                await new MessageDialog("이름을 적어주세요").ShowAsync();
                return;
            }
            if (artist.Explain == "")
            {
                await new MessageDialog("설명을 적어주세요").ShowAsync();
                return;
            }
            result = artist;
            sender.Hide();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            result = null;
            sender.Hide();
        }
    }
}
