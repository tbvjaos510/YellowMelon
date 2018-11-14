using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowMelon.Model
{
    /// <summary>
    /// 유저 테이블
    /// Table : User_TB
    /// </summary>
    public class User
    {
        /// <summary>
        /// 인덱스
        /// </summary>
        private int u_idx;
        public int Index { get => u_idx; set => u_idx = value; }

        /// <summary>
        /// 아이디
        /// </summary>
        private string u_id;
        public string Id { get => u_id; set => u_id = value; }

        /// <summary>
        /// 비밀번호
        /// </summary>
        private string u_pw;
        public string Password { get => u_pw; set => u_pw = value; }

        /// <summary>
        /// 이름
        /// </summary>
        private string u_name;
        public string Name { get => u_name; set => u_name = value; }
    }
}
