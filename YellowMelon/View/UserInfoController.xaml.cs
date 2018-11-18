using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YellowMelon.Model;
using YellowMelon.ViewModel;
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace YellowMelon.View
{
    public sealed partial class UserInfoController : UserControl
    {
        public event EventHandler MusicAdded;
        MusicViewModel musicView = new MusicViewModel(1);
        UserViewModel userViewModel = new UserViewModel();
        Artist current = null;
        public UserInfoController()
        {
            this.InitializeComponent();
        }
        internal void InitData(User user)
        {
            Artist art = userViewModel.CheckArtist(user.Index);
            DataContext = user;
            if (art != null)
            {
                tbUserType.Text = " 아티스트님";
                current = art;
                btnChangeArtist.Visibility = Visibility.Collapsed;
                btnUpload.Visibility = Visibility.Visible;
            }
            else
            {
                current = new Artist() { User = user.Index, FK_User = user };
                btnUpload.Visibility = Visibility.Collapsed;
            }
        }

        private async void BtnChangeArtist_Click(object sender, RoutedEventArgs e)
        {
            ArtistAddDialog artistAddDialog = new ArtistAddDialog();
            await artistAddDialog.ShowAsync();
            if (artistAddDialog.result != null)
            {
                artistAddDialog.result.User = current.User;
                userViewModel.AddArtist(artistAddDialog.result);
                await new MessageDialog("아티스트로 변환이 완료되었습니다.").ShowAsync();
                InitData(current.FK_User);
            }
        }

        private async void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            MusicAddDialog musicAddDialog = new MusicAddDialog();
            await musicAddDialog.ShowAsync();
            if (musicAddDialog.result != null)
            {
                musicAddDialog.result.Artist = current.Index;
                musicAddDialog.result.Genre++;
                musicAddDialog.result.IsOpen = 1;
                musicView.AddMusic(musicAddDialog.result);
                await new MessageDialog("음악을 성공적으로 등록하였습니다.").ShowAsync();
                MusicAdded?.Invoke(this, null);
            }
        }
    }
}
