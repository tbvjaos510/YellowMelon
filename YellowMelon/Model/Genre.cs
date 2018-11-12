using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowMelon.Model
{
    /// <summary>
    /// 장르 테이블
    /// Table : Genre_RF
    /// </summary>
    public class Genre : IDBModel
    {
        /// <summary>
        /// 인덱스 (pk)
        /// </summary>
        private int gn_idx;
        public int Index { get => gn_idx; set => gn_idx = value; }

        /// <summary>
        /// 장르 이름
        /// </summary>
        private string gn_name;
        public string Name { get => gn_name; set => gn_name = value; }
    }
}
