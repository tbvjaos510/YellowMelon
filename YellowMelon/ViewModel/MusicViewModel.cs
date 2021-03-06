﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowMelon.Model;
using DataAccessLibrary;
using Dapper;

namespace YellowMelon.ViewModel
{
    public class MusicViewModel
    {
        public ObservableCollection<Music> musics;

        public MusicViewModel()
        {
            string query = "select a.mus_idx, a.mus_name, a.mus_exp, a.mus_like, a.mus_is_open, a.mus_link, " +
                "b.art_idx, b.u_idx, b.art_nick, b.art_date, b.art_exp, c.gn_idx, c.gn_name from music_tb as a join artist_tb as b on a.art_idx = b.art_idx join " +
                "genre_rf as c on a.gn_idx = c.gn_idx";
            musics = new ObservableCollection<Music>(DataAccess.db.Query<Music, Artist, Genre, Music>(query, (m, a, g) =>
            {
                m.FKGenre = g;
                m.FKArtist = a;
                return m;
            }, splitOn: "art_idx,gn_idx").ToList());

        }
        public MusicViewModel(int i)
        {

        }
        public void LikeMusic(int idx)
        {
            DataAccess.executeQuery("update music_tb set mus_like = mus_like + 1 where mus_idx = " + musics[idx].Index);
            musics[idx].Like++;
        }

        public void AddMusic(Music music)
        {
            /*
                    "mus_idx INTEGER primary key AUTOINCREMENT," +
                "mus_name text not null," +
                "gn_idx int not null," +
                "art_idx int not null," +
                "mus_exp text," +
                "mus_like int not null default 0," +
                "mus_is_open int not null," +
                "mus_link text not null," +
             */
            DataAccess.db.Execute("insert into music_tb (mus_name, gn_idx, art_idx, mus_exp, mus_link, mus_is_open) values " +
                "(@Name, @Genre,  @Artist, @Explain, @Link, @IsOpen)", music);
        }
    }
}
