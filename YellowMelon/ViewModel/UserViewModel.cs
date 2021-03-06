﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using DataAccessLibrary;
using System.Diagnostics;
using Dapper;
using YellowMelon.Model;

namespace YellowMelon.ViewModel
{
    public class UserViewModel
    {
        public User Login(string id, string pw)
        {
            return DataAccess.db.Query<User>("select * from user_tb where u_id=@id and u_pw=@pw", new { id = id, pw = pw }).FirstOrDefault();
        }

        public bool Signup(string id, string pw, string name)
        {
            try
            {
                return DataAccess.executeQuery("insert into user_tb ('u_id', 'u_pw', 'u_name') values ('" + id + "', '" + pw + "', '" + name + "')").RecordsAffected > 0;
            } catch (SqliteException e)
            {
                return false;
            }
        }
        public Artist CheckArtist(int id)
        {
            return DataAccess.db.Query<Artist>("select * from artist_tb where u_idx=@id", new { id }).FirstOrDefault();
        }

        public void AddArtist(Artist artist)
        {
            DataAccess.db.Execute("insert into artist_tb (u_idx, art_nick, art_exp) values (@User, @NickName, @Explain)", artist);
        }
    }
}
