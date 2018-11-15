using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowMelon.Model
{
    /// <summary>
    /// 재생목록의 음악
    /// Table : ListMusic_TB
    /// </summary>
    public class ListMusic : IDBModel
    {
        /// <summary>
        /// 인덱스
        /// </summary>
        private int lsm_idx;
        public int Index { get => lsm_idx; set => lsm_idx = value; }

        /// <summary>
        /// 음악 인덱스
        /// Foreign Key Music
        /// </summary>
        private int mus_idx;
        public int Music { get => mus_idx; set => mus_idx = value; }

        /// <summary>
        /// 재생목록 순위
        /// </summary>
        private int lsm_pos;
        public int Pos { get => lsm_pos; set => lsm_pos = value; }

        /// <summary>
        /// FK Music
        /// </summary>
        public Music FK_Music { get; set; }
    }
}
