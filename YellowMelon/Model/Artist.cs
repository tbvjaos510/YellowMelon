using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowMelon.Model
{
    /// <summary>
    /// 아티스트 테이블
    /// Table : Artist_TB
    /// </summary>
    public class Artist : IDBModel
    {
        /// <summary>
        /// 인덱스 (pk, ai)
        /// </summary>
        private int art_idx;
        public int Index { get => art_idx; set => art_idx = value; }

        /// <summary>
        /// 유저 아이디
        /// Foreign Key User
        /// </summary>
        private int u_idx;
        public int User { get => u_idx; set => u_idx = value; }

        /// <summary>
        /// 닉네임 
        /// </summary>
        private string art_nick;
        public string NickName { get => art_nick; set => art_nick = value; }

        /// <summary>
        /// 활동 일자
        /// </summary>
        private DateTime art_date;
        public DateTime Date { get => art_date; set => art_date = value; }

        /// <summary>
        /// 아티스트 소개
        /// </summary>
        private string art_exp;
        public string Explain { get => art_exp; set => art_exp = value; }
    }
}
