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
    public sealed partial class MusicAddDialog : ContentDialog
    {
        private Music music = new Music();

        public Music result;
        public MusicAddDialog()
        {
            this.InitializeComponent();
            this.GetGenre();
            this.DataContext = music;
        }
        private void GetGenre()
        {
            cbGenre.ItemsSource = DataAccess.db.Query<Genre>("select * from genre_rf").AsList();
            cbGenre.SelectedIndex = 0;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (music.Name == "")
            {
                await new MessageDialog("이름을 적어주세요").ShowAsync();
                return;
            }
            if (music.Explain == "")
            {
                await new MessageDialog("설명을 적어주세요").ShowAsync();
                return;
            }
            if (music.Link == "")
            {
                await new MessageDialog("유튜브 링크를 적어주세요").ShowAsync();
                return;
            }
            result = music;
            sender.Hide();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            result = null;
            sender.Hide();
        }

        private void CbGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            music.Genre = cbGenre.SelectedIndex;
        }
    }
}
