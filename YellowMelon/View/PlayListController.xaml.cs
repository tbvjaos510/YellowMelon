using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
            cbPlayLists.ItemsSource = playListViewModel.PlayLists;
            cbPlayLists.SelectedIndex = 0;
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
            playListViewModel.RemoveMusic(selected.Pos);
        }

        private void LvPlayList_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            if ((e.OriginalSource as FrameworkElement).DataContext == null)
                return;
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
        internal Music GetMusicPrev()
        {
            if (playListViewModel.playList.PlayList.Count == 0)
            {
                new MessageDialog("재생목록에 곡을 넣어주세요").ShowAsync().AsTask();
                return null;
            }
            if (currentMusic <= 1)
            {
                new MessageDialog("이전 곡이 없습니다.").ShowAsync().AsTask();
                return null;
            }
            currentMusic -= 2;
            lvPlayList.SelectedIndex = currentMusic;
            return playListViewModel.playList.PlayList[currentMusic++].FK_Music;
        }

        private void LvPlayList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            currentMusic = lvPlayList.SelectedIndex;
            if (currentMusic > -1)
                requestMusic(this, playListViewModel.playList.PlayList[currentMusic++].FK_Music);
        }

        private void CbPlayLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            playListViewModel.CurrentIdx = cbPlayLists.SelectedIndex;
            lvPlayList.ItemsSource = playListViewModel.playList.PlayList;

        }
        private async Task<string> InputTextDialogAsync(string title)
        {
            TextBox inputTextBox = new TextBox();
            inputTextBox.AcceptsReturn = false;
            inputTextBox.Height = 32;
            ContentDialog dialog = new ContentDialog();
            dialog.Content = inputTextBox;
            dialog.Title = title;
            dialog.IsSecondaryButtonEnabled = true;
            dialog.PrimaryButtonText = "Ok";
            dialog.SecondaryButtonText = "Cancel";
            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
                return inputTextBox.Text;
            else
                return "";
        }

        private async void BtnAddList_Click(object sender, RoutedEventArgs e)
        {
            string name = await InputTextDialogAsync("재생목록 추가");
            if (name != "")
            {
                playListViewModel.AddPlayList(name);
                lvPlayList.ItemsSource = playListViewModel.playList.PlayList;
                cbPlayLists.SelectedIndex = playListViewModel.CurrentIdx;
            }
        }

        private void BtnDelList_Click(object sender, RoutedEventArgs e)
        {
            if (playListViewModel.CurrentIdx == 0)
            {
                new MessageDialog("기본 플레이리스트는 삭제할 수 없습니다.").ShowAsync().AsTask();
                return;
            }
            playListViewModel.RemovePlayList();
            cbPlayLists.SelectedIndex = 0;
        }
    }
}
