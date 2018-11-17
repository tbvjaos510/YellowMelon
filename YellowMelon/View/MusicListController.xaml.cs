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
using YellowMelon.ViewModel;
using YellowMelon.Model;
using System.Diagnostics;
// 사용자 정의 컨트롤 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234236에 나와 있습니다.

namespace YellowMelon.View
{
    public sealed partial class MusicListController : UserControl
    {
        public event EventHandler<Music> AddMusicPlayList;
        private MusicViewModel musicViewModel;
        private Music selected;
        public MusicListController()
        {
            this.InitializeComponent();
        }

        internal void setData(List<Music> musics)
        {
            lvMusicList.ItemsSource = musics;
        }
        internal void InitData()
        {
            musicViewModel = new MusicViewModel();
            lvMusicList.ItemsSource = musicViewModel.musics;
        }

        private void LvMusicList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Music clicked = lvMusicList.SelectedItem as Music;
            AddMusicPlayList(this, clicked);
        }

        
        private void CtrlArtist_Close(object sender, EventArgs e)
        {
            lvMusicList.Visibility = Visibility.Visible;
            ctrlArtistView.Visibility = Visibility.Collapsed;
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine("Clicked!!!!!");
        }

        private void LvMusicList_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView list = sender as ListView;
            playlistMenuFly.ShowAt(list, e.GetPosition(list));
            selected = ((FrameworkElement)e.OriginalSource).DataContext as Music;
        }

        private void ShowArtist_Click(object sender, RoutedEventArgs e)
        {
            ctrlArtistView.InitData(selected.FKArtist.Index);
            lvMusicList.Visibility = Visibility.Collapsed;
            ctrlArtistView.Visibility = Visibility.Visible;
        }

        private void LikeMusic_Click(object sender, RoutedEventArgs e)
        {
            musicViewModel.LikeMusic(musicViewModel.musics.IndexOf(selected));
        }
    }
}
