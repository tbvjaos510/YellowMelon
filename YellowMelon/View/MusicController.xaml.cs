using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Playback;
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
    public sealed partial class MusicController : UserControl
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        public event EventHandler requestNext;
        public event EventHandler requestPrev;
        Music music = null;
        PlayerViewModel playerViewModel = new PlayerViewModel();
        bool isPlayed = false;
        private TimeSpan TotalTime;
        bool isLoadded = false;
        bool isDraged = false;
        bool moveMoused = false;
        public MusicController()
        {
            this.InitializeComponent();
            mediaPlayer.Volume = 0.3;
            youtubePlayer.TransportControls = playerContoller;
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded; ;
            youtubePlayer.SetMediaPlayer(mediaPlayer);
            mediaPlayer.SystemMediaTransportControls.IsNextEnabled = true;
            mediaPlayer.SystemMediaTransportControls.PropertyChanged += SystemMediaTransportControls_PropertyChanged;
            PauseBtn.Visibility = Visibility.Collapsed;
            DataContext = mediaPlayer;
        }

        private void SystemMediaTransportControls_PropertyChanged(SystemMediaTransportControls sender, SystemMediaTransportControlsPropertyChangedEventArgs args)
        {

        }

        private async void MediaPlayer_MediaEnded(MediaPlayer sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                requestNext(this, null);
                Music_Pause();
            });
        }

        private async void MediaPlayer_MediaOpened(MediaPlayer sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                TotalTime = mediaPlayer.PlaybackSession.NaturalDuration;
            });
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(mediaPlayer.PlaybackSession.Position.ToString());
            if (isPlayed)
            {
                Music_Pause();
            }
            else
            {
                if (!isLoadded)
                {
                    requestNext(this, null);
                    return;
                }
                Music_Play();
            }
        }
        private void Music_Play()
        {
            PauseBtn.Visibility = Visibility.Visible;
            PlayBtn.Visibility = Visibility.Collapsed;
            isPlayed = true;
            mediaPlayer.Play();
        }
        private void Music_Pause()
        {

            PlayBtn.Visibility = Visibility.Visible;
            PauseBtn.Visibility = Visibility.Collapsed;
            isPlayed = false;
            mediaPlayer.Pause();
        }
        internal void PlayMusic(Music music)
        {
            if (music == null)
            {
                mediaPlayer.Pause();

                isLoadded = false;
                PauseBtn.Visibility = Visibility.Collapsed;
                PlayBtn.Visibility = Visibility.Visible;
                return;
            }
            this.music = music;
            tbMusicName.Text = music.Name;
            tbArtistName.Text = music.FKArtist.NickName;
            mediaPlayer.Pause();

            var updater = mediaPlayer.SystemMediaTransportControls.DisplayUpdater;
            updater.Type = Windows.Media.MediaPlaybackType.Music;
            updater.MusicProperties.Artist = music.FKArtist.NickName;
            updater.MusicProperties.Title = music.Name;
            updater.Update();
            playerViewModel.setSourceAsync(music.Link, mediaPlayer);
            isLoadded = true;
            Music_Play();
        }


        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            requestNext(this, null);
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer.PlaybackSession.Position.TotalSeconds > 5)
            {
                mediaPlayer.PlaybackSession.Position = new TimeSpan(0, 0, 0);
            } else
            {
                requestPrev(this, null);
            }
        }
    }
}
