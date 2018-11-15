using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using YellowMelon.ViewModel;

// 사용자 정의 컨트롤 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234236에 나와 있습니다.

namespace YellowMelon.View
{
    public sealed partial class PlayListController : UserControl
    {
        PlayListViewModel playListViewModel;
        User currentUser;
        public PlayListController()
        {
            this.InitializeComponent();
        }

        internal void InitPlayList(User user)
        {
            currentUser = user;
            playListViewModel = new PlayListViewModel(user);
            lvPlayList.ItemsSource = playListViewModel.playList.PlayList;
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
            Debug.WriteLine(sender);
        }
    }
}
