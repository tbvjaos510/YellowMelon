using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;
using NAudio.Wave;
using System.Diagnostics;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace YellowMelon.ViewModel
{
    public class PlayerViewModel
    {
        public PlayerViewModel()
        {

        }
        
        public async void setSourceAsync(string link, MediaPlayer mediaElement)
        {
            YouTube youTube = YouTube.Default;
            YouTubeVideo video = null;
            video = await youTube.GetVideoAsync(link);
            Debug.WriteLine("Download Display: " + video.Resolution + ", AudioBit: " + video.AudioBitrate + ", 포맷: " + video.Format);
            var memStream = new MemoryStream();
            await (await video.StreamAsync()).CopyToAsync(memStream);
            memStream.Position = 0;
            mediaElement.Source = MediaSource.CreateFromStream(memStream.AsRandomAccessStream(), video.FileExtension);
            mediaElement.Play();
        }
    }
}
