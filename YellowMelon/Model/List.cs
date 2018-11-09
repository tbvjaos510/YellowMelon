using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowMelon.Model
{
    /// <summary>
    /// 재생목록 테이블
    /// Table : List_TB
    /// </summary>
    public class List
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
    }
}
