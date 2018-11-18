using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowMelon.Model
{
    /// <summary>
    /// 음악 테이블
    /// Table : Music_TB
    /// </summary>
    public class Music : IDBModel, INotifyPropertyChanged
    {
        /// <summary>
        /// 인덱스
        /// </summary>
        private int mus_idx;
        public int Index { get => mus_idx; set => mus_idx = value; }

        /// <summary>
        /// 노래 제목
        /// </summary>
        private string mus_name;
        public string Name { get => mus_name; set => mus_name = value; }


        /// <summary>
        /// 장르 아이디
        /// Foreign Key Genre
        /// </summary>
        private int gn_idx;
        public int Genre { get => gn_idx; set => gn_idx = value; }

        /// <summary>
        /// 아티스트 아이디
        /// Foreign key Artist
        /// </summary>
        private int art_idx;
        public int Artist { get => art_idx; set => art_idx = value; }

        /// <summary>
        /// 음악 좋아요 수
        /// </summary>
        private int mus_like;
        public int Like { get => mus_like; set {
                mus_like = value;
                OnPropertyChanged(nameof(Like));
            } }

        /// <summary>
        /// 노래 공개 여부
        /// </summary>
        private int mus_is_open;


        public int IsOpen { get => mus_is_open; set => mus_is_open = value; }

        /// <summary>
        /// 노래 설명
        /// </summary>
        private string mus_exp;
        public string Explain { get => mus_exp; set => mus_exp = value; }


        /// <summary>
        /// 노래 링크 (Youtube)
        /// </summary>
        private string mus_link;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Link { get => mus_link; set => mus_link = value; }

        // FK 설정
        public Artist FKArtist { get; set; }
        public Genre FKGenre { get; set; }

    }
}
