using System;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;

namespace DataAccessLibrary
{
    public class DataAccess
    {
        public static SqliteConnection db = new SqliteConnection(@"Filename=yellowmelon.db");
        static DataAccess()
        {
            InitializeDatabase();
        }

        public static void dropAllTable()
        {
            executeQuery("Drop table if exists listmusic_tb");
            executeQuery("Drop table if exists  list_tb");
            executeQuery("Drop table if exists  music_tb");
            executeQuery("Drop table if exists  genre_rf");
            executeQuery("Drop table if exists  artist_tb");
            executeQuery("Drop table if exists  user_tb");
        }
        public static void InitGenre()
        {
            try
            {
                execute("insert into genre_rf (gn_idx, gn_name) values (1, '발라드')");
                execute("insert into genre_rf (gn_idx, gn_name) values (2, '팝')");
                execute("insert into genre_rf (gn_idx, gn_name) values (3, 'OST')");
                execute("insert into genre_rf (gn_idx, gn_name) values (4, '인디음악')");
                execute("insert into genre_rf (gn_idx, gn_name) values (5, 'R&B / Soul')");
            } catch (SqliteException e) {
                Debug.Write(e);
            }
        }
        public static void InitUser()
        {
            try
            {
                execute("insert into user_tb (u_idx, u_id, u_pw, u_name) values " +
                    "(500, 'dlwldms', 'dlwldms', '이지은')");
                execute("insert into user_tb (u_idx, u_id, u_pw, u_name) values " +
                    "(501, 'bol4', 'qhftk', '볼빨간사춘기')");
                execute("insert into user_tb (u_idx, u_id, u_pw, u_name) values " +
                    "(502, 'hello', 'world', '사용자')");
                execute("insert into user_tb (u_idx, u_id, u_pw, u_name) values " +
                    "(503, 'gdragon', 'dragon', '권지용')");

            } catch (Exception) { }
        }
        public static void InitArtist()
        {
            try
            {
                execute("insert into artist_tb (art_idx, u_idx, art_nick, art_exp, art_date) values " +
                    "(1, 500, '아이유', '안녕하세요 아이유입니다', '2008-09-18')");
                execute("insert into artist_tb (art_idx, u_idx, art_nick, art_exp, art_date) values " +
                    "(2, 501, '볼빨간사춘기', '안녕하세요 볼빨간사춘기입니다.', '2016-04-22')");
                execute("insert into artist_tb (art_idx, u_idx, art_nick, art_exp, art_date) values " +
                    "(3, 503, 'G-DRAGON', 'GDragon!!!', '2009-08-01')");
            } catch (Exception) { }
        }

        public static void InitMusic()
        {
            try
            {
                execute("insert into music_tb (mus_idx, mus_name, gn_idx, art_idx, mus_exp, mus_like, mus_is_open, mus_link) values" +
                    @"(1, '삐삐', 5, 1, '올 해로 꼭 데뷔 10주년을 맞이한 아이유가 디지털 싱글 ‘삐삐’를 발표한다.', 0, 1, " +
                    "'https://www.youtube.com/watch?v=nM0xDI5R50E')");
                execute("insert into music_tb (mus_idx, mus_name, gn_idx, art_idx, mus_exp, mus_like, mus_is_open, mus_link) values" +
                    @"(2, '밤편지', 1, 1, '이 밤, 당신을 잠 못 들게 할, 모두가 기다린 아이유의 목소리, 그리고 이야기.', 0, 1," +
                    "'https://www.youtube.com/watch?v=BzYnNdJhZQw')");
                execute("insert into music_tb (mus_idx, mus_name, gn_idx, art_idx, mus_exp, mus_like, mus_is_open, mus_link) values" +
                    @"(3, '사랑이 잘', 5, 1, '이름만으로도 충분한, 동갑내기 뮤지션 ‘아이유’와 ‘오혁’의 첫 콜라보레이션', 0, 1," +
                    "'https://www.youtube.com/watch?v=dMn509ddAkc')");
                execute("insert into music_tb (mus_idx, mus_name, gn_idx, art_idx, mus_exp, mus_like, mus_is_open, mus_link) values" +
                    @"(4, '여행', 4, 2, '한편의 동화 같은, 궁금해서 자꾸만 넘겨보고 싶은 볼빨간사춘기 [Red Diary Page.2]', 0, 1," +
                    "'https://www.youtube.com/watch?v=xRbPAVnqtcs')");
                execute("insert into music_tb (mus_idx, mus_name, gn_idx, art_idx, mus_exp, mus_like, mus_is_open, mus_link) values" +
                    @"(5, 'Dejavu', 4, 2, '볼빨간사춘기 Red Diary, 그 마지막 장', 0, 1," +
                    "'https://www.youtube.com/watch?v=oPNmqG4o-GM')");
                execute("insert into music_tb (mus_idx, mus_name, gn_idx, art_idx, mus_exp, mus_like, mus_is_open, mus_link) values" +
                    @"(6, '썸 탈꺼야', 4, 2, '어설퍼 귀여운, 완벽하지 않기에 더 사랑스러운 사춘기의 감정들.', 0, 1," +
                    "'https://www.youtube.com/watch?v=hZmoMyFXDoI')");
                execute("insert into music_tb (mus_idx, mus_name, gn_idx, art_idx, mus_exp, mus_like, mus_is_open, mus_link) values" +
                    @"(7, 'LAST DANCE', 1, 3, '‘MADE SERIES’ 의 마침표이자 10주년 기념 프로젝트 대미의 장식', 0, 1," +
                    "'https://www.youtube.com/watch?v=--zku6TB5NY')");
                execute("insert into music_tb (mus_idx, mus_name, gn_idx, art_idx, mus_exp, mus_like, mus_is_open, mus_link) values" +
                    "(8, '무제(Untitled)', 5, 3, '이번 솔로 앨범은 그의 30대의 출발점이자, 인생 제 3막의 시작을 알리는 앨범이기도 하다', 0, 1," +
                    "'https://www.youtube.com/watch?v=9kaCAbIXuyg')");

            }
            catch (Exception) { }
        }

        public static void InitializeDatabase()
        {
            db.Open();
         //   dropAllTable();
            string Create_user_tb = "create table if not exists user_tb (" +
                "u_idx INTEGER primary key AUTOINCREMENT," +
                "u_id varchar(45) not null unique," +
                "u_pw varchar(50) not null," +
                "u_name text not null" +
                ")";
            string Create_artist_tb = "create table if not exists artist_tb (" +
                "art_idx INTEGER primary key AUTOINCREMENT," +
                "u_idx int not null," +
                "art_nick varchar(45) not null," +
                "art_date date default CURRENT_TIMESTAMP," +
                "art_exp text," +
                "foreign key(`u_idx`) references `user_tb` (`u_idx`) on delete cascade" +
                ")";
            string Create_genre_rf = "create table if not exists genre_rf (" +
                "gn_idx INTEGER primary key AUTOINCREMENT," +
                "gn_name varchar(45) not null" +
                ")";
            string Create_music_tb = "create table if not exists music_tb (" +
                "mus_idx INTEGER primary key AUTOINCREMENT," +
                "mus_name text not null," +
                "gn_idx int not null," +
                "art_idx int not null," +
                "mus_exp text," +
                "mus_like int not null default 0," +
                "mus_is_open int not null," +
                "mus_link text not null," +
                "foreign key(`gn_idx`) references `genre_rf` (`gn_idx`)," +
                "foreign key(`art_idx`) references `artist_tb` (`art_idx`) on delete cascade" +
                ")";
            string Create_list_tb = "create table if not exists list_tb (" +
                "lst_idx INTEGER primary key AUTOINCREMENT," +
                "u_idx int not null," +
                "lst_title text not null," +
                "foreign key(`u_idx`) references `user_tb` (`u_idx`) on delete cascade" +
                ")";
            string Create_listmusic_tb = "create table if not exists listmusic_tb (" +
                "lsm_idx INTEGER primary key AUTOINCREMENT," +
                "lst_idx int not null," +
                "mus_idx int not null," +
                "lsm_pos int not null," +
                "foreign key(`lst_idx`) references `list_tb` (`lst_idx`)," +
                "foreign key(`mus_idx`) references `music_tb` (`mus_idx`)" +
                ")";
            executeQuery(Create_user_tb);
            executeQuery(Create_artist_tb);
            executeQuery(Create_genre_rf);
            executeQuery(Create_music_tb);
            executeQuery(Create_list_tb);
            executeQuery(Create_listmusic_tb);

            InitGenre();
            InitUser();
            InitArtist();
            InitMusic();
        }
        public static SqliteDataReader executeQuery(string query)
        {
            return new SqliteCommand(query, db).ExecuteReader();
        }
        public static bool execute(string query)
        {
            try
            {
                new SqliteCommand(query, db).ExecuteReader();
                return true;
            }
            catch(SqliteException e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        public static List<T> Select<T>(string query)
        {
            return db.Query<T>(query).AsList();
        }
    }
}
