using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

// 사용자 정의 컨트롤 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234236에 나와 있습니다.

namespace YellowMelon.View
{
    public sealed partial class PlayListController : UserControl
    {
        public event EventHandler <Music>requestMusic;
        PlayListViewModel playListViewModel;
        User currentUser;
        int currentMusic = 0;
        ListMusic selected;
        public PlayListController()
        {
            this.InitializeComponent();
        }

        internal void InitPlayList(User user)
        {
            currentUser = user;
            playListViewModel = new PlayListViewModel(user);
            lvPlayList.ItemsSource = playListViewModel.playList.PlayList;
            cbPlayLists.ItemsSource = playListViewModel.PlayLists;
        }

        internal void AddMusic(Music music)
        {
            playListViewModel.AddMusic(music, playListViewModel.playList.PlayList.Count+1);
        }

        private void LvPlayList_Drop(object sender, DragEventArgs e)
        {
            Debug.WriteLine(sender);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int idx = playListViewModel.playList.PlayList.IndexOf(selected);
            playListViewModel.RemoveMusic(idx);
        }

        private void LvPlayList_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView list = sender as ListView;
            playlistMenuFly.ShowAt(list, e.GetPosition(list));
            selected = ((FrameworkElement)e.OriginalSource).DataContext as ListMusic;
        }

        internal Music GetMusic()
        {
            if (playListViewModel.playList.PlayList.Count == 0)
            {
                new MessageDialog("재생목록에 곡을 넣어주세요").ShowAsync().AsTask();
                return null;
            }
            if (playListViewModel.playList.PlayList.Count <= currentMusic)
                currentMusic = 0;
            lvPlayList.SelectedIndex = currentMusic;
            return playListViewModel.playList.PlayList[currentMusic++].FK_Music;
        }

        private void LvPlayList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            currentMusic = lvPlayList.SelectedIndex;
            requestMusic(this, playListViewModel.playList.PlayList[currentMusic++].FK_Music);
        }
    }
}
