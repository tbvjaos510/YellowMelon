using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowMelon.Model
{
    /// <summary>
    /// 재생목록 테이블
    /// Table : List_TB
    /// </summary>
    public class MList : IDBModel
    {
        /// <summary>
        /// 인덱스
        /// </summary>
        private int lst_idx;
        public int Index { get => lst_idx; set => lst_idx = value; }

        /// <summary>
        /// 유저 아이디
        /// Foreign key : User
        /// </summary>
        private int u_idx;
        public int User { get => u_idx; set => u_idx = value; }

        /// <summary>
        /// 재생목록 제목
        /// </summary>
        private string lst_title;
        public string Title { get => lst_title; set => lst_title = value; }

        /// <summary>
        /// 플레이 리스트
        /// </summary>
        private ObservableCollection<ListMusic> playList;
        public ObservableCollection<ListMusic> PlayList { get => playList; set => playList = value; }

        public override string ToString()
        {
            return this.lst_title;
        }
    }
}
