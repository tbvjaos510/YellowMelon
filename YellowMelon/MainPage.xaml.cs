using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YellowMelon.Model;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace YellowMelon
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ctrlMusicList.InitData();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(700, 500));
        }

        private void LoginController_LoginEnd(object sender, EventArgs e)
        {
            ctrlLogin.Visibility = Visibility.Collapsed;
            grMain.Visibility = Visibility.Visible;
            ctrlUser.InitData(sender as User);
            ctrlPlayList.InitPlayList(sender as User);
        }

        private void CtrlMusicList_AddMusicPlayList(object sender, object e)
        {
            Music addMusic = e as Music;
            ctrlPlayList.AddMusic(addMusic);
        }

        private void CtrlMusic_requestNext(object sender, EventArgs e)
        {
            ctrlMusic.PlayMusic(ctrlPlayList.GetMusic());
        }

        private void CtrlPlayList_requestMusic(object sender, object e)
        {
            ctrlMusic.PlayMusic(e as Music);
        }

        private void CtrlMusic_requestPrev(object sender, EventArgs e)
        {
            ctrlMusic.PlayMusic(ctrlPlayList.GetMusicPrev());
        }

        private void CtrlUser_MusicAdded(object sender, EventArgs e)
        {
            ctrlMusicList.InitData();
        }
    }
}
