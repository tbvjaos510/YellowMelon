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
        private MusicViewModel musicViewModel = new MusicViewModel();
        public MusicListController()
        {
            this.InitializeComponent();

            InitData();
        }

        private void InitData()
        {
            lvMusicList.ItemsSource = musicViewModel.musics;
        }

        private void LvMusicList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Music clicked = lvMusicList.SelectedItem as Music;
            AddMusicPlayList(this, clicked);
        }

    }
}
